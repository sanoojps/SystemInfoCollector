using System;
using System.Collections.Generic;
using System.Text;


using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Management;
using System.IO;


namespace SystemInfoCollector
{
    public class GetSystemInfo
    {

        [DllImport("shell32.dll")]
        public static extern bool IsUserAnAdmin();



        #region WMI

        private static ConnectionOptions connectionOptions = 
            new ConnectionOptions();

        // Make a connection to a remote computer. 
        // Replace the "FullComputerName" section of the
        // string "\\\\FullComputerName\\root\\cimv2" with
        // the full computer name or IP address of the 
        // remote computer.

        private static string ComputerName = @"\\.";
        private static string Namespace = @"\root\cimv2";
        private static string ManagementPath = ComputerName + Namespace;

        private static ManagementScope scope = 
            new ManagementScope(ManagementPath, connectionOptions);

        //seperate declaration of return variables
        private string manufacturerName;
        private string listOfDrives;

        #endregion



        public GetSystemInfo()
        {
        }


        #region CheckIfAdmin

        public void CheckIfAdmin()
        {
            if (IsUserAnAdmin())
            {
   MessageBox.Show("User is Admin");
            }
            else
            {
   MessageBox.Show("Not a Admin");
            }

            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            if (identity == null) 
                throw new InvalidOperationException(
                    "Couldn't get the current user identity");
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            MessageBox.Show(
                principal.IsInRole(WindowsBuiltInRole.Administrator).ToString());
        }

        #endregion



        #region UserName

        public string getUserName()
        {
            string currentUserName =
                (Environment.UserName).ToString();

            return currentUserName;
        }

        #endregion



        #region ComputerName

        public string getComputerName()
        {
            string computerName =
                (SystemInformation.ComputerName).ToString();

            return computerName;
        }

        #endregion



        #region SystemPartition

        public string getSystemPartition()
        {
            string windir = 
                Environment.SystemDirectory; // C:\windows\system32
            string SystemPartition = 
                Path.GetPathRoot(Environment.SystemDirectory); // C:\
            return SystemPartition;

        }


        #endregion


        #region '<pNumberOfPartitions>'

        //returns both list of drives and and number of drives
        public int getListOfDrives(out string listODrives)
        {
            string[] strDrives = Environment.GetLogicalDrives();
            int numberOfDrives = strDrives.Length;
            StringBuilder DrivesBuilder = new StringBuilder();
            int c = 0;
            foreach (string strDrive in strDrives)
            {
                c = c + 1;

                if (c >= numberOfDrives)
                {
                    listOfDrives = 
                        DrivesBuilder.Append(strDrive).ToString();
                }

                else
                {
                    listOfDrives =
                   DrivesBuilder.Append(strDrive).Append(",").ToString();
                }

            }

            //return listOfDrives;
            listODrives = listOfDrives;
          
            return numberOfDrives;
        }
        #endregion



        //WMI - Queries

        #region SystemManufacturer

        public string getSystemManufacturer()
        {
            connectionOptions.Impersonation =
                       System.Management.ImpersonationLevel.Impersonate;
            scope.Connect();
            try 
            {
            //Query system for Operating System information
            ObjectQuery query = new ObjectQuery(
                "SELECT * FROM Win32_ComputerSystem");
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(scope, query);

            ManagementObjectCollection queryCollection = 
                searcher.Get();
            
            foreach (ManagementObject m in queryCollection)
            {
                // get computer information Manufacturer
                manufacturerName =
                    m["Manufacturer"].ToString();
                
            }
            }

            catch(ManagementException exception)
            {
                manufacturerName = "Unable to Read";
                System.Diagnostics.Debug.WriteLine(exception);
            }


            return manufacturerName;

        }

        #endregion

















    } 
}
