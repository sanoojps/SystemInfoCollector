using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Windows.Forms;

namespace SystemInfoCollector
{
    class Program
    {
        static void Main(string[] args)
        {
            GetSystemInfo infoCollector = new GetSystemInfo();


            infoCollector.CheckIfAdmin();

            Console.WriteLine(System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory()
                );

            Console.WriteLine(infoCollector.getUserName());
            Console.WriteLine(
                System.Security.Principal.WindowsIdentity.GetCurrent().Name);

            Console.WriteLine(infoCollector.getComputerName());

            Console.WriteLine(infoCollector.getSystemManufacturer());

            Console.WriteLine(infoCollector.getSystemPartition());


            string a;

            Console.WriteLine(infoCollector.getListOfDrives(out a));
            Console.WriteLine(a);
            
            Console.ReadLine();
        }
    }
}
