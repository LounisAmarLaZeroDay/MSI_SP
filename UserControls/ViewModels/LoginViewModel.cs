using Data.Model;
using Data.Utils;
using LaZeroDayCore.Controller;
using Prism.Commands;
using System;
using System.Windows.Controls;

namespace UserControls.ViewModels
{
    class LoginViewModel : Bases.ViewModelBase
    {
        public LoginViewModel()
        {
            initDelegateCommand();
            NAME = "admin";
        }
        //*************************************************************************************
        #region initDelegateCommand
        void initDelegateCommand()
        {
            commandLogin = new DelegateCommand(executeLogin, CanExecuteLogin);
            commandPasswordChanged = new DelegateCommand<Object>(executePasswordChanged);
        }
        #endregion
        //*************************************************************************************
        #region commandLogin
        public DelegateCommand commandLogin { get; set; }
        private bool CanExecuteLogin() { return true; }
        private void executeLogin()
        {
            if (T_Helper.IsNull()) UserControlsModule.NavigateContent("Settings");

            if (T_Helper.user.login(NAME,PASSWORD))
            {
                UserControlsModule.NavigateContent("MainMenu");
            }
            else
            {
                DialogError.Error();
            }
            HeaderViewModel.Send("login");
        }
        #endregion
        //*************************************************************************************
        #region commandPasswordChanged
        public DelegateCommand<Object> commandPasswordChanged { get; set; }
        private void executePasswordChanged(Object o)
        {
            PASSWORD = (o as PasswordBox).Password;
        }
        #endregion
        //*************************************************************************************
    }
    

}
