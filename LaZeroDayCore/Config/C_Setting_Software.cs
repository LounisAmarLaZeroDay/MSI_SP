using LaZeroDayCore.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaZeroDayCore.Config
{
    public class C_Setting_Software
    {
        public static void SaveConfigSoftware()
        {
            try
            {
                C_Setting_ini.Write(nameof(C_Variables.Path_.dir_home), C_Variables.Path_.dir_home);
                C_Setting_ini.Write(nameof(C_Variables.Software_.language), C_Variables.Software_.language);
                C_Setting_ini.Write(nameof(C_Variables.Software_.currency), C_Variables.Software_.currency);
            }
            catch (Exception e)
            {
                F_File.LogError(e);
            }
           
        }

        public static void LoadConfigSoftware()
        {
            try
            {
                if (C_Setting_ini.Read(nameof(C_Variables.Software_.language)) == "") SaveConfigSoftware();
            }
            catch (Exception e)
            {
                SaveConfigSoftware();
                F_File.LogError(e);
            }
            try
            {
                if (C_Setting_ini.Read(nameof(C_Variables.DB_.type)) == "")
                {
                    SaveConfigSoftware();
                }
                C_Variables.Path_.dir_home = C_Setting_ini.Read(nameof(C_Variables.Path_.dir_home));
                C_Variables.Software_.language = C_Setting_ini.Read(nameof(C_Variables.Software_.language));
                C_Variables.Software_.currency = C_Setting_ini.Read(nameof(C_Variables.Software_.currency));
            }
            catch (Exception e)
            {
                F_File.LogError(e);
            }
            
        }
    }
}
