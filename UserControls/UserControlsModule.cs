using LaZeroDayCore.Config;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using UserControls.Views;

namespace UserControls
{
    public class UserControlsModule : IModule
    {
        private static IRegionManager regionManager;
        public void OnInitialized(IContainerProvider containerProvider)
        {
            C_Setting.Run();
            //init RegionManager
            regionManager = containerProvider.Resolve<IRegionManager>();
            //Header
            regionManager.RegisterViewWithRegion("r_header", typeof(Header));
            //Footer
            regionManager.RegisterViewWithRegion("r_footer", typeof(Footer));
            //default main region
            regionManager.RegisterViewWithRegion("r_content", typeof(Login));
        }
        public static void NavigateContent(string p_url)
        {
            regionManager.RequestNavigate("r_content", p_url);
        }
        //public static void Navigate(string p_region, string p_url)
        //{
        //    regionManager.RequestNavigate(p_region, p_url);
        //}
        //public static void Navigate(string p_region, System.Type p_Type)
        //{
        //    regionManager.RegisterViewWithRegion(p_region, p_Type);
        //}
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

    }
}