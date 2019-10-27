using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UserControls.ViewModels
{
    class MainMenuViewModel:Bases.ViewModelBase
    {
        public MainMenuViewModel()
        {
            initDelegateCommand();
        }
        #region command
        void initDelegateCommand()
        {
            commandNavigate = new DelegateCommand<string>(UserControlsModule.NavigateContent);
        }
        public DelegateCommand<string> commandNavigate { get; set; }

        #endregion
    }
}
