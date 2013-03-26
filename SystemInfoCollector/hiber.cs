using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

namespace SystemInfoCollector
{
    class hiber
    {
        /// <summary>
        /// constructor
        /// </summary>
        public hiber()
        {
            
        }
        /// <summary>
        /// destructor
        /// </summary>
        ~hiber()
        { }


        /// use GetPwrCapabilities function
        /// to check if the system is capable
        /// of going into HIBERNATION
        ///[url][http://msdn.microsoft.com/en-us/library/aa372691%28VS.85%29.aspx
        ///[function definition]
        ///
        /// 
        
        /*
         BOOLEAN WINAPI GetPwrCapabilities(
  _Out_  PSYSTEM_POWER_CAPABILITIES lpSystemPowerCapabilities
);
         */ 
        
        ///pSystemPowerCapabilities [out]
        ///A pointer to a SYSTEM_POWER_CAPABILITIES structure 
        ///that receives the information.

        #region [SYSTEM_POWER_CAPABILITIES structure]

        ///[url][http://msdn.microsoft.com/en-us/library/aa373215%28VS.85%29.aspx]
        ///[SYSTEM_POWER_CAPABILITIES structure]
        ///[structure layout]

        /*
         * Contains information about the power capabilities of the system.
         typedef struct {
  BOOLEAN                 PowerButtonPresent;
         * If this member is TRUE, there is a system power button.
  BOOLEAN                 SleepButtonPresent;
         * If this member is TRUE, there is a system sleep button.
  BOOLEAN                 LidPresent;
         * If this member is TRUE, there is a lid switch.
  BOOLEAN                 SystemS1;
         * If this member is TRUE, the operating system supports sleep state S1.
         * [System Power States]
         * [url][http://msdn.microsoft.com/en-us/library/windows/desktop/aa373229(v=vs.85).aspx]
  BOOLEAN                 SystemS2;
         * If this member is TRUE, the operating system supports sleep state S2.
  BOOLEAN                 SystemS3;
         * If this member is TRUE, the operating system supports sleep state S3.
  BOOLEAN                 SystemS4;
         * If this member is TRUE, the operating system supports sleep state S4
         * (hibernation).
  BOOLEAN                 SystemS5;
         * If this member is TRUE, the operating system supports power off state
         * S5 (soft off).
  BOOLEAN                 HiberFilePresent;
         * If this member is TRUE, the system hibernation file is present.
  BOOLEAN                 FullWake;
         * If this member is TRUE, the system supports wake capabilities.
  BOOLEAN                 VideoDimPresent;
         * If this member is TRUE, the system supports 
         * video display dimming capabilities.
  BOOLEAN                 ApmPresent;
         * If this member is TRUE, the system supports 
         * APM BIOS power management features.
  BOOLEAN                 UpsPresent;
         * If this member is TRUE, there is an 
         * uninterruptible power supply (UPS).
  BOOLEAN                 ThermalControl;
         * If this member is TRUE, the system supports thermal zones.
  BOOLEAN                 ProcessorThrottle;
         * If this member is TRUE, the system supports processor throttling.
  BYTE                    ProcessorMinThrottle;
         * The minimum level of 
         * system processor throttling supported, expressed as a percentage.
  BYTE                    ProcessorMaxThrottle;
         * The maximum level of 
         * system processor throttling supported, expressed as a percentage.
  BOOLEAN                 FastSystemS4;
         * If this member is TRUE, the system supports the hybrid sleep state.
  BOOLEAN                 HiberBoot;
         * If this member is set to TRUE, the system is currently 
         * capable of performing a fast startup transition. 
         * This setting is based on whether 
         * the machine is capable of hibernate,
         * whether the machine currently has hibernate enabled (hiberfile exists),
         * and the local and group policy settings for using hibernate 
         * (including the Hibernate option in the Power control panel).
  BOOLEAN                 WakeAlarmPresent;
         * If this member is TRUE, the platform has support for 
         * ACPI wake alarm devices. 
         * For more details on wake alarm devices, 
         * please see the ACPI specification section 9.18.
  BOOLEAN                 AoAc;
         * If this member is TRUE, the system supports 
         * the connected standby power model.
         * Windows XP, Windows Server 2003, Windows Vista, Windows Server 2008,
         * Windows 7, and Windows Server 2008 R2:  
         * This value is supported starting in 
         * Windows 8 and Windows Server 2012
  BOOLEAN                 DiskSpinDown;
         * If this member is TRUE, the system supports 
         * allowing the removal of power to fixed disk devices.
  BYTE                    spare3[8];
         * Reserved.
  BOOLEAN                 SystemBatteriesPresent;
         * If this member is TRUE, there are one or more batteries in the system.
  BOOLEAN                 BatteriesAreShortTerm;
         * If this member is TRUE, 
         * the system batteries are short-term. 
         * Short-term batteries are used in 
         * uninterruptible power supplies (UPS).
  BATTERY_REPORTING_SCALE BatteryScale[3];
         * A BATTERY_REPORTING_SCALE structure that contains information
         * about how system battery metrics are reported.
  SYSTEM_POWER_STATE      AcOnLineWake;
         * The lowest system sleep state (Sx) that will generate
         * a wake event when the system is on AC power. 
         * This member must be one of the 
         * SYSTEM_POWER_STATE enumeration type values.
  SYSTEM_POWER_STATE      SoftLidWake;
         * The lowest system sleep state (Sx) that will generate
         * a wake event  via the lid switch. 
         * This member must be one of the 
         * SYSTEM_POWER_STATE enumeration type values.
  SYSTEM_POWER_STATE      RtcWake;
         * The lowest system sleep state (Sx) supported by hardware 
         * that will generate a wake event 
         * via the Real Time Clock (RTC). 
         * This member must be one of the 
         * SYSTEM_POWER_STATE enumeration type values.
         * To wake the computer using the RTC, 
         * the operating system must also support waking from the sleep state 
         * the computer is in when the RTC generates the wake event. 
         * Therefore, the effective lowest sleep state 
         * from which an RTC wake event can wake the computer is the lowest sleep
         * state supported by the operating system 
         * that is equal to or higher than the value of RtcWake. 
         * To determine the sleep states that the operating system supports,
         * check the SystemS1, SystemS2, SystemS3, and SystemS4 members.
  SYSTEM_POWER_STATE      MinDeviceWakeState;
         * The minimum allowable system power state supporting wake events. 
         * This member must be 
         * one of the SYSTEM_POWER_STATE enumeration type values. 
         * Note that this state may change 
         * as different device drivers are installed on the system.
  SYSTEM_POWER_STATE      DefaultLowLatencyWake;
         * The default system power state used if an application calls 
         * RequestWakeupLatency with LT_LOWEST_LATENCY. 
         * This member must be one of the 
         * SYSTEM_POWER_STATE enumeration type values.
} SYSTEM_POWER_CAPABILITIES, *PSYSTEM_POWER_CAPABILITIES;

         */


