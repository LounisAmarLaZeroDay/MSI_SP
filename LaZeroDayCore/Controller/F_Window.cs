using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LaZeroDayCore.Controller
{
    public class F_Window
    {
        public static MetroWindow newMetroWindow(Object p_content, int p_width, int p_height)
        {
            MetroWindow window = new MetroWindow
            {
                ShowTitleBar = false,
                Width = p_width,
                Height = p_height,
                Content = p_content
            };

            Application curApp = Application.Current;
            Window mainWindow = curApp.MainWindow;
            int p_Left = (int)(mainWindow.Left + (mainWindow.Width - window.Width) / 2);
            int p_Top = (int)(mainWindow.Top + (mainWindow.Height - window.Height) / 2);

            window.Top = p_Top;
            window.Left = p_Left;
            return window;
        }
    }
}
