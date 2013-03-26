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

       


        /// <summary>
        /// 
        /// </summary>
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
      
        #endregion



        public GetSystemInfo()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        #region CheckIfAdmin

        [DllImport("shell32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)] 
        public static extern bool IsUserAnAdmin();

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


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        #region UserName

        public string getUserName()
        {
            string currentUserName =
                (Environment.UserName).ToString();

            return currentUserName;
        }

        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        #region ComputerName

        public string getComputerName()
        {
            string computerName =
                (SystemInformation.ComputerName).ToString();

            return computerName;
        }

        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// getListOfDrives
        /// returns both list of drives and and number of drives
        /// </summary>
        /// <param name="listODrives"></param>
        /// <returns>
        /// listOfDrives
        /// </returns>
        #region '<pNumberOfPartitions>'

        
        public int getListOfDrives(out string listODrives)
        {
            //returns both list of drives and and number of drives
            string listOfDrives = string.Empty;
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



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        #region IPAddress

        public string getIpAddress()
        {
            System.Net.IPHostEntry host;
            string localIP = null;
            try
            {
                host =
                    System.Net.Dns.GetHostEntry(
                    System.Net.Dns.GetHostName());

                foreach (System.Net.IPAddress ip 
                    in host.AddressList)
                {
                    if (ip.AddressFamily.ToString()
                        == "InterNetwork")
                    {
                        localIP = ip.ToString();
                    }
                }
            }

            catch (System.Net.Sockets.SocketException eXception)
            {
                System.Diagnostics.Debug.WriteLine(
                    eXception.Message + "/n"
                    + eXception.NativeErrorCode + "/n"
                    + eXception.SocketErrorCode + "\n"
                    + eXception.Source + "\n"
                    + eXception.ErrorCode + "\n"
                    + eXception.Data + "\n"
                    + eXception.HelpLink + "\n"
                    + eXception.InnerException + "\n"
                    + eXception.StackTrace + "\n"
                    + eXception.TargetSite + "\n"
                    );

            }
            return localIP;
        }

        #endregion
        /// <summary>
        /// 
        /// </summary>
        #region DisplayIPAddress

        public void DisplayIPAddresses()
        {

            Console.WriteLine("\r\n****************************");
            Console.WriteLine("     IPAddresses");
            Console.WriteLine("****************************");


            StringBuilder sb = new StringBuilder();
            // Get a list of all network interfaces (usually one per network card, dialup, and VPN connection)     
            System.Net.NetworkInformation.NetworkInterface[] networkInterfaces = 
                System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();

            foreach (System.Net.NetworkInformation.NetworkInterface 
                network in networkInterfaces)
            {

                if (network.OperationalStatus == 
                    System.Net.NetworkInformation.OperationalStatus.Up)
                {
                    if (network.NetworkInterfaceType == 
                        System.Net.NetworkInformation.NetworkInterfaceType.Tunnel) continue;
                    if (network.NetworkInterfaceType == 
                        System.Net.NetworkInformation.NetworkInterfaceType.Tunnel) continue;
                    //GatewayIPAddressInformationCollection GATE = network.GetIPProperties().GatewayAddresses;
                    // Read the IP configuration for each network   

                    System.Net.NetworkInformation.IPInterfaceProperties 
                        properties = 
                        network.GetIPProperties();
                    //discard those who do not have a real gateaway 
                    if (properties.GatewayAddresses.Count > 0)
                    {
                        bool good = false;
                        foreach (
                            System.Net.NetworkInformation.GatewayIPAddressInformation 
                                gInfo in properties.GatewayAddresses)
                        {
                            //not a true gateaway (VmWare Lan)
                            if (!gInfo.Address.ToString().Equals("0.0.0.0"))
                            {
                                sb.AppendLine(
                                    " GATEAWAY " + gInfo.Address.ToString());
                                good = true;
                                break;
                            }
                        }
                        if (!good)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }
                    // Each network interface may have multiple IP addresses       
                    foreach (System.Net.NetworkInformation.IPAddressInformation address in properties.UnicastAddresses)
                    {
                        // We're only interested in IPv4 addresses for now       
                        if (address.Address.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork) continue;

                        // Ignore loopback addresses (e.g., 127.0.0.1)    
                        if (System.Net.IPAddress.IsLoopback(address.Address)) continue;

                        if (!address.IsDnsEligible) continue;
                        if (address.IsTransient) continue;

                        sb.AppendLine(address.Address.ToString() + " (" + network.Name + ") nType:" + network.NetworkInterfaceType.ToString());
                    }
                }
            }
            Console.WriteLine(sb.ToString());
        }


        #endregion


        //[pending Exception Checking]
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        #region SystemDriveFreeSpace

        public decimal SystemDriveFreeSpace()
        {
            DriveInfo d = new DriveInfo(Path.GetPathRoot(
            Environment.SystemDirectory));
            //System.Windows.Forms.MessageBox.Show(((long)d.TotalFreeSpace / (long)d.TotalSize)).ToString());
            //(long)d.TotalFreeSpace / (long)d.TotalSize);
            decimal totalSize =
                (decimal)d.TotalSize;
            decimal totalFreeSpace = (decimal)d.TotalFreeSpace;

            decimal percentFreeSpace = (totalFreeSpace / totalSize) * 100;


            return Math.Round(percentFreeSpace, 3);


        }

        #endregion

        
       
        //[pending Exception Checking]
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        #region IEVersion

        public string GetIEVersion()
        {
            string key = @"Software\Microsoft\Internet Explorer";
            string data = string.Empty;
            try
            {
                Microsoft.Win32.RegistryKey dkey =
                    Microsoft.Win32.Registry.LocalMachine.OpenSubKey(
                    key,
                     Microsoft.Win32.RegistryKeyPermissionCheck.Default);




                data = dkey.GetValue("Version").ToString();
            }

            catch(Exception eXception)
            {
                System.Diagnostics.Debug.WriteLine(
                    eXception.Message + "\n"
                    + eXception.StackTrace + "\n"
                    );
            }
                return data;
        }


        #endregion   

       
        #region GetHibernationStatus

        /// <summary>
        ///
        /// BOOLEAN WINAPI IsPwrHibernateAllowed(void);
        ///
        /// </summary>
        ///
        /// <returns>
        /// True - if hibernation Enabled
        /// False - if hibernation disabled
        /// </returns>

        [DllImport("powrprof.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsPwrHibernateAllowed();


        /// <summary>
        /// alternate Function to check if hibernation is enabled
        /// </summary>
        /// [url][http://msdn.microsoft.com/en-us/library/aa372705(v=vs.85).aspx]
        /// <returns></returns>






        /// <summary>
        /// isHibernationEnabled()
        /// </summary>
        /// [url][http://msdn.microsoft.com/en-us/library/aa372705(v=vs.85).aspx]
        /// <returns>
        /// onSuccess -- > string returned by IsPwrHibernateAllowed() [bool converted to string]
        /// onFailure -- > returns <Failed to retreive Hibernation Status>
        /// </returns>
        public string isHibernationEnabled()
        {
            string HiberEnabled = string.Empty;
            bool resultOfFun = IsPwrHibernateAllowed();
            //check lastError
            int errorOfFunc = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
            //check if there was an error
            if (errorOfFunc != 0)
            {
                HiberEnabled = "<Failed to retreive Hibernation Status>";
            }

            else
            {
                HiberEnabled = resultOfFun.ToString();
            }

            return HiberEnabled;
        }


        #endregion



        //WMI - Queries
        /// <summary>
        /// getSystemManufacturer
        /// </summary>
        /// <returns>manufacturerName</returns>
        #region SystemManufacturer

        public string getSystemManufacturer()
        {
            connectionOptions.Impersonation =
                       System.Management.ImpersonationLevel.Impersonate;
            scope.Connect();

            string manufacturerName = string.Empty;

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


        /// <summary>
        /// getVideoCardName
        /// </summary>
        /// <returns>
        /// videoCardName
        /// </returns>
        #region VideoCardName

        public string getVideoCardName()
        {
            connectionOptions.Impersonation =
                       System.Management.ImpersonationLevel.Impersonate;
            scope.Connect();

            string videoCardName = string.Empty;

            try
            {
                //Query system for Operating System information
                ObjectQuery query = new ObjectQuery(
                    "SELECT * FROM Win32_VideoController");
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, query);

                ManagementObjectCollection queryCollection =
                    searcher.Get();

                foreach (ManagementObject m in queryCollection)
                {
                    // get videoCardName
                    videoCardName =
                        m["Caption"].ToString();

                }
            }

            catch (ManagementException exception)
            {
                videoCardName = "Unable to Read";
                System.Diagnostics.Debug.WriteLine(exception);
            }


            return videoCardName;

        }


        #endregion

        /// <summary>
        /// getOSVersion
        /// </summary>
        /// <returns>
        /// _getOSVersion
        /// </returns>
        #region OS Version

        public string getOSVersion()
        {
            connectionOptions.Impersonation =
                       System.Management.ImpersonationLevel.Impersonate;
            scope.Connect();

            string _getOSVersion = string.Empty;

            try
            {
                //Query system for Operating System information
                ObjectQuery query = new ObjectQuery(
                    "SELECT * FROM Win32_OperatingSystem");
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, query);

                ManagementObjectCollection queryCollection =
                    searcher.Get();

                foreach (ManagementObject m in queryCollection)
                {
                    // get  getOSVersion
                    _getOSVersion =
                        m["Caption"].ToString() + "Service Pack " + m["ServicePackMajorVersion"].ToString();

                }
            }

            catch (ManagementException exception)
            {
                _getOSVersion = "Unable to Read";
                System.Diagnostics.Debug.WriteLine(exception);
            }


            return _getOSVersion;

        }


        #endregion



        /// <summary>
        /// getSystemUptime
        /// </summary>
        /// <returns>
        /// _SystemUptime
        /// </returns>
        #region SystemUptime

        public string getSystemUptime()
        {
            connectionOptions.Impersonation =
                       System.Management.ImpersonationLevel.Impersonate;
            scope.Connect();

           string _SystemUptime = string.Empty;

            try
            {
                //Query system for Operating System information
                ObjectQuery query = new ObjectQuery(
                    "SELECT * FROM Win32_OperatingSystem");
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, query);

                ManagementObjectCollection queryCollection =
                    searcher.Get();

                foreach (ManagementObject m in queryCollection)
                {
                    // get  SystemUptime
                    string _SUptime =
                        m["LastBootUpTime"].ToString();

                    DateTime System_Uptime =
                System.Management.ManagementDateTimeConverter.ToDateTime(_SUptime);

                    _SystemUptime = System_Uptime.ToString();
                }
            }

            catch (ManagementException exception)
            {
                _SystemUptime = "Unable to Read";
                System.Diagnostics.Debug.WriteLine(exception);
            }




            return _SystemUptime;

        }


        #endregion

        
        
        
       
        /// <summary>
        /// getSystemManufacturer //with model and pc name
        /// </summary>
        /// <param name="pcModel"></param>
        /// <param name="pcName"></param>
        /// <param name="pcTotalPhysicalMemory"></param>
        /// <param name="pcUserName"></param>
        /// <returns>
        ///  <param name="manufacturerName"></param>
        /// </returns>
        #region SystemManufacturer,pcModel,pcName,pcTotalPhysicalMemory,["UserName"]
        
        public string getSystemManufacturer(
            out string pcModel, out string pcName, out string pcTotalPhysicalMemory, out string pcUserName)
        {
            connectionOptions.Impersonation =
                       System.Management.ImpersonationLevel.Impersonate;
            scope.Connect();

            string manufacturerName = string.Empty;
            //with model and pc name
            pcModel = string.Empty;
            pcName = string.Empty;
            pcTotalPhysicalMemory = string.Empty;
            pcUserName = string.Empty;

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
                    pcModel = m["Model"].ToString();
                    pcName = m["Name"].ToString();
                    //pcTotalPhysicalMemory = m["TotalPhysicalMemory"].ToString();

                    pcTotalPhysicalMemory = Math.Round(
                        Convert.ToDecimal(
                        m["TotalPhysicalMemory"].ToString()
                        ) / (1024 * 1024 * 1024), 2) + " GB ";

                    pcUserName = m["UserName"].ToString();

                }
            }

            catch (ManagementException exception)
            {
                manufacturerName = "Unable to Read";
                pcModel = "Unable to Read";
                pcName = "Unable to Read";
                pcTotalPhysicalMemory = "Unable to Read";
                pcUserName = "Unable to Read";
                System.Diagnostics.Debug.WriteLine(exception);
            }


            return manufacturerName;

        }

        #endregion



        /// <summary>
        /// getProcessorInfo
        /// </summary>
        /// <param name="CurrentClockSpeed"></param>
        /// <param name="processorManufacturer"></param>
        /// <param name="MaxClockSpeed"></param>
        /// <returns>
        /// <param name ="processorName"></param>
        /// </returns>
        #region ProcessorInfo - processorName,CurrentClockSpeed,processorManufacturer,MaxClockSpeed

        public string getProcessorInfo(
            out string CurrentClockSpeed,
            out string processorManufacturer,
            out string MaxClockSpeed
            )
        {
            connectionOptions.Impersonation =
                       System.Management.ImpersonationLevel.Impersonate;
            scope.Connect();

            string processorName = string.Empty;
            CurrentClockSpeed = string.Empty;
            processorManufacturer = string.Empty;
            MaxClockSpeed = string.Empty;

            try
            {
                //Query system for Operating System information
                ObjectQuery query = new ObjectQuery(
                    "SELECT * FROM Win32_Processor");
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, query);

                ManagementObjectCollection queryCollection =
                    searcher.Get();

                foreach (ManagementObject m in queryCollection)
                {
                    // get processorInfo
                    processorName =
                        m["Name"].ToString();

                    CurrentClockSpeed =
                       m["CurrentClockSpeed"].ToString();

                    processorManufacturer =
                        m["Manufacturer"].ToString();

                    MaxClockSpeed =
                        m["MaxClockSpeed"].ToString();



                }
            }

            catch (ManagementException exception)
            {
                processorName = "Unable to Read";
                CurrentClockSpeed = "Unable to Read";
                processorManufacturer = "Unable to Read";
                MaxClockSpeed = "Unable to Read";

                System.Diagnostics.Debug.WriteLine(exception);
            }


            return processorName;

        }


        #endregion









    } 
}