        ///[url][http://msdn.microsoft.com/en-us/library/aa372668(v=vs.85).aspx]
        ///[BATTERY_REPORTING_SCALE]
        ///[structure layout]

        /*
          typedef struct {
  ULONG Granularity;
  ULONG Capacity;
} BATTERY_REPORTING_SCALE, *PBATTERY_REPORTING_SCALE;

         */


        ///[url][http://msdn.microsoft.com/en-us/library/aa373227(v=vs.85).aspx]
        ///[SYSTEM_POWER_STATE enumeration]
        ///[enumeration members]

        /*
          typedef enum _SYSTEM_POWER_STATE { 
  PowerSystemUnspecified  = 0,
  PowerSystemWorking      = 1,
  PowerSystemSleeping1    = 2,
  PowerSystemSleeping2    = 3,
  PowerSystemSleeping3    = 4,
  PowerSystemHibernate    = 5,
  PowerSystemShutdown     = 6,
  PowerSystemMaximum      = 7
} SYSTEM_POWER_STATE, *PSYSTEM_POWER_STATE;

         */




        ///Define the 
        ///SYSTEM_POWER_CAPABILITIES (Structures)


        ///[url][http://msdn.microsoft.com/en-IN/library/system.runtime.interopservices.unmanagedtype.aspx]
       /// [UnmanagedType Enumeration]
       /// 

///This structure internally refers to 1 other structure
///and an enum
///BATTERY_REPORTING_SCALE  [structure]
///SYSTEM_POWER_STATE [enum]

       
        /// <summary>
        /// BATTERY_REPORTING_SCALE  [structure]
        /// <summary>

