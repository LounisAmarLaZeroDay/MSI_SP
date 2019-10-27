using LaZeroDayCore.Config;
using System;
using System.Management;

namespace LaZeroDayCore.Controller
{
    class ActivationSoftware
    {
        ///////////////////////////////////////////////////////////////////////////////////
        #region ID
        public static string get_CPU_ID()
        {
            try
            {
                string cpuInfo = "";
                ManagementClass managClass = new ManagementClass("win32_processor");
                ManagementObjectCollection managCollec = managClass.GetInstances();
                foreach (ManagementObject managObj in managCollec)
                {
                    if (cpuInfo == "")
                    {
                        cpuInfo = managObj.Properties["processorID"].Value.ToString();
                        break;
                    }
                }
                return cpuInfo;
            }
            catch (Exception e) { F_File.LogError(e); return ""; }
        }
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////
        #region activation
        public static String ComputeActivation()
        {
            return HASH.Base64(get_CPU_ID() + C_Variables.Software_.activation_hash_add,HASH.Algorithms.MD5);
        }
        public static Boolean IsActivated()
        {
            if (C_Setting_ini.Read("key_ActivationSoftware").Equals(ComputeActivation())) return true;
            C_Setting_ini.Write("key_ActivationSoftware", "");
            return false;
        }
        public static Boolean IsActivated(String p_code)
        {
            if (ComputeActivation().Equals(p_code))
            {
                C_Setting_ini.Write("key_ActivationSoftware", p_code);
                return true;
            }
            else
            {
                C_Setting_ini.Write("key_ActivationSoftware", "");
                return false;
            }
        }
        #endregion
    }
}
