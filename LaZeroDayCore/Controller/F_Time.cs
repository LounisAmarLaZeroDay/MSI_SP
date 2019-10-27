using System;
using System.Globalization;

namespace LaZeroDayCore.Controller
{
    public class F_Time
    {
        public static String DateTime2String_US(DateTime p_DateTime)
        {
            return p_DateTime.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }
        public static String DateTime2String_yyyy_MM_dd_HH_mm_ss(DateTime p_DateTime)
        {
            return p_DateTime.ToString("yyyy/MM/dd  HH:mm:ss", CultureInfo.InvariantCulture);
        }
        public static String DateTime2String_File_yyyy_MM_dd_HH_mm_ss(DateTime p_DateTime)
        {
            return p_DateTime.ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture);
        }
        public static DateTime DateTime_From_String_US(String p_DateTime)
        {
            return DateTime.Parse(p_DateTime, new CultureInfo("en-US"));
        }
        public static DateTime DateTime_From_String_FR(String p_DateTime)
        {
            return DateTime.Parse(p_DateTime, new CultureInfo("fr-FR"));
        }
    }
}
