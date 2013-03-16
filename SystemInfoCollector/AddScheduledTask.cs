using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace SystemInfoCollector
{
    class AddScheduledTask
    {
        public AddScheduledTask()
        {

        }

        public static string FramewrokV2Location =
         @"\Windows\Microsoft.NET\Framework\v2.0.50727";
        public static string systemdrive =
            Environment.GetEnvironmentVariable("SYSTEMDRIVE").ToString();
        public string installutilLocation =
            string.Concat(systemdrive, FramewrokV2Location);


        public void _InstallServiceUsingInstallutil(string _serviceLocation)
        {
            //System.Diagnostics.Process.Start(installutilLocation + @"\installutil.exe",_serviceLocation);

            //initialize a set of values 
            //that are used when you start a process.
            ProcessStartInfo _processStartInfo =
            new ProcessStartInfo();

            //add parameters

            //Gets or sets a value indicating whether to use 
            //the operating system shell to start the process.
            _processStartInfo.UseShellExecute = true;

            //Launch This Process as Administrator
            _processStartInfo.Verb = "runas";

            //Gets or sets the window state to use when 
            //the process is started.
            //Launch process Hidden
            _processStartInfo.WindowStyle =
                ProcessWindowStyle.Hidden;

            //sets the directory that contains the process to be 
            //started
            _processStartInfo.WorkingDirectory =
                installutilLocation;

            //add "InstallUtil.exe" command
            _processStartInfo.FileName = "InstallUtil.exe";


            //pass arguments to installUtil
            _processStartInfo.Arguments = _serviceLocation;



            //Instantiate the process Class

            Process _installutilLaunchprocess =
                new Process();

            //add processStartInfo to the Process
            _installutilLaunchprocess.StartInfo =
                _processStartInfo;

            //Console.WriteLine(_processStartInfo.WorkingDirectory.ToString());
            //Console.WriteLine(_processStartInfo.FileName.ToString());
            //Console.WriteLine(_processStartInfo.Arguments.ToString());
            //Console.WriteLine(_processStartInfo.WorkingDirectory.ToString());


            try
            {
                //Launch the process
                _installutilLaunchprocess.Start();
            }

            catch
            {

            }

            finally
            {

            }
        }

    }
}
