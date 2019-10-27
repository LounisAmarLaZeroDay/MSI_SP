using Data.Model;
using Data.Utils;
using LaZeroDayCore.Controller;
using Prism.Commands;
using Report;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using UserControls.ViewModels.Bases;
using UserControls.Views;

namespace UserControls.ViewModels
{
    partial class InvoiceSellViewModel : Bases.ViewModelBase
    {
        public InvoiceSellViewModel()
        {
            initDelegateCommand();
            AddDefaultInvoice();
            //
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
        public void Receiver(object p_message)
        {
            var v = (KeyValuePair<string, int>)p_message;
            if (v.Key == KeyStatic.view_.product_id)
            {
                T_Helper.productSell.AddProductSell(v.Value, Invoice_ID);
            }
            else if (v.Key == KeyStatic.view_.customer_id)
            {
                Customer_id = v.Value;
                Customer_Name = T_Helper.customer.Get(Customer_id).NAME;
                PICTURE_CUSTOMER = T_Helper.customer.Get(Customer_id).PICTURE;
                //
                var mInvoiceSell = T_Helper.InvoiceSell.Get(Invoice_ID);
                mInvoiceSell.ID_CUSTOMERS = Customer_id;
                T_Helper.InvoiceSell.Commit();

            }
            else if (v.Key == KeyStatic.view_.invoice_id)
            {
                Invoice_ID = v.Value;
            }
            refresh(Invoice_ID);
            Visibility_Collapsed();
        }
        #endregion
    }
    //************************************************************************************* function
    partial class InvoiceSellViewModel : Bases.ViewModelBase
    {
        void AddDefaultInvoice()
        {
            var thisInvoice = T_Helper.InvoiceSell.AddInvoice(T_Helper.user.thisUser.ID, 1);
            refresh(thisInvoice.ID);
        }
        void refresh(int p_invoice_id)
        {
            Invoice_ID = p_invoice_id;
            Invoice_ALL = T_Helper.InvoiceSell.Count();
            //
            var v = T_Helper.InvoiceSell.Get(p_invoice_id);
            //
            Customer_id = v.ID_CUSTOMERS ?? 0;
            Customer_Name = T_Helper.customer.Get(Customer_id).NAME;
            PICTURE_CUSTOMER = T_Helper.customer.Get(Customer_id).PICTURE;
            //
            DataGridItemsSource = T_Helper.productSell.GetProductsFromInvoice(Invoice_ID).ToList();
            InvoiceCalc(DataGridItemsSource);
        }
        void InvoiceCalc(System.Collections.IEnumerable p_list)
        {
            double sum = 0f;
            foreach (object o in p_list)
            {
                sum += F_File.GetPropertyDouble(o, "MONEY_PAID");
            }
            MONEY_PAID_ALL = sum;
        }
        private string EditWhat = "";
    }
    //************************************************************************************* command
    partial class InvoiceSellViewModel : Bases.ViewModelBase
    {
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
                        VisibilityValidate_Visible();
                        InvoiceSellValidateViewModel.Send(new KeyValuePair<string, Int32>(this.GetType().Name, Invoice_ID));
                    }   
                    break;
                //********************************************************************
                case "Delete":
                    {
                        try
                        {
                            var delete = DialogAlert.Delete();
                            if (delete == MessageBoxResult.OK)
                            {
                                T_Helper.productSell.Delete(id_selectedProduct);
                            }
                        }
                        catch (Exception e)
                        {
                            F_File.LogError(e);
                        }
                    }
                    break;
                //********************************************************************
                case "AddNewInvoice":
                    {
                        AddDefaultInvoice();
                    }
                    break;
                //********************************************************************
                case "MouseDoubleClick":
                    {

                    }
                    break;
                //********************************************************************
                case "EditMoneyUnit":
                    {
                        EditWhat = "EditMoneyUnit";
                        EditValue = T_Helper.productSell.Get(id_selectedProduct).MONEY_UNIT.ToString();
                        VisibilityEdit_Visible();
                    }
                    break;
                case "EditQuantity":
                    {
                        EditWhat = "EditQuantity";
                        EditValue = T_Helper.productSell.Get(id_selectedProduct).QUANTITY.ToString();
                        VisibilityEdit_Visible();
                    }
                    break;
                case "EditTaxPerce":
                    {
                        EditWhat = "EditTaxPerce";
                        EditValue = T_Helper.productSell.Get(id_selectedProduct).TAX_PERCE.ToString();
                        VisibilityEdit_Visible();
                    }
                    break;
                case "EditStamp":
                    {
                        EditWhat = "EditStamp";
                        EditValue = T_Helper.productSell.Get(id_selectedProduct).STAMP.ToString();
                        VisibilityEdit_Visible();
                    }
                    break;
                case "EditOk":
                    {
                        if (EditWhat.Equals("EditMoneyUnit")) T_Helper.productSell.EditMoneyUnitOfProductSell(Convert.ToDouble(EditValue), id_selectedProduct);
                        else if (EditWhat.Equals("EditQuantity")) T_Helper.productSell.EditQuantityOfProductSell(Convert.ToDouble(EditValue), id_selectedProduct);
                        else if (EditWhat.Equals("EditTaxPerce")) T_Helper.productSell.EditTaxPerceOfProductSell(Convert.ToDouble(EditValue), id_selectedProduct);
                        else if (EditWhat.Equals("EditStamp")) T_Helper.productSell.EditStampOfProductSell(Convert.ToDouble(EditValue), id_selectedProduct);
                        Visibility_Collapsed();
                    }
                    break;
                //********************************************************************
                case "GotFocusSearch":
                    {
                        VisibilitySearchProduct_Visible();
                        TableProductsViewModel.Send(new KeyValuePair<string, string>(this.GetType().Name, ValueSearchProduct));
                    }
                    break;
                //********************************************************************
                case "EditCustomer":
                    {
                        VisibilitySearchCustomer_Visible();
                        TableCustomersViewModel.Send(new KeyValuePair<string, string>(this.GetType().Name, ""));
                    }
                    break;
                //********************************************************************
                case "EditInvoice":
                    {
                        VisibilitySearchInvoice_Visible();
                        TableInvoiceSellViewModel.Send(new KeyValuePair<string, string>(this.GetType().Name, ""));
                    }
                    break;
                //********************************************************************
                case "OverlayGridCancel":
                    {
                        Visibility_Collapsed();
                    }
                    break;
                default: break;
            }
            refresh(Invoice_ID);
        }
    }
    //************************************************************************************* Visibility
    partial class InvoiceSellViewModel : Bases.ViewModelBase
    {
        private void VisibilityValidate_Visible()
        {
            VisibilityValidate = Visibility.Visible;
            VisibilityEdit = Visibility.Collapsed;
            VisibilitySearchProduct = Visibility.Collapsed;
            VisibilitySearchCustomer = Visibility.Collapsed;
            VisibilitySearchInvoice = Visibility.Collapsed;
        }
        private void VisibilitySearchInvoice_Visible()
        {
            VisibilityValidate = Visibility.Collapsed;
            VisibilityEdit = Visibility.Collapsed;
            VisibilitySearchProduct = Visibility.Collapsed;
            VisibilitySearchCustomer = Visibility.Collapsed;
            VisibilitySearchInvoice = Visibility.Visible;
        }
        private void VisibilitySearchProduct_Visible()
        {
            VisibilityValidate = Visibility.Collapsed;
            VisibilityEdit = Visibility.Collapsed;
            VisibilitySearchProduct = Visibility.Visible;
            VisibilitySearchCustomer = Visibility.Collapsed;
            VisibilitySearchInvoice = Visibility.Collapsed;
        }
        void VisibilityEdit_Visible()
        {
            VisibilityValidate = Visibility.Collapsed;
            VisibilityEdit = Visibility.Visible;
            VisibilitySearchProduct = Visibility.Collapsed;
            VisibilitySearchCustomer = Visibility.Collapsed;
            VisibilitySearchInvoice = Visibility.Collapsed;
        }
        private void VisibilitySearchCustomer_Visible()
        {
            VisibilityValidate = Visibility.Collapsed;
            VisibilityEdit = Visibility.Collapsed;
            VisibilitySearchProduct = Visibility.Collapsed;
            VisibilitySearchCustomer = Visibility.Visible;
            VisibilitySearchInvoice = Visibility.Collapsed;
        }
        private void Visibility_Collapsed()
        {
            VisibilityValidate = Visibility.Collapsed;
            VisibilityEdit = Visibility.Collapsed;
            VisibilitySearchProduct = Visibility.Collapsed;
            VisibilitySearchCustomer = Visibility.Collapsed;
            VisibilitySearchInvoice = Visibility.Collapsed;
        }
    }
    //************************************************************************************* Property
    partial class InvoiceSellViewModel : Bases.ViewModelBase
    {
        int id_selectedProduct;
        private Object _DataGridSelectedItem;
        public Object DataGridSelectedItem
        {
            get { return _DataGridSelectedItem; }
            set
            {
                SetProperty(ref _DataGridSelectedItem, value);
                if (_DataGridSelectedItem != null)
                {
                    id_selectedProduct = F_File.GetPropertyInt(_DataGridSelectedItem, "ID");
                }
            }
        }
        private string _ValueSearchProduct;
        public string ValueSearchProduct
        {
            get { return _ValueSearchProduct ?? ""; }
            set
            {
                SetProperty(ref _ValueSearchProduct, value);
                TableProductsViewModel.Send(new KeyValuePair<string, string>(this.GetType().Name, _ValueSearchProduct));
            }
        }
    }

}