        [StructLayout(LayoutKind.Sequential,Pack=1)]
        public struct BATTERY_REPORTING_SCALE
        {
            public ulong Granularity;
            public ulong Capacity;
        };


        /// <summary>
        /// SYSTEM_POWER_STATE [enum]
        /// </summary>

        public enum SYSTEM_POWER_STATE 
        {
            PowerSystemUnspecified  = 0,
            PowerSystemWorking      = 1,
            PowerSystemSleeping1    = 2,
            PowerSystemSleeping2    = 3,
            PowerSystemSleeping3    = 4,
            PowerSystemHibernate    = 5,
            PowerSystemShutdown     = 6,
            PowerSystemMaximum      = 7
        } ;



        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SYSTEM_POWER_CAPABILITIES
        {
            [MarshalAs(UnmanagedType.Bool)]
            public bool PowerButtonPresent;

            [MarshalAs(UnmanagedType.Bool)]
            public bool SleepButtonPresent;

            [MarshalAs(UnmanagedType.Bool)]
            public bool LidPresent;

            [MarshalAs(UnmanagedType.Bool)]
            public bool SystemS1;

            [MarshalAs(UnmanagedType.Bool)]
            public bool SystemS2;

            [MarshalAs(UnmanagedType.Bool)]
            public bool SystemS3;

            [MarshalAs(UnmanagedType.Bool)]
            public bool SystemS4;

            [MarshalAs(UnmanagedType.Bool)]
            public bool SystemS5;

            [MarshalAs(UnmanagedType.Bool)]
            public bool HiberFilePresent;

            [MarshalAs(UnmanagedType.Bool)]
            public bool FullWake;

            [MarshalAs(UnmanagedType.Bool)]
            public bool VideoDimPresent;
            
            [MarshalAs(UnmanagedType.Bool)]
            public bool ApmPresent;
            
            [MarshalAs(UnmanagedType.Bool)]
            public bool UpsPresent;
            
            [MarshalAs(UnmanagedType.Bool)]
            public bool ThermalControl;

            [MarshalAs(UnmanagedType.Bool)]
            public bool ProcessorThrottle;

            public byte ProcessorMinThrottle;

            public byte ProcessorMaxThrottle;

            [MarshalAs(UnmanagedType.Bool)]
            public bool FastSystemS4;

            [MarshalAs(UnmanagedType.Bool)]
            public bool HiberBoot;

            [MarshalAs(UnmanagedType.Bool)]
            public bool WakeAlarmPresent;

            [MarshalAs(UnmanagedType.Bool)]
            public bool AoAc;

            [MarshalAs(UnmanagedType.Bool)]
            public bool DiskSpinDown;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            private byte[] spare3;

            [MarshalAs(UnmanagedType.Bool)]
            public bool SystemBatteriesPresent;

            [MarshalAs(UnmanagedType.Bool)]
            public bool BatteriesAreShortTerm;

            public BATTERY_REPORTING_SCALE[] BatteryScale;

            public SYSTEM_POWER_STATE AcOnLineWake;
            
            public SYSTEM_POWER_STATE SoftLidWake;
            
            public SYSTEM_POWER_STATE RtcWake;
            
            public SYSTEM_POWER_STATE MinDeviceWakeState;
            
            public SYSTEM_POWER_STATE DefaultLowLatencyWake; 

        };

