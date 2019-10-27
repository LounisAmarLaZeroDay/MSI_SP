using System;
using System.IO;

namespace LaZeroDayCore.Config
{
    public class C_Variables
    {
        public static class Path_
        {
            public static String dir_home = Path.Combine(Directory.GetCurrentDirectory(), @"SOLUTION_PRO\");
            public static String dir_backup = Path.Combine(dir_home, @"backup\");
            public static String dir_Invoice = Path.Combine(dir_home, @"Invoice\");
            public static String dir_db = Path.Combine(dir_home, @"db\");
            public static String dir_setting = Path.Combine(dir_home, @"setting\");
        }
        public static class DB_
        {
            public static int type = 0; // 0 or 1
            public static String Charset = "UTF8";
            public static String host = "localhost";
            public static String UserID = "SYSDBA";
            public static String Password = "masterkey";
            public static int Port = 3050;
            public static String file = Path.Combine(Path_.dir_db, @"MSI.FDB");
        }
        public static class Company_
        {
            public static String NAME = "Company";
            public static String ACTIVITY = "";
            public static String CREATE_DATE = "";
            public static String ADDRESS = "";
            public static String COUNTRY = "";
            public static String PHONE = "";
            public static String FAX = "";
            public static String WEBSITE = "";
            public static String EMAIL = "";
            public static String CODE = "";
            public static String VAT_Reg = "";
            public static String CORP_CAPITAL = "";
            public static String OTHER = "";
        }
        public static class Software_
        {
            public static String activation_hash_add = "solution_pro_inventory_management_system";
            public static String default_access = "0000000000000000000000000000000000000000";
            /************/
            public static String language = "EN";
            public static String currency = "DA";
            /************/
            public static int pageSizeSearch = 10;
        }
    }
}
