using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.ViewModels
{
    class SettingsSoftwareViewModel : Bases.ViewModelBase
    {

        //************************************************************************************* ComboBoxPageSize
        #region ComboBoxPageSize
        public class C_PageSize { public string Display { get; set; } public int Value { get; set; } }
        public void initComboBoxPageSize()
        {
            ComboBoxPageSizeItemsSource = new List<C_PageSize>()
            {
                new C_PageSize { Display = "05" ,Value = 05 },
                new C_PageSize { Display = "10" ,Value = 10 },
                new C_PageSize { Display = "20", Value = 20 },
                new C_PageSize { Display = "30", Value = 30 },
                new C_PageSize { Display = "40", Value = 40 },
                new C_PageSize { Display = "50", Value = 50 }
            };
            ComboBoxPageSizeSelected = 05;
        }
        private int _ComboBoxPageSizeSelected;
        public int ComboBoxPageSizeSelected
        {
            get { return _ComboBoxPageSizeSelected; }
            set
            {
                SetProperty(ref _ComboBoxPageSizeSelected, value);
            }
        }
        private IList<C_PageSize> _ComboBoxPageSizeItemsSource;
        public IList<C_PageSize> ComboBoxPageSizeItemsSource
        {
            get { return _ComboBoxPageSizeItemsSource; }
            set
            {
                SetProperty(ref _ComboBoxPageSizeItemsSource, value);
            }
        }
        #endregion

    }
}
