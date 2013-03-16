using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Windows.Forms;

using System.Threading;
using System.ComponentModel;

namespace SystemInfoCollector
{
    class Program
    {
        static BackgroundWorker _bw = new BackgroundWorker();
        
        static void Main(string[] args)
        {
            
            _bw.WorkerReportsProgress = true;
             _bw.DoWork += _bw_DoWork;
            _bw.RunWorkerAsync("Message To Worker");

            //backgroundWorker h = new backgroundWorker();



           
            
            Console.ReadLine();
        }

        static void _bw_DoWork(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine(e.Argument);

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

            Console.WriteLine(infoCollector.getVideoCardName());

            Console.WriteLine(
                infoCollector.getIpAddress());

            infoCollector.DisplayIPAddresses();

            Console.WriteLine(
                 infoCollector.getOSVersion());
                

        }




    }
}
