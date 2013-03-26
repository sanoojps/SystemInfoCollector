using System;
using System.Collections.Generic;
using System.Text;

namespace SystemInfoCollector
{
    class powerInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public powerInfo()
        { }

        /// <summary>
        /// 
        /// </summary>
        ~powerInfo()
        { }


        /// <summary>
        /// POWER_INFORMATION_LEVEL enumeration
        /// for
        /// CallNtPowerInformation function
        /// NTSTATUS WINAPI CallNtPowerInformation
        /// (
        ///     _In_   POWER_INFORMATION_LEVEL InformationLevel,
        ///     _In_   PVOID lpInputBuffer,
        ///     _In_   ULONG nInputBufferSize,
        ///     _Out_  PVOID lpOutputBuffer,
        ///     _In_   ULONG nOutputBufferSize
        ///     );
        /// [url][http://msdn.microsoft.com/en-us/library/aa372675(v=vs.85).aspx]
        /// 
        /// C# Signature:

        /// [DllImport("powrprof.dll", SetLastError = true, CharSet = CharSet.Auto)]

        /// private static extern UInt32 CallNtPowerInformation(
        ///     Int32 InformationLevel,
        ///     IntPtr lpInputBuffer,
        ///     UInt32 nInputBufferSize,
        ///     IntPtr lpOutputBuffer,
        ///     UInt32 nOutputBufferSize
        ///     );
        /// </summary>
        
        private enum POWER_INFORMATION_LEVEL 
        {
            AdministratorPowerPolicy        = 9,
            LastSleepTime                   = 15,
            LastWakeTime                    = 14,
            ProcessorInformation            = 11,
            ProcessorPowerPolicyAc          = 18,
            ProcessorPowerPolicyCurrent     = 22,
            ProcessorPowerPolicyDc          = 19,
            SystemBatteryState              = 5,
            SystemExecutionState            = 16,
            SystemPowerCapabilities         = 4,
            SystemPowerInformation          = 12,
            SystemPowerPolicyAc             = 0,
            SystemPowerPolicyCurrent        = 8,
            SystemPowerPolicyDc             = 1,
            SystemReserveHiberFile          = 10,
            VerifyProcessorPowerPolicyAc    = 20,
            VerifyProcessorPowerPolicyDc    = 21,
            VerifySystemPolicyAc            = 2,
            VerifySystemPolicyDc            = 3
        }

    }
}
