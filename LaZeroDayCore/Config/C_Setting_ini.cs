using LaZeroDayCore.Controller;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace LaZeroDayCore.Config
{
    public class C_Setting_ini
    {
        private static string Path = new FileInfo(C_Variables.Path_.dir_setting+"setting.ini").FullName.ToString();
        private static string section = "setting";
        //
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);
        public static string Read(string Key)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(section, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }
        public static void Write(string Key, string Value)
        {
            WritePrivateProfileString(section, Key, Value, Path);
        }
        //
        public static int ReadInt(string Key)
        {
            try
            {
                return Convert.ToInt32(Read(Key));
            }
            catch (Exception e)
            {
                F_File.LogError(e);
                return 0;
            }
        }
    }
}
