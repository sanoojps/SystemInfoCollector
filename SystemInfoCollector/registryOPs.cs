using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Win32;

///SystemInfoCollector 'own Registry Read/Write Class

namespace SystemInfoCollector
{
    /// <summary>
    /// 
    /// </summary>
    class registryOPs
    {
        /// <summary>
        /// 
        /// </summary>
        public registryOPs()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        ~registryOPs()
        {
        }

        /// <summary>
        /// eXceptionEnum
        /// </summary>
        #region eXceptionEnum

        private enum eXceptionEnum
        {
            QUERY_OR_WRITE_SUCCESSFUL = 0,
            QUERIED_DEFAULT_VALUE = 1,
            PERMISSION_DENIED = 2,//same as SECURITY_EXCEPTION
            NULL_REFERENCE_EXCEPTION = 3,
            IOEXCEPTION = 4,
            ARGUMENT_EXCEPTION = 5,
            KEY_DOES_NOT_EXIST = 6,
            KEY_VALUE_NAME_DOES_NOT_EXIST = 7,
            ARGUMENT_NULL_EXCEPTION = 8,
            UNAUTHORIZED_ACCESS_EXCEPTION = 9,
            SECURITY_EXCEPTION = 10 //same as PERMISSION_DENIED

        };






        //private void eXceptionD()
        //{
        //    Dictionary<int, string> eXceptionDictionary =
        //   new Dictionary<int, string>();

        //    eXceptionDictionary.Add(0, "QUERY_OR_WRITE_SUCCESSFUL");
        //    eXceptionDictionary.Add(1, "QUERIED_DEFAULT_VALUE");
        //    eXceptionDictionary.Add(2, "PERMISSION_DENIED");
        //    eXceptionDictionary.Add(3, "NULL_REFERENCE_EXCEPTION");
        //    eXceptionDictionary.Add(4, "IOEXCEPTION");
        //    eXceptionDictionary.Add(5, "ARGUMENT_EXCEPTION");
        //    eXceptionDictionary.Add(6, "KEY_DOES_NOT_EXIST");
        //    eXceptionDictionary.Add(7, "KEY_VALUE_NAME_DOES_NOT_EXIST");
        //    eXceptionDictionary.Add(8, "ARGUMENT_NULL_EXCEPTION");
        //    eXceptionDictionary.Add(9, "UNAUTHORIZED_ACCESS_EXCEPTION");
        //    eXceptionDictionary.Add(10, "SECURITY_EXCEPTION");
        //}




        #endregion


        /// <summary>
        /// keyRead - logs read success to readStatus [0 =  Success] 
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="keyValueName"></param>
        /// <param name="readStatus"></param>
        /// <returns>keyValueData</returns>
        #region keyRead

