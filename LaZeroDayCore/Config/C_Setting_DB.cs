using FirebirdSql.Data.FirebirdClient;
using LaZeroDayCore.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaZeroDayCore.Config
{
    public class C_Setting_DB
    {
        #region url_database
        public static void set_db_Default()
        {
            C_Variables.DB_.type = 0;
            C_Variables.DB_.Charset = "UTF8";
            C_Variables.DB_.host = "localhost";
            C_Variables.DB_.Port = 3050;
            C_Variables.DB_.UserID = "SYSDBA";
            C_Variables.DB_.Password = "masterkey";
        }
        public static void set_db_Embedded()
        {
            C_Variables.DB_.type = 1;
            C_Variables.DB_.Charset = "UTF8";
            C_Variables.DB_.host = "localhost";
            C_Variables.DB_.Port = 3050;
            C_Variables.DB_.UserID = "sysdba";
            C_Variables.DB_.Password = "masterkey";

        }
        public static String get_db_url()
        {
            LoadConfigDatabase();
            FbConnectionStringBuilder mFBcsb = new FbConnectionStringBuilder();
            mFBcsb.ServerType = C_Variables.DB_.type == 0 ? FbServerType.Default : FbServerType.Embedded;
            mFBcsb.Charset = C_Variables.DB_.Charset;
            mFBcsb.Database = C_Variables.DB_.file;
            mFBcsb.DataSource = C_Variables.DB_.host;
            mFBcsb.Port = C_Variables.DB_.Port;
            mFBcsb.UserID = C_Variables.DB_.UserID;
            mFBcsb.Password = C_Variables.DB_.Password;
            return mFBcsb.ToString();
        }
        #endregion
        public static void SaveConfigDatabase()
        {
            try
            {
                C_Setting_ini.Write(nameof(C_Variables.DB_.type), C_Variables.DB_.type + "");
                C_Setting_ini.Write(nameof(C_Variables.DB_.Charset), C_Variables.DB_.Charset);
                C_Setting_ini.Write(nameof(C_Variables.DB_.file), C_Variables.DB_.file);
                C_Setting_ini.Write(nameof(C_Variables.DB_.host), C_Variables.DB_.host);
                C_Setting_ini.Write(nameof(C_Variables.DB_.Port), C_Variables.DB_.Port + "");
                C_Setting_ini.Write(nameof(C_Variables.DB_.UserID), C_Variables.DB_.UserID);
                C_Setting_ini.Write(nameof(C_Variables.DB_.Password), C_Variables.DB_.Password);
            }
            catch(Exception e)
            {
                F_File.LogError(e);
            }
        }
        public static void LoadConfigDatabase()
        {
            try
            {
                if(C_Setting_ini.Read(nameof(C_Variables.DB_.type))=="") SaveConfigDatabase();
            }
            catch (Exception e)
            {
                F_File.LogError(e);
            }
            try
            {
                C_Variables.DB_.type = C_Setting_ini.ReadInt(nameof(C_Variables.DB_.type));
                C_Variables.DB_.Charset = C_Setting_ini.Read(nameof(C_Variables.DB_.Charset));
                C_Variables.DB_.file = C_Setting_ini.Read(nameof(C_Variables.DB_.file));
                C_Variables.DB_.host = C_Setting_ini.Read(nameof(C_Variables.DB_.host));
                C_Variables.DB_.Port = C_Setting_ini.ReadInt(nameof(C_Variables.DB_.Port));
                C_Variables.DB_.UserID = C_Setting_ini.Read(nameof(C_Variables.DB_.UserID));
                C_Variables.DB_.Password = C_Setting_ini.Read(nameof(C_Variables.DB_.Password));
            }
            catch(Exception e)
            {
                F_File.LogError(e);
            }
}
    }
}
