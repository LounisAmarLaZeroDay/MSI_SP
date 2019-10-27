using Data.Model;
using Data.Utils;
using LaZeroDayCore.Controller;
using MahApps.Metro.Controls;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using UserControls.ViewModels.Bases;

namespace UserControls.ViewModels
{
    class TableCustomersEditViewModel : Bases.ViewModelBase
    {
        public TableCustomersEditViewModel()
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
                var Selected = (p_message as CUSTOMER);
                InitInput(Selected);
            }
            else
            {
                InitInput(new CUSTOMER()
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
                            PICTURE_CUSTOMER = F_Image.BitmapImage2Bytes(bitmapImage);
                        }
                        catch (Exception e)
                        {
                            F_File.LogError(e);
                        }
                    }
                    break;
                case "DeleteImage":
                    {
                        PICTURE_CUSTOMER = new byte[] { };
                    }
                    break;
                case "Save":
                    {
                        T_Helper.customer.EditFromObject(getInput());
                        TableCustomersViewModel.Send(null);;
                    }
                    break;
                default: break;
            }
        }
        #endregion
        //************************************************************************************* in out
        #region in out
        void InitInput(CUSTOMER p_table)
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
            MONEY_ACCOUNT = p_table.MONEY_ACCOUNT;
            PICTURE_CUSTOMER = p_table.PICTURE;
        }
        CUSTOMER getInput()
        {
            var r = new CUSTOMER()
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
                PICTURE = PICTURE_CUSTOMER,
                TYPE = TYPE
            };
            return r;
        }
        #endregion
    }
}