        public string keyRead(
            string keyName,
            string keyValueName,
            out uint readStatus)
        {
            readStatus = uint.MinValue;//ZERO
            string keyValueData = string.Empty;
            try
            {
                keyValueData =
                       Registry.GetValue(
                       keyName, keyValueName, "<Not Found>").ToString();

                //value returned will be "<Not Found>" if keyValueName not found
                //value returned will be "null" if key not found

                if (keyValueData.Equals(null))
                {
                    System.Diagnostics.Debug.WriteLine("\n"
                     + "Requested registry key does not exist."
                     + "\n"
                     + "RegistryKey : " + keyName
                     + "\n"
                     + "keyValueName : " + keyValueName
                     + "\n"

                     );

                    keyValueData = "key does not exist";
                    readStatus = (uint)eXceptionEnum.KEY_DOES_NOT_EXIST;
                }


                if (keyValueData.Equals("<Not Found>"))
                {
                    if (keyValueName != null)
                    {
                        System.Diagnostics.Debug.WriteLine("\n"
                         + "Requested registry keyValueName does not exist."
                         + "\n"
                         + "RegistryKey : " + keyName
                         + "\n"
                         + "keyValueName : " + keyValueName
                         + "\n"
                         + "keyValueData : " + keyValueData
                        + "\n"

                         );


                        readStatus = (uint)eXceptionEnum.KEY_VALUE_NAME_DOES_NOT_EXIST;
                    }

                    else
                    {
                        System.Diagnostics.Debug.WriteLine("\n"
                         + "Requested registry keyValueName does not exist."
                         + "\n"
                         + "RegistryKey : " + keyName
                         + "\n"
                         + "keyValueName : " + keyValueName
                         + "\n"
                         + "keyValueData : " + keyValueData
                        + "\n"
                        );

                        keyValueData = string.Empty;
                        readStatus = (uint)eXceptionEnum.QUERIED_DEFAULT_VALUE;
                    }
                }//could also mean the keyValueName is (Default) and keyValueData is (value not set)
                //basically could be a blank entry
                //meaning if i query (Default) valuename it will return <Not Found>

                else
                {

                    System.Diagnostics.Debug.WriteLine("\n"
                    + "Requested registry keyValueName is : "
                    + "\n"
                    + "RegistryKey : " + keyName
                    + "\n"
                    + "keyValueName : " + keyValueName
                    + "\n"
                    + "keyValueData : " + keyValueData
                    + "\n"
                    );


                    readStatus = (uint)eXceptionEnum.QUERY_OR_WRITE_SUCCESSFUL;

                }

            }

            catch (System.Security.SecurityException)
            {
                System.Diagnostics.Debug.WriteLine("\n"
                    + "Requested registry access is not allowed."
                    + "\n"
                    + "RegistryKey : " + keyName
                    + "\n"
                    + "keyValueName : " + keyValueName
                    + "\n"

                    );

                keyValueData = "Permission Denied";
                readStatus = (uint)eXceptionEnum.PERMISSION_DENIED;
            }


            catch (System.NullReferenceException)
            {
                System.Diagnostics.Debug.WriteLine("\n"
                    + "Requested registry key not found."
                    + "\n"
                    + "RegistryKey : " + keyName
                    + "\n"
                    + "keyValueName : " + keyValueName
                    + "\n"

                    );
                keyValueData = "NullReferenceException";
                readStatus = (uint)eXceptionEnum.NULL_REFERENCE_EXCEPTION;
            }


            catch (System.IO.IOException)
            {
                System.Diagnostics.Debug.WriteLine("\n"
                    + "Requested RegistryKey that contains the specified value has been marked for deletion."
                    + "\n"
                    + "RegistryKey : " + keyName
                    + "\n"
                    + "keyValueName : " + keyValueName
                    + "\n"

                    );
                keyValueData = "IOException";
                readStatus = (uint)eXceptionEnum.IOEXCEPTION;
            }

            catch (System.ArgumentException)
            {
                System.Diagnostics.Debug.WriteLine("\n"
                    + "Requested RegistryKey does not begin with a valid registry root."
                    + "\n"
                    + "RegistryKey : " + keyName
                    + "\n"
                    + "keyValueName : " + keyValueName
                    + "\n"

                    );
                keyValueData = "ArgumentException";
                readStatus = (uint)eXceptionEnum.ARGUMENT_EXCEPTION;
            }

            return keyValueData;
        }

        #endregion


        /// <summary>
        /// keyWrite - logs write success to writeStatus [0 =  Success] 
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="keyValueName"></param>
        /// <param name="keyValueType"></param>
        /// <param name="keyValueData"></param>
        /// <param name="writeStatus"></param>
        #region keyWrite

