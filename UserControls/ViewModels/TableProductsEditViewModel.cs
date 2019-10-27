using Data.Model;
using Data.Utils;
using LaZeroDayCore.Controller;
using MahApps.Metro.Controls;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace UserControls.ViewModels
{
    class TableProductsEditViewModel : Bases.ViewModelBase
    {
        public TableProductsEditViewModel()
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
                var Selected = (p_message as PRODUCT);
                InitInput(Selected);
            }
            else
            {
                InitInput(new PRODUCT()
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
                            PICTURE_PRODUCT = F_Image.BitmapImage2Bytes(bitmapImage);
                        }
                        catch (Exception e)
                        {
                            F_File.LogError(e);
                        }
                    }
                    break;
                case "DeleteImage":
                    {
                        PICTURE_PRODUCT = new byte[] { };
                    }
                    break;
                case "Save":
                    {
                        T_Helper.product.EditFromObject(getInput());
                        TableProductsViewModel.Send(null);
                    }
                    break;
                case "OverlayGridCancel":
                    {
                        TableProductsViewModel.Send(null);
                    }
                    break;
                default: break;
            }
        }
        #endregion
        //************************************************************************************* in out
        #region in out
        void InitInput(PRODUCT p_table)
        {
            ID = p_table.ID;
            NAME = p_table.NAME;
            DESCRIPTION = p_table.DESCRIPTION;
            CODE = p_table.CODE;
            //IMPORTANCE = 0;
            QUANTITY = p_table.QUANTITY;
            QUANTITY_MIN = p_table.QUANTITY_MIN;
            TYPE = p_table.TYPE;
            TAX_PERCE = p_table.TAX_PERCE;
            MONEY_PURCHASE = p_table.MONEY_PURCHASE;
            MONEY_SELLING_1 = p_table.MONEY_SELLING_1;
            //MONEY_SELLING_2 = p_table.MONEY_SELLING_1;
            //MONEY_SELLING_3 = p_table.MONEY_SELLING_1;
            //MONEY_SELLING_4 = p_table.MONEY_SELLING_1;
            //MONEY_SELLING_5 = p_table.MONEY_SELLING_1;
            MONEY_SELLING_MIN = p_table.MONEY_SELLING_MIN;
            DATE_PRODUCTION = p_table.DATE_PRODUCTION ?? DateTime.Now;
            DATE_PURCHASE = p_table.DATE_PURCHASE?? DateTime.Now;
            DATE_EXPIRATION = p_table.DATE_EXPIRATION ?? DateTime.Now;
            PICTURE_PRODUCT = p_table.PICTURE;
        }
        PRODUCT getInput()
        {
            var r = new PRODUCT() {
                ID = ID,
                NAME = NAME,
                DESCRIPTION = DESCRIPTION,
                CODE = CODE,
                IMPORTANCE = 0,
                QUANTITY = QUANTITY,
                QUANTITY_MIN = QUANTITY_MIN,
                TYPE = TYPE,
                TAX_PERCE = TAX_PERCE,
                MONEY_PURCHASE = MONEY_PURCHASE,
                MONEY_SELLING_1 = MONEY_SELLING_1,
                MONEY_SELLING_2 = MONEY_SELLING_1,
                MONEY_SELLING_3 = MONEY_SELLING_1,
                MONEY_SELLING_4 = MONEY_SELLING_1,
                MONEY_SELLING_5 = MONEY_SELLING_1,
                MONEY_SELLING_MIN = MONEY_SELLING_MIN,
                DATE_PRODUCTION = DATE_PRODUCTION,
                DATE_PURCHASE = DATE_PURCHASE,
                DATE_EXPIRATION = DATE_EXPIRATION,
                PICTURE = PICTURE_PRODUCT,
            };
            return r;
        }
        #endregion
    }
}
