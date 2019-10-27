using Data.Model;
using Data.Utils;
using LaZeroDayCore.Config;
using LaZeroDayCore.Controller;
using MahApps.Metro.Controls;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace UserControls.ViewModels
{
    class TableUsersEditViewModel : Bases.ViewModelBase
    {
        public TableUsersEditViewModel()
        {
            initDelegateCommand();
            initEventHandler();
        }
        //************************************************************************************* Messanger Begin
        #region Messanger
        public class EventHandlerClass
        {
            public event EventHandler mEventHandler;
            public void Send(object p_message) { mEventHandler?.Invoke(p_message, new EventArgs()); }
        }
        private static List<EventHandlerClass> mListEventHandlerClass;
        public void initEventHandler()
        {
            mListEventHandlerClass = new List<EventHandlerClass>();
            mListEventHandlerClass.Insert(0, new EventHandlerClass());
            mListEventHandlerClass[0].mEventHandler += delegate (object p_message, EventArgs e) { Receiver(p_message); };
        }
        public static void Send(object p_message)
        {
            foreach (var v in mListEventHandlerClass) v.Send(p_message);
        }
        //
        public void Receiver(object p_message)
        {
            if (p_message != null)
            {
                var Selected = (p_message as USER);
                InitInput(Selected);
            }
            else
            {
                InitInput(new USER()
                {
                    ID = 0
                });
            }
        }
        #endregion
        //************************************************************************************* command
        #region commandExecute
        void initDelegateCommand()
        {
            commandExecute = new DelegateCommand<Object>(execute);
            commandPasswordChanged = new DelegateCommand<Object>(executePasswordChanged);
            commandCPasswordChanged = new DelegateCommand<Object>(executeCPasswordChanged);
        }
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
                            var bitmapImage = new BitmapImage(new Uri(path));
                            PICTURE_USER = F_Image.BitmapImage2Bytes(bitmapImage);
                        }
                        catch (Exception e)
                        {
                            F_File.LogError(e);
                        }
                    }
                    break;
                case "DeleteImage":
                    {
                        PICTURE_USER = new byte[] { };
                    }
                    break;
                case "Save":
                    {
                        if (PASSWORD == PASSWORD_CONFIRMATION )
                        {
                            T_Helper.user.EditFromObject(getInput());
                            TableUsersViewModel.Send(null);
                            HeaderViewModel.Send(null);
                        }
                        else DialogError.Error();
                    }
                    break;
                case "OverlayGridCancel":
                    {
                        TableUsersViewModel.Send(null);
                    }
                    break;
                default: break;
            }
        }
        #endregion
        //************************************************************************************* password
        #region commandPasswordChanged
        public DelegateCommand<Object> commandPasswordChanged { get; set; }
        private void executePasswordChanged(Object o)
        {
            PASSWORD = (o as PasswordBox).Password;
        }
        #endregion
        #region commandConfirmPasswordChanged
        public DelegateCommand<Object> commandCPasswordChanged { get; set; }
        private void executeCPasswordChanged(Object o)
        {
            PASSWORD_CONFIRMATION = (o as PasswordBox).Password;
        }
        #endregion
        //************************************************************************************* in out
        #region in out
        void InitInput(USER p_table)
        {
            ID = p_table.ID;
            NAME = p_table.NAME;
            DESCRIPTION = p_table.DESCRIPTION;
            CODE = p_table.CODE;
            TYPE = p_table.TYPE;
            FIRSTNAME = p_table.FIRSTNAME;
            LASTNAME = p_table.LASTNAME;
            GENDER = p_table.GENDER;
            BIRTHDAY = p_table.BIRTHDAY ?? DateTime.Now;
            ADDRESS = p_table.ADDRESS;
            CITY = p_table.CITY;
            COUNTRY = p_table.COUNTRY;
            PHONE = p_table.PHONE;
            FAX = p_table.FAX;
            WEBSITE = p_table.WEBSITE;
            EMAIL = p_table.EMAIL;
            PASSWORD = p_table.PASSWORD;
            PASSWORD_CONFIRMATION = p_table.PASSWORD;
            MONEY_ACCOUNT = p_table.MONEY_ACCOUNT;
            PICTURE_USER = p_table.PICTURE;
        }
        USER getInput()
        {
            var r = new USER()
            {
                ID = ID,
                NAME = NAME,
                DESCRIPTION = DESCRIPTION,
                CODE = CODE,
                FIRSTNAME = FIRSTNAME,
                LASTNAME = LASTNAME,
                GENDER = GENDER,
                BIRTHDAY = BIRTHDAY,
                ADDRESS = ADDRESS,
                CITY = CITY,
                COUNTRY = COUNTRY,
                PHONE = PHONE,
                FAX = FAX,
                WEBSITE = WEBSITE,
                EMAIL = EMAIL,
                MONEY_ACCOUNT = MONEY_ACCOUNT,
                PASSWORD = PASSWORD,
                PICTURE = PICTURE_USER,
                TYPE = TYPE
            };
            return r;
        }
        #endregion
    }
}