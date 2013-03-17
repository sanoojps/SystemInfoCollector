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
        private string videoCardName;
        private string _getOSVersion;
        private string _SystemUptime;

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



        #region VideoCardName

        public string getVideoCardName()
        {
            connectionOptions.Impersonation =
                       System.Management.ImpersonationLevel.Impersonate;
            scope.Connect();
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


        #region OS Version

        public string getOSVersion()
        {
            connectionOptions.Impersonation =
                       System.Management.ImpersonationLevel.Impersonate;
            scope.Connect();
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

        #region SystemUptime

        public string getSystemUptime()
        {
            connectionOptions.Impersonation =
                       System.Management.ImpersonationLevel.Impersonate;
            scope.Connect();

            

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












    } 
}
