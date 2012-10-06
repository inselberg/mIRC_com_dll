mIRC_com_dll
------------
Example of a mIRC com.library in c#.
Adding an easy way to handle lists in mIRC.


install
-------
IMPORTANT: use the regasm from a framework >=4 

register:

	C:\...\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe /codebase Release\mIRC_com_dll.dll
unregister:

	C:\...\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe /unregister Release\mIRC_com_dll.dll




test (mIRC) 
-----------
	/load -rs scripts/mirccomdll.mrc

and test it with:

	/co
	/status


commands
--------
List of commands and return value.
Using: 

	var %result = $com(lmirc,<COMMAND>,1)
	echo $com(lmirc).result

or functions with parameters 

	var %result = $com(lmirc,<COMMAND>,1,<bstr OR int>,<VALUE>)
	echo $com(lmirc).result

check scripts/mirccomdll.mrc for examples


list of commands
----------------

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


todo:
-----
write a todo list ;)