       ///<summary>
       ///create a new structure of type SYSTEM_POWER_CAPABILITIES
       ///</summary>

        static SYSTEM_POWER_CAPABILITIES _SYSTEM_POWER_CAPABILITIES;



        ///<summary>
        ///Variable declarations to retrieve the contents of the 
        ///SYSTEM_POWER_CAPABILITIES
        ///structure
        ///</summary>


    
         public bool _PowerButtonPresent
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.PowerButtonPresent;
            }
           
        }
    
         public bool _SleepButtonPresent
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.SleepButtonPresent;
            }
           
        }
    
         public bool _LidPresent
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.LidPresent;
            }
           
        }
   
         public bool _SystemS1
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.SystemS1;
            }
           
        }
   
        public bool _SystemS2
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.SystemS2;
            }
           
        }
    
        public bool _SystemS3
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.SystemS3;
            }
           
        }
   
        public bool _SystemS4
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.SystemS4;
            }
           
        }
   
        public bool _SystemS5
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.SystemS5;
            }
           
        }
   
        public bool _HiberFilePresent
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.HiberFilePresent;
            }
           
        }
    
    public bool _FullWake
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.FullWake;
            }
           
        }
   
         public bool _VideoDimPresent
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.VideoDimPresent;
            }
           
        }
    
         public bool _ApmPresent
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.ApmPresent;
            }
           
        }
   
        public bool _UpsPresent
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.UpsPresent;
            }
           
        }
    
         public bool _ThermalControl
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.ThermalControl;
            }
           
        }
   
         public bool _ProcessorThrottle
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.ProcessorThrottle;
            }
           
        }

   
         public byte _ProcessorMinThrottle
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.ProcessorMinThrottle;
            }
           
        }
   
         public byte _ProcessorMaxThrottle
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.ProcessorMaxThrottle;
            }
           
        }
  
        public bool _FastSystemS4
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.FastSystemS4;
            }
           
        }
   
        public bool _HiberBoot
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.HiberBoot;
            }
           
        }
   
        public bool _WakeAlarmPresent
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.WakeAlarmPresent;
            }
           
        }
    
        public bool _AoAc
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.AoAc;
            }
           
        }
    
        public bool _DiskSpinDown
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.DiskSpinDown;
            }
           
        }
    private byte[]      spare3;//system reserved
   
         public bool _SystemBatteriesPresent
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.SystemBatteriesPresent;
            }
           
        }
   
         public bool _BatteriesAreShortTerm
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.BatteriesAreShortTerm;
            }
           
        }
   
         public BATTERY_REPORTING_SCALE[] _BatteryScale
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.BatteryScale;
            }
           
        }
    
         public SYSTEM_POWER_STATE _AcOnLineWake
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.AcOnLineWake;
            }
           
        }
   
         public SYSTEM_POWER_STATE _SoftLidWake
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.SoftLidWake;
            }
           
        }
    
         public SYSTEM_POWER_STATE _RtcWake
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.RtcWake;
            }
           
        }
   
         public SYSTEM_POWER_STATE _MinDeviceWakeState
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.MinDeviceWakeState;
            }
           
        }
   
         public SYSTEM_POWER_STATE _DefaultLowLatencyWake
        {
            get
            {
                return _SYSTEM_POWER_CAPABILITIES.DefaultLowLatencyWake;
            }

        }


        #endregion



         /*
         BOOLEAN WINAPI GetPwrCapabilities(
  _Out_  PSYSTEM_POWER_CAPABILITIES lpSystemPowerCapabilities
);
         */

         ///<summary>
         ///GetPwrCapabilities
         ///
         ///</summary>
         ///<param name="_SYSTEM_POWER_CAPABILITIES">
         ///A SYSTEM_POWER_CAPABILITIES structure
         ///</param>

         ///<returns>
         ///bool
         ///onSuccess --> returns a non-zero value
         ///onFailure -->  return value is zero
         ///To get extended error information, call GetLastError.
         ///or 
         /// int LastWin32Error = 
         /// System.Runtime.InteropServices.Marshal.GetLastWin32Error();
         ///</returns>

         [DllImport("PowrProf.dll", SetLastError = true, CharSet = CharSet.Auto)]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool GetPwrCapabilities(
             out SYSTEM_POWER_CAPABILITIES _SYSTEM_POWER_CAPABILITIES);


         public int getPowerCapabilities()
         {
             
             bool result = GetPwrCapabilities(out _SYSTEM_POWER_CAPABILITIES);
            

             System.Diagnostics.Debug.WriteLine(
                 "Function run status : "+ result
                 +"\n"
                  + "PowerButtonPresent : " + this._PowerButtonPresent
                 + "\n"
                 + "_SleepButtonPresent : " + this._SleepButtonPresent
                  + "\n"
                  + "_LidPresent : " + this._LidPresent
                   + "\n"
                   + "_SystemS1 : " + this._SystemS1
                    + "\n"
                    + "_SystemS2 : " + this._SystemS2
                     + "\n"
                     + "_SystemS3 : " + this._SystemS3
                      + "\n"
                      + "_SystemS4 : " + this._SystemS4
                       + "\n"
                      + "_SystemS5 : " + this._SystemS5
                        + "\n"
                        + "_HiberFilePresent : " + this._HiberFilePresent
                         + "\n"
                         + "_FullWake : " + this._FullWake
                          + "\n"
                          + "_VideoDimPresent : " + this._VideoDimPresent
                           + "\n"
                          + "_ApmPresent : " + this._ApmPresent
                            + "\n"
                           + "_UpsPresent : " + this._UpsPresent
                             + "\n"
                            + "_ThermalControl : " + this._ThermalControl
                              + "\n"
                            + "_ProcessorThrottle : " + this._ProcessorThrottle
                               + "\n"
                              + "_ProcessorMinThrottle : " + this._ProcessorMinThrottle
                                + "\n"
                               + "ProcessorMaxThrottle : " + this._ProcessorMaxThrottle
                                 + "\n"
                                + "_FastSystemS4 : " + this._FastSystemS4
                                  + "\n"
                                 + "_HiberBoot : " + this._HiberBoot
                                   + "\n"
                                  + "_WakeAlarmPresent : " + this._WakeAlarmPresent
                                    + "\n"
                                   + "_AoAc : " + this._AoAc
                                     + "\n"
                                     + "_DiskSpinDown : " + this._DiskSpinDown
                                      + "\n"
                                      + "_SystemBatteriesPresent : " + this._SystemBatteriesPresent
                                       + "\n"
                                      + "_BatteriesAreShortTerm : " + this._BatteriesAreShortTerm
                                        + "\n"
                                       + "_BatteryScale : " + this._BatteryScale
                                         + "\n"
                                        + "_AcOnLineWake : " + this._AcOnLineWake
                                         +"\n"
                                        + "_SoftLidWake : " + this._SoftLidWake
                                         +"\n"
                                        + "_RtcWake : " + this._RtcWake
                                         +"\n"
                                       + "_MinDeviceWakeState : " + this._MinDeviceWakeState
                                         +"\n"
                                       + "_DefaultLowLatencyWake : " + this._DefaultLowLatencyWake
                                         +"\n"
                           
                 );
             return Convert.ToInt32(
             System.Runtime.InteropServices.Marshal.GetLastWin32Error()); 
         }
      



    }





    }

