// mirc dll .... test com
// register: C:\...\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe /codebase ..\Release\mIRC_com_dll.dll
// unregister: C:\...\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe /unregister ..\Release\mIRC_com_dll.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        /*
        [DispId(6)]
        int Delete(string str);

        
        [DispId(8)]
        void AddItem();

        [DispId(9)]
        string AddItemStr();


        */




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

        public string Version()
        {
            return "0.031";
        }

        public string Status()
        {
            return String.Format("[Position: {0}/{1}] [FirstDateTime: {2}] [Running: {3}] [Current: {4}]  ", cursor + 1, Count(), startTime, Math.Truncate((DateTime.Now - startTime).TotalMinutes), Current());
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
            if (isInRange(cursor))
                return liste[cursor];
            else
                return "";
        }

        public string Current()
        {
            if (isInRange(cursor))
                return liste[cursor];
            else
                return "";
        }

        public string Last()
        {
            cursor = liste.Count - 1;
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
            if (isInRange(0)) return liste[cursor];
            return "";
        }



        public string Add(string str)
        {
            // first element added, set cursor to first position
            if (liste.Count() == 0) cursor = 0;


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


        // 
        bool isInRange(int idx)
        {
            bool b = ((idx >= 0) && (idx < liste.Count));
            if (!b) cursor = -1;
            return b;
        }

        public string Load(string fileName = "mirc_com_dll.queue")
        {
            liste.Clear();
            using (System.IO.StreamReader file = new System.IO.StreamReader(fileName))
                while (file.Peek() >= 0)
                {
                    liste.Add(file.ReadLine());
                }
            return String.Format("Loaded \"[{0}]\" ", fileName);
        }


        public string Save(string fileName = "mirc_com_dll.queue")
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
                foreach (string line in liste)
                {
                    file.WriteLine(line);
                }
            return String.Format("Saved \"[{0}]\" ", fileName);

        }

        /*
        public int Delete(string str)
        {
            var1 = "Delete() aufgerufen";
            liste.Remove(str);
            return 0;
        }

      

        //
        public void AddItem()
        {
            var1 = "AddItem() aufgerufen";
            Random random = new Random();
            string str = random.Next(1000000, 999999).ToString();
            

            ///           liste.Add(str);
        }

        public string AddItemStr()
        {
            var1 = "AddItemStr() aufgerufen";
        //    Random random = new Random();
          //  string str = random.Next(1000000, 999999).ToString();
            
            return "str";
            
        }
        */


    }





}
