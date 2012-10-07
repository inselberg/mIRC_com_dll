// mirc dll .... test com
// register: C:\...\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe /codebase ..\Release\mIRC_com_dll.dll
// unregister: C:\...\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe /unregister ..\Release\mIRC_com_dll.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.IO;

namespace mIrcComDll
{
    [Guid("EAA4976A-45C3-4BC5-BC0B-E474F4C3C83F")]
    public interface ComClass1Interface
    {
        [DispId(1)]
        string Version();

        [DispId(2)]
        string Status();

        [DispId(3)]
        void Clear();

        [DispId(4)]
        string First();

        [DispId(5)]
        string Last();

        [DispId(6)]
        bool isLast();

        [DispId(7)]
        string Next();

        [DispId(8)]
        string Current();

        [DispId(9)]
        string Add(string str);

        [DispId(10)]
        int Count();

        [DispId(11)]
        int IndexOf(string str);

        [DispId(12)]
        string GetItem(int idx);

        [DispId(13)]
        string Load(string fileName);

        [DispId(14)]
        string Save(string fileName);

        [DispId(15)]
        string Insert(string str, int index = 0);

        [DispId(16)]
        void Delete(int index);

    }

    [Guid("7BD20046-DF8C-44A6-8F6B-687FAA26FA71"),
        InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface ComClass1Events
    {
    }

    [Guid("0D53A3E8-E51A-49C7-944E-E72A2064F938"),
        ClassInterface(ClassInterfaceType.None),
        ComSourceInterfaces(typeof(ComClass1Events))]
    public class Listen : ComClass1Interface
    {

        private readonly List<string> liste = new List<string>() { };
        private int cursor = -1;
        private DateTime startTime = DateTime.Now;
        private string currentStr = "";

        #region Helper

        private bool isInRange(int idx)
        {
            bool b = ((idx >= 0) && (idx < liste.Count));
            if (!b) cursor = -1;
            return b;
        }
       
        
        // used to avoid that Current() is changed if a item is insert at (the current) cursor position
        private void SetCurrentStr()
        {
            if (isInRange(cursor)) currentStr = liste[cursor];
        }

        #endregion

        public string Version()
        {
            return "0.041";
        }

        public string Status()
        {
            return String.Format("[Position: {0}/{1}] [FirstDateTime: {2}] [Running: {3}] [Current: {4}]  ", cursor + 1, Count(), startTime, Math.Truncate((DateTime.Now - startTime).TotalMinutes), currentStr);
        }

        public void Clear()
        {
            liste.Clear();
            cursor = -1;
        }


        public string First()
        {
            startTime = DateTime.Now;
            cursor = 0;
            SetCurrentStr();

            if (isInRange(cursor))
                return liste[cursor];
            else
                return "";
        }

        

        public string Current()
        {
            SetCurrentStr();
            return currentStr;
        }

        public string Last()
        {
            cursor = liste.Count - 1;
            SetCurrentStr();
            if (isInRange(cursor)) return liste[cursor];
            return "";
        }

        public bool isLast()
        {
            return cursor == liste.Count - 1;
        }

        public string Next()
        {
            cursor++;
            SetCurrentStr();
            if (isInRange(cursor)) return liste[cursor];
            return "";
        }



        public string Add(string str)
        {
            // first element added, set cursor to first position
            if (liste.Count() == 0) cursor = 0;
            if (String.IsNullOrEmpty(currentStr)) SetCurrentStr();
            //

            liste.Add(str);
            return String.Format("[{0}] ", str);

        }

        public string AddStr(string str)
        {
            liste.Add(str);
            return String.Format("[{0}] ", str);
        }


        public int Count()
        {
            return liste.Count();
        }

        public int IndexOf(string str)
        {
            return liste.IndexOf(str);
        }


        public string GetItem(int idx)
        {
            if ((idx >= 0) && (idx < liste.Count)) return liste[idx];
            return "";
        }


        public string Load(string fileName = "mirc_com_dll.queue")
        {
            liste.Clear();
            using (StreamReader file = new StreamReader(fileName))
                while (file.Peek() >= 0)
                {
                    liste.Add(file.ReadLine());
                }

            // Set cursor to the 1st position
            if (isInRange(0)) {
                cursor = 0;
                SetCurrentStr();
            }
            return String.Format("Loaded [{0}]", fileName);
        }


        public string Save(string fileName = "mirc_com_dll.queue")
        {
            using (StreamWriter file = new StreamWriter(fileName))
                foreach (string line in liste)
                {
                    file.WriteLine(line);
                }
            return String.Format("Saved [{0}]", fileName);

        }


        // -1 insert at cursor next position
        public string Insert(string str, int index = -1)
        {
            // insert at the 1st position of the queue or if still "running" after the current position
            if (index == -1)
            {
                index = String.IsNullOrEmpty(str) ? 0 : cursor+1;
            }
            liste.Insert(index, str);
            return String.Format("Insert [{0}] at {1}", str, index);
        }


        public void Delete(int index)
        {
            liste.RemoveAt(index);
        }




    }





}
