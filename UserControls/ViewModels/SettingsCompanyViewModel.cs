using LaZeroDayCore.Config;
using LaZeroDayCore.Controller;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;


namespace UserControls.ViewModels
{
    class SettingsCompanyViewModel : Bases.ViewModelBase
    {
        public SettingsCompanyViewModel()
        {
            initInput();
            initDelegateCommand();
        }
        //************************************************************************************* command
        #region initDelegateCommand
        void initDelegateCommand()
        {
            commandExecute = new DelegateCommand<Object>(execute);
        }
        #endregion
        #region commandExecute
        public DelegateCommand<Object> commandExecute { get; set; }
        private void execute<Object>(Object obj)
        {
            string st = obj as string;
            switch (st)
            {
                case "EditImage":
                    {
                        try
                        {
                            var path = F_File.browserFile("image | *.png;*.jpg;");
                            if (path != "")
                            {
                                PICTURE = new byte[] { };
                            }
                        }
                        catch (Exception e)
                        {
                            F_File.LogError(e);
                        }
                    }
                    break;
                case "DeleteImage":
                    {
                        PICTURE = new byte[] { };
                    }
                    break;
                case "Save":
                    {
                        SaveInput();
                        DialogInformation.OK();
                    }
                    break;
                default: break;
            }
        }
        #endregion
        //*************************************************************************************
        #region function
        public void initInput()
        {
            C_Setting_Company.LoadConfigCompany();
            //
            PICTURE = F_Image.PictureCompanyGet();
            TB_NAME = C_Variables.Company_.NAME;
            TB_ACTIVITY = C_Variables.Company_.ACTIVITY;
            TB_CODE = C_Variables.Company_.CODE;
            TB_VAT_Reg = C_Variables.Company_.VAT_Reg;
            TB_CORP_CAPITAL = C_Variables.Company_.CORP_CAPITAL;
            TB_CREATE_DATE = C_Variables.Company_.CREATE_DATE;
            TB_ADDRESS = C_Variables.Company_.ADDRESS;
            TB_COUNTRY = C_Variables.Company_.COUNTRY;
            TB_PHONE = C_Variables.Company_.PHONE;
            TB_FAX = C_Variables.Company_.FAX;
            TB_WEBSITE = C_Variables.Company_.WEBSITE;
            TB_EMAIL = C_Variables.Company_.EMAIL;
            TB_OTHER = C_Variables.Company_.OTHER;
        }
        public void SaveInput()
        {
            F_Image.PictureCompanySet(PICTURE);
            C_Variables.Company_.NAME = TB_NAME;
            C_Variables.Company_.ACTIVITY = TB_ACTIVITY;
            C_Variables.Company_.CODE = TB_CODE;
            C_Variables.Company_.VAT_Reg = TB_VAT_Reg;
            C_Variables.Company_.CORP_CAPITAL = TB_CORP_CAPITAL;
            C_Variables.Company_.CREATE_DATE = TB_CREATE_DATE;
            C_Variables.Company_.ADDRESS = TB_ADDRESS;
            C_Variables.Company_.COUNTRY = TB_COUNTRY;
            C_Variables.Company_.PHONE = TB_PHONE;
            C_Variables.Company_.FAX = TB_FAX;
            C_Variables.Company_.WEBSITE = TB_WEBSITE;
            C_Variables.Company_.EMAIL = TB_EMAIL;
            C_Variables.Company_.OTHER = TB_OTHER;
            //
            C_Setting_Company.SaveConfigCompany();
        }
        #endregion
        //************************************************************************************* Proprity
        #region Proprity
        private byte[] _PICTURE;
        public byte[] PICTURE
        {
            get { return _PICTURE ?? new byte[] { }; }
            set { SetProperty(ref _PICTURE, value); }
        }
        private string _TB_NAME;
        public string TB_NAME
        {
            get { return _TB_NAME ?? ""; }
            set { SetProperty(ref _TB_NAME, value); }
        }
        //
        private string _TB_ACTIVITY;
        public string TB_ACTIVITY
        {
            get { return _TB_ACTIVITY ?? ""; }
            set { SetProperty(ref _TB_ACTIVITY, value); }
        }
        //
        private string _TB_CODE;
        public string TB_CODE
        {
            get { return _TB_CODE ?? ""; }
            set { SetProperty(ref _TB_CODE, value); }
        }
        //
        private string _TB_VAT_Reg;
        public string TB_VAT_Reg
        {
            get { return _TB_VAT_Reg ?? ""; }
            set { SetProperty(ref _TB_VAT_Reg, value); }
        }
        //
        private string _TB_CORP_CAPITAL;
        public string TB_CORP_CAPITAL
        {
            get { return _TB_CORP_CAPITAL ?? ""; }
            set { SetProperty(ref _TB_CORP_CAPITAL, value); }
        }
        //
        private string _TB_CREATE_DATE;
        public string TB_CREATE_DATE
        {
            get { return _TB_CREATE_DATE ?? ""; }
            set { SetProperty(ref _TB_CREATE_DATE, value); }
        }
        //
        private string _TB_ADDRESS;
        public string TB_ADDRESS
        {
            get { return _TB_ADDRESS ?? ""; }
            set { SetProperty(ref _TB_ADDRESS, value); }
        }
        //
        private string _TB_COUNTRY;
        public string TB_COUNTRY
        {
            get { return _TB_COUNTRY ?? ""; }
            set { SetProperty(ref _TB_COUNTRY, value); }
        }
        //
        private string _TB_PHONE;
        public string TB_PHONE
        {
            get { return _TB_PHONE ?? ""; }
            set { SetProperty(ref _TB_PHONE, value); }
        }
        //
        private string _TB_FAX;
        public string TB_FAX
        {
            get { return _TB_FAX ?? ""; }
            set { SetProperty(ref _TB_FAX, value); }
        }
        //
        private string _TB_WEBSITE;
        public string TB_WEBSITE
        {
            get { return _TB_WEBSITE ?? ""; }
            set { SetProperty(ref _TB_WEBSITE, value); }
        }
        // 
        private string _TB_EMAIL;
        public string TB_EMAIL
        {
            get { return _TB_EMAIL ?? ""; }
            set { SetProperty(ref _TB_EMAIL, value); }
        }
        // 
        private string _TB_OTHER;
        public string TB_OTHER
        {
            get { return _TB_OTHER ?? ""; }
            set { SetProperty(ref _TB_OTHER, value); }
        }
        #endregion
    }
}
