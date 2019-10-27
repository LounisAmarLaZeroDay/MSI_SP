using LaZeroDayCore.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LaZeroDayCore.Controller
{
    public class DialogAlert
    {
        public static void exitYasNo()
        {
            var dr = MessageBox.Show("Do you want to close the program?", "Exit", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (dr == MessageBoxResult.OK)
            {
                F_Run.Exit();
            }
        }
        public static MessageBoxResult Delete()
        {
            return MessageBox.Show("are you sure you want delete this !", "delete", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
        }
    }
}
