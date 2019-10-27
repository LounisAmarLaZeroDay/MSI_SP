using System;
using System.Windows;

namespace LaZeroDayCore.Controller
{
    public class DialogInformation
    {
        #region Information
        public static void Information(String p_String)
        {
            MessageBox.Show(p_String, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public static void OK() { Information("Ok"); }
        public static void Activation() { Information("think for activet this software"); }
        #endregion
    }
}
