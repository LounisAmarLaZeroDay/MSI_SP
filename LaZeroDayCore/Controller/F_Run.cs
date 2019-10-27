using LaZeroDayCore.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace LaZeroDayCore.Controller
{
    public class F_Run
    {
        public static void Exit()
        {
            try
            {
                 //System.Windows.Forms.Application.Exit();
            }
            catch (Exception e)
            {
                F_File.LogError(e);
            }
            try
            {
                System.Environment.Exit(1);
            }
            catch (Exception e)
            {
                F_File.LogError(e);
            }
        }
    }
}