        public void keyWrite(
          string keyName,
          string keyValueName,
          string keyValueType,
          string keyValueData,
          out uint writeStatus)
        {
            writeStatus = uint.MinValue;//ZERO
            string writeThis = string.Empty;
            RegistryValueKind keyValueKind;

            switch (keyValueType)
            {

                case "REG_BINARY":
                    keyValueKind = RegistryValueKind.Binary;
                    break;

                case "REG_DWORD":
                    keyValueKind = RegistryValueKind.DWord;
                    break;

                case "REG_EXPAND_SZ":
                    keyValueKind = RegistryValueKind.ExpandString;
                    break;

                case "REG_MULTI_SZ":
                    keyValueKind = RegistryValueKind.MultiString;
                    break;

                case "REG_QWORD":
                    keyValueKind = RegistryValueKind.QWord;
                    break;

                case "REG_SZ":
                    keyValueKind = RegistryValueKind.String;
                    break;

                case "REG_DWORD_LITTLE_ENDIAN":
                    keyValueKind = RegistryValueKind.DWord;
                    break;

                case "REG_QWORD_LITTLE_ENDIAN":
                    keyValueKind = RegistryValueKind.QWord;
                    break;

                case "REG_DWORD_BIG_ENDIAN":
                    keyValueKind = RegistryValueKind.Unknown;
                    break;

                case "REG_LINK":
                    keyValueKind = RegistryValueKind.Unknown;
                    break;

                case "REG_RESOURCE_LIST":
                    keyValueKind = RegistryValueKind.Unknown;
                    break;

                case "REG_NONE":
                    keyValueKind = RegistryValueKind.Unknown;
                    break;

                default:
                    keyValueKind = RegistryValueKind.String;
                    break;
            }

            try
            {
                Registry.SetValue(
                    keyName,
                    keyValueName,
                    keyValueData,
                    keyValueKind
                    );

                writeStatus = (uint)eXceptionEnum.QUERY_OR_WRITE_SUCCESSFUL;

            }


            catch (System.ArgumentNullException)
            {
                System.Diagnostics.Debug.WriteLine("\n"
                    + " Registry Write Operation Failed "
                    + "\n"
                    + "'keyValueName' is a null reference "
                    + "\n"
                    + "RegistryKey : " + keyName
                    + "\n"
                    + "keyValueName : " + keyValueName
                    + "\n"
                    + "keyValueKind : " + keyValueType
                    + "\n"
                    + "keyValueData : " + keyValueData
                    + "\n"

                    );
                writeStatus = (uint)eXceptionEnum.ARGUMENT_NULL_EXCEPTION;
            }

            catch (System.ArgumentException)
            {
                System.Diagnostics.Debug.WriteLine("\n"
                    + " Registry Write Operation Failed for one of the following reasons : "
                    + "\n"
                    + "'keyValueName' does not begin with a valid registry root "
                    + "\n"
                    + "'keyValueName' is longer than the maximum length allowed (255 characters). "
                    + "\n"
                    + "The type of value did not match the registry data type specified by valueKind, therefore the data could not be converted properly."
                    + "\n"
                    + "RegistryKey : " + keyName
                    + "\n"
                    + "keyValueName : " + keyValueName
                    + "\n"
                    + "keyValueKind : " + keyValueType
                    + "\n"
                    + "keyValueData : " + keyValueData
                    + "\n"

                    );
                writeStatus = (uint)eXceptionEnum.ARGUMENT_EXCEPTION;
            }

            catch (System.UnauthorizedAccessException)
            {
                System.Diagnostics.Debug.WriteLine("\n"
                    + " Registry Write Operation Failed "
                    + "\n"
                    + "The RegistryKey is read-only, and thus cannot be written to; for example, it is a root-level node, or the key has not been opened with write access."
                    + "\n"
                    + "RegistryKey : " + keyName
                    + "\n"
                    + "keyValueName : " + keyValueName
                    + "\n"
                    + "keyValueKind : " + keyValueType
                    + "\n"
                    + "keyValueData : " + keyValueData
                    + "\n"

                    );
                writeStatus = (uint)eXceptionEnum.UNAUTHORIZED_ACCESS_EXCEPTION;
            }

            catch (System.Security.SecurityException)
            {
                System.Diagnostics.Debug.WriteLine("\n"
                    + " Registry Write Operation Failed "
                    + "\n"
                    + "The user does not have the permissions required to create or modify registry keys. "
                    + "\n"
                    + "RegistryKey : " + keyName
                    + "\n"
                    + "keyValueName : " + keyValueName
                    + "\n"
                    + "keyValueKind : " + keyValueType
                    + "\n"
                    + "keyValueData : " + keyValueData
                    + "\n"

                    );
                writeStatus = (uint)eXceptionEnum.SECURITY_EXCEPTION;
            }

            catch (System.NullReferenceException)
            {
                System.Diagnostics.Debug.WriteLine("\n"
                    + " Registry Write Operation Failed "
                    + "\n"
                    + "RegistryKey : " + keyName
                    + "\n"
                    + "keyValueName : " + keyValueName
                    + "\n"
                    + "keyValueKind : " + keyValueType
                    + "\n"
                    + "keyValueData : " + keyValueData
                    + "\n"

                    );
                writeStatus = (uint)eXceptionEnum.NULL_REFERENCE_EXCEPTION;
            }

        }
        #endregion



    }
}
