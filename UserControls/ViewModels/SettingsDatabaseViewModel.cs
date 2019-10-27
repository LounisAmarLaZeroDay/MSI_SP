using Data.Model;
using Data.Utils;
using LaZeroDayCore.Config;
using LaZeroDayCore.Controller;
using Prism.Commands;
using System;
using System.Collections.Generic;

namespace UserControls.ViewModels
{
    class SettingsDatabaseViewModel:Bases.ViewModelBase
    {
        public SettingsDatabaseViewModel()
        {
            initDelegateCommand();
            initComboBox();
        }
        //****************************************************************************************************** command
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
                case "DatabasePath":
                    {
                        TB_DatabasePath = F_File.browserFile("Database | *.FDB;");
                        C_Variables.DB_.file = TB_DatabasePath;
                    }
                    break;
                case "Save":
                    {
                        C_Setting_DB.SaveConfigDatabase();
                    }
                    break;
                case "Test":
                    {
                        if (T_Helper.IsNull()) DialogError.Error();
                        else DialogInformation.OK();
                    }
                    break;
                case "Shutdown":
                    {
                        if (DB_Access.GetInstatce().Shutdown()) DialogInformation.OK();
                        else DialogError.Error();
                    }
                    break;
                case "Backup":
                    {
                        if (DB_Access.GetInstatce().Backup() == "") DialogError.Error();
                        else DialogInformation.OK();
                    }
                    break;
                case "Restore":
                    {
                        string p = F_File.browserFile("Database | *.FBK;");
                        if (p == "") { DialogError.Error(); break; }
                        if (DB_Access.GetInstatce().Restore(p)) DialogInformation.OK();
                        else DialogError.Error();
                    }
                    break;
                case "CreateNew":
                    {
                        if (DB_Access.GetInstatce().CreateNew()) DialogInformation.OK();
                        else DialogError.Error();
                    }
                    break;
                case "Default":
                    {
                        C_Setting_DB.set_db_Default();
                        TB_DatabaseDataSource = C_Variables.DB_.host;
                        TB_DatabasePath = C_Variables.DB_.file;
                        TB_DatabasePort = C_Variables.DB_.Port;
                        TB_DatabaseUserID = C_Variables.DB_.UserID;
                        TB_DatabasePassword = C_Variables.DB_.Password;
                        ComboBoxSelected = C_Variables.DB_.type == 0 ? "Default" : "Embedded";
                    }
                    break;
                case "Embedded":
                    {
                        C_Setting_DB.set_db_Embedded();
                        TB_DatabaseDataSource = C_Variables.DB_.host;
                        TB_DatabasePath = C_Variables.DB_.file;
                        TB_DatabasePort = C_Variables.DB_.Port;
                        TB_DatabaseUserID = C_Variables.DB_.UserID;
                        TB_DatabasePassword = C_Variables.DB_.Password;
                        ComboBoxSelected = C_Variables.DB_.type == 0 ? "Default" : "Embedded";
                    }
                    break;
                default:break;
            }
        }
        #endregion
        //****************************************************************************************************** ConnectionType
        #region ConnectionType
        public class ConnectionTypeClass { public string Display { get; set; } public string Value { get; set; } }
        public void initComboBox()
        {
            string[] names = { "Default", "Embedded" };
            ComboBoxSource = new List<ConnectionTypeClass>();
            for (int i = 0; i < names.Length; i++)
            {
                ComboBoxSource.Add(new ConnectionTypeClass { Display = names[i], Value = names[i] });
            }
        }

        private string _ComboBoxSelected;
        public string ComboBoxSelected
        {
            get { return _ComboBoxSelected = C_Variables.DB_.type==0? "Default": "Embedded"; }
            set
            {
                SetProperty(ref _ComboBoxSelected, value);
                if (_ComboBoxSelected.Equals("Default")) C_Variables.DB_.type = 0;
                else C_Variables.DB_.type = 1;
            }
        }

        private List<ConnectionTypeClass> _ComboBoxSource;
        public List<ConnectionTypeClass> ComboBoxSource
        {
            get { return _ComboBoxSource; }
            set
            {
                SetProperty(ref _ComboBoxSource, value);
            }
        }
        #endregion
        //****************************************************************************************************** proprity
        #region Property
        private string _TB_DatabasePath = C_Variables.DB_.file;
        public string TB_DatabasePath
        {
            get { return _TB_DatabasePath ?? ""; }
            set
            {
                SetProperty(ref _TB_DatabasePath, value);
            }
        }
        private string _TB_DatabaseDataSource = C_Variables.DB_.host;
        public string TB_DatabaseDataSource
        {
            get { return _TB_DatabaseDataSource; }
            set
            {
                SetProperty(ref _TB_DatabaseDataSource, value);
            }
        }
        private int _TB_DatabasePort = C_Variables.DB_.Port;
        public int TB_DatabasePort
        {
            get { return _TB_DatabasePort; }
            set
            {
                SetProperty(ref _TB_DatabasePort, value);
            }
        }
        private string _TB_DatabaseUserID = C_Variables.DB_.UserID;
        public string TB_DatabaseUserID
        {
            get { return _TB_DatabaseUserID; }
            set
            {
                SetProperty(ref _TB_DatabaseUserID, value);
            }
        }
        private string _TB_DatabasePassword = C_Variables.DB_.Password;
        public string TB_DatabasePassword
        {
            get { return _TB_DatabasePassword; }
            set
            {
                SetProperty(ref _TB_DatabasePassword, value);
            }
        }
        #endregion
    }
}
