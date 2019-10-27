using FirebirdSql.Data.FirebirdClient;
using LaZeroDayCore.Controller;
using System;

namespace LaZeroDayCore.Config
{
    public class C_Setting
    {
        #region save load
        public static void Run()
        {
            createDir();
            C_Setting_Software.LoadConfigSoftware();
            C_Setting_DB.LoadConfigDatabase();
            C_Setting_Company.LoadConfigCompany();
        }
        public static void createDir()
        {
            try
            {
                string[] array_dir = { C_Variables.Path_.dir_home, C_Variables.Path_.dir_backup, C_Variables.Path_.dir_db, C_Variables.Path_.dir_Invoice, C_Variables.Path_.dir_setting };
                foreach (string dir in array_dir)
                {
                    if (!System.IO.Directory.Exists(dir))
                    {
                        System.IO.Directory.CreateDirectory(dir);
                    }
                }
            }
            catch(Exception e)
            {
                F_File.LogError(e);
            }
        }
        #endregion
    }
}
