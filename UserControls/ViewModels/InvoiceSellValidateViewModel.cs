using Data.Utils;
using LaZeroDayCore.Controller;
using Prism.Commands;
using Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UserControls.ViewModels
{
    class InvoiceSellValidateViewModel:Bases.ViewModelBase
    {
        public InvoiceSellValidateViewModel()
        {
            initDelegateCommand();
            initEventHandler();
        }
        //****************************************************************************************************** Messanger Begin
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
        string ReceiverFrom = "";
        public void Receiver(object p_message)
        {
            if (p_message != null)
            {
                try
                {
                    var v = (KeyValuePair<string, Int32>)p_message;
                    ReceiverFrom = v.Key;
                    refresh(v.Value);
                    //MessageBox.Show("ReceiverFrom: " + ReceiverFrom);
                }
                catch { }
            }
        }
        #endregion
        //****************************************************************************************************** function
        #region function
        void refresh(int p_invoice_id)
        {
            Invoice_ALL = T_Helper.InvoiceSell.Count();
            //
            Invoice_ID = p_invoice_id;
            T_Helper.InvoiceSell.Calc(Invoice_ID);
            var mInvoiceSell = T_Helper.InvoiceSell.Get(p_invoice_id);
            //
            Customer_id = mInvoiceSell.ID_CUSTOMERS ?? 0;
            Customer_Name = T_Helper.customer.Get(Customer_id).NAME;
            //
            money_total = mInvoiceSell.MONEY_TOTAL;
            Description = mInvoiceSell.DESCRIPTION;
        }
        #endregion
        //****************************************************************************************************** command
        #region command
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
                case "ValidateInvoice":
                    {
                        T_Helper.InvoiceSell.Validate(Invoice_ID, money_paid, Description);
                    }
                    break;
                case "PrintInvoice":
                    {
                        var p_kvp = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>("pName", T_Helper.user.thisUser.NAME),
                            new KeyValuePair<string, string>("pImage", F_Image.PictureCompanyGetbase64()),
                        };
                        var productsViews = T_Helper.productSell.GetProductsFromInvoice(Invoice_ID).ToList();
                        Printing.PrintInvoice(productsViews, p_kvp);
                    }
                    break;
                case "SaveInvoicePDF":
                    {
                        var p_kvp = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>("pName", T_Helper.user.thisUser.NAME),
                            new KeyValuePair<string, string>("pImage", F_Image.PictureCompanyGetbase64()),
                        };
                        var productsViews = T_Helper.productSell.GetProductsFromInvoice(Invoice_ID).ToList();
                        Printing.SaveInvoice(productsViews, p_kvp);
                    }
                    break;
                default: break;
            }
        }
        #endregion
        //****************************************************************************************************** property
        #region property
        private string _NumbersToWords;
        public string NumbersToWords { get { return _NumbersToWords ?? ""; } set { SetProperty(ref _NumbersToWords, value); } }
        private double _money_total;
        public double money_total
        { get { return _money_total; }
            set
            {
                SetProperty(ref _money_total, value);
                money_paid = _money_total; NumbersToWords = F_NumberToWord.NumberArabic((long)_money_total);
            }
        }
        private double _money_paid;
        public double money_paid
        {
            get { return _money_paid;  }
            set
            {
                SetProperty(ref _money_paid, value);
                _money_paid = _money_paid < 0 ? 0 : _money_paid;
                _money_paid = _money_paid > _money_total ? _money_total : _money_paid;
                money_unpaid = money_total - _money_paid;
            }
        }
        private double _money_unpaid;
        public double money_unpaid { get { return _money_unpaid; } set { SetProperty(ref _money_unpaid, value); } }
        private string _Description;
        public string Description { get { return _Description ?? ""; } set { SetProperty(ref _Description, value); } }
        #endregion

    }
}
