using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LaZeroDayCore.Controller
{
    public class DialogError
    {
        #region error
        public static void Error(String p_String)
        {
            MessageBox.Show(p_String, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static void Error() { Error("error"); }
        public static void Activation() { Error("error code\n conetact devloper \n +213551635360"); }
        #endregion
    }
}
