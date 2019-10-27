using System.Windows;
using Prism.Modularity;
using Prism.Unity;
using Unity;
using UserControls.Views;

namespace MSI.App_Start
{
    [System.Obsolete]
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Views.Shell>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterType(typeof(object), typeof(TableUsers), "TableUsers");
            Container.RegisterType(typeof(object), typeof(TableProducts), "TableProducts");
            Container.RegisterType(typeof(object), typeof(TableCustomers), "TableCustomers");
            Container.RegisterType(typeof(object), typeof(TableWholesaler), "TableWholesaler");
            Container.RegisterType(typeof(object), typeof(TableInvoiceSell), "TableInvoiceSell");
            Container.RegisterType(typeof(object), typeof(TableInvoicePurchase), "TableInvoicePurchase");

            Container.RegisterType(typeof(object), typeof(InvoiceSell), "InvoiceSell"); 
            Container.RegisterType(typeof(object), typeof(InvoicePurchase), "InvoicePurchase");

            Container.RegisterType(typeof(object), typeof(Login), "Login");
            Container.RegisterType(typeof(object), typeof(Settings), "Settings");
            Container.RegisterType(typeof(object), typeof(Footer), "Footer");
            Container.RegisterType(typeof(object), typeof(Header), "Header");
            Container.RegisterType(typeof(object), typeof(MainMenu), "MainMenu");
        }
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            base.ModuleCatalog.AddModule<UserControls.UserControlsModule>();
        }
    }
}