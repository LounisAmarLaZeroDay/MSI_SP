using LaZeroDayCore.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaZeroDayCore.Config
{
    public class C_Setting_Company
    {
        #region ConfigDatabase ConfigCompany

        public static void SaveConfigCompany()
        {
            try
            {
                C_Setting_ini.Write(nameof(C_Variables.Company_.NAME), C_Variables.Company_.NAME);
                C_Setting_ini.Write(nameof(C_Variables.Company_.ACTIVITY), C_Variables.Company_.ACTIVITY);
                C_Setting_ini.Write(nameof(C_Variables.Company_.CODE), C_Variables.Company_.CODE);
                C_Setting_ini.Write(nameof(C_Variables.Company_.VAT_Reg), C_Variables.Company_.VAT_Reg);
                C_Setting_ini.Write(nameof(C_Variables.Company_.CORP_CAPITAL), C_Variables.Company_.CORP_CAPITAL);
                C_Setting_ini.Write(nameof(C_Variables.Company_.CREATE_DATE), C_Variables.Company_.CREATE_DATE);
                C_Setting_ini.Write(nameof(C_Variables.Company_.ADDRESS), C_Variables.Company_.ADDRESS);
                C_Setting_ini.Write(nameof(C_Variables.Company_.COUNTRY), C_Variables.Company_.COUNTRY);
                C_Setting_ini.Write(nameof(C_Variables.Company_.PHONE), C_Variables.Company_.PHONE);
                C_Setting_ini.Write(nameof(C_Variables.Company_.FAX), C_Variables.Company_.FAX);
                C_Setting_ini.Write(nameof(C_Variables.Company_.WEBSITE), C_Variables.Company_.WEBSITE);
                C_Setting_ini.Write(nameof(C_Variables.Company_.EMAIL), C_Variables.Company_.EMAIL);
                C_Setting_ini.Write(nameof(C_Variables.Company_.OTHER), C_Variables.Company_.OTHER);
            }
            catch (Exception e)
            {
                F_File.LogError(e);
            }
            
        }
        public static void LoadConfigCompany()
        {
            try
            {
                if (C_Setting_ini.Read(nameof(C_Variables.Company_.NAME)) == "") SaveConfigCompany();
            }
            catch (Exception e)
            {
                F_File.LogError(e);
            }
            try
            {
                C_Variables.Company_.NAME = C_Setting_ini.Read(nameof(C_Variables.Company_.NAME));
                C_Variables.Company_.ACTIVITY = C_Setting_ini.Read(nameof(C_Variables.Company_.ACTIVITY));
                C_Variables.Company_.CODE = C_Setting_ini.Read(nameof(C_Variables.Company_.CODE));
                C_Variables.Company_.VAT_Reg = C_Setting_ini.Read(nameof(C_Variables.Company_.VAT_Reg));
                C_Variables.Company_.CORP_CAPITAL = C_Setting_ini.Read(nameof(C_Variables.Company_.CORP_CAPITAL));
                C_Variables.Company_.CREATE_DATE = C_Setting_ini.Read(nameof(C_Variables.Company_.CREATE_DATE));
                C_Variables.Company_.ADDRESS = C_Setting_ini.Read(nameof(C_Variables.Company_.ADDRESS));
                C_Variables.Company_.COUNTRY = C_Setting_ini.Read(nameof(C_Variables.Company_.COUNTRY));
                C_Variables.Company_.PHONE = C_Setting_ini.Read(nameof(C_Variables.Company_.PHONE));
                C_Variables.Company_.FAX = C_Setting_ini.Read(nameof(C_Variables.Company_.FAX));
                C_Variables.Company_.WEBSITE = C_Setting_ini.Read(nameof(C_Variables.Company_.WEBSITE));
                C_Variables.Company_.EMAIL = C_Setting_ini.Read(nameof(C_Variables.Company_.EMAIL));
                C_Variables.Company_.OTHER = C_Setting_ini.Read(nameof(C_Variables.Company_.OTHER));
            }
            catch(Exception e)
            {
                F_File.LogError(e);
            }
    
        }
        #endregion
    }
}
