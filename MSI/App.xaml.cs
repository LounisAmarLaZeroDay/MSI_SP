using MSI.App_Start;
using System;
using System.Windows;

namespace MSI
{
    public partial class App : Application
    {
        [Obsolete]
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }
}
