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
using UserControls.ViewModels.Bases;

namespace UserControls.ViewModels
{
    partial class InvoicePurchaseViewModel:Bases.ViewModelBase
    {
        public InvoicePurchaseViewModel()
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
                T_Helper.productPurchase.AddProductPurchase(v.Value, Invoice_ID);
            }
            else if (v.Key == KeyStatic.view_.wholesaler_id)
            {
                Wholesaler_id = v.Value;
                Wholesaler_Name = T_Helper.wholesaler.Get(Wholesaler_id).NAME;
                PICTURE_WHOLESALER = T_Helper.wholesaler.Get(Wholesaler_id).PICTURE;
                //
                var mInvoicePurchase = T_Helper.InvoicePurchase.Get(Invoice_ID);
                mInvoicePurchase.ID_WHOLESALERS = Wholesaler_id;
                T_Helper.InvoicePurchase.Commit();

            }
            else if (v.Key == KeyStatic.view_.invoice_id)
            {
                Invoice_ID = v.Value;
            }
            refresh(Invoice_ID);
            Visibility_Collapsed();
        }
    }
    //************************************************************************************* function
    partial class InvoicePurchaseViewModel : Bases.ViewModelBase
    {
        void AddDefaultInvoice()
        {
            var thisInvoice = T_Helper.InvoicePurchase.AddInvoice(T_Helper.user.thisUser.ID, 1);
            refresh(thisInvoice.ID);
        }
        void refresh(int p_invoice_id)
        {
            Invoice_ID = p_invoice_id;
            Invoice_ALL = T_Helper.InvoicePurchase.Count();
            //
            var v = T_Helper.InvoicePurchase.Get(p_invoice_id);
            //
            Wholesaler_id = v.ID_WHOLESALERS ?? 0;
            Wholesaler_Name = T_Helper.wholesaler.Get(Wholesaler_id).NAME;
            PICTURE_WHOLESALER = T_Helper.wholesaler.Get(Wholesaler_id).PICTURE;
            //
            DataGridItemsSource = T_Helper.productPurchase.GetProductsFromInvoice(Invoice_ID).ToList();
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
        #endregion
    }
    //************************************************************************************* command
    partial class InvoicePurchaseViewModel : Bases.ViewModelBase
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
                case "PrintInvoice":
                    {
                        var p_kvp = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>("pName", T_Helper.user.thisUser.NAME),
                            new KeyValuePair<string, string>("pImage", F_Image.PictureCompanyGetbase64()),
                        };
                        Printing.PrintInvoice(DataGridItemsSource, p_kvp);
                    }
                    break;
                case "SaveInvoicePDF":
                    {
                        var p_kvp = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>("pName", T_Helper.user.thisUser.NAME),
                            new KeyValuePair<string, string>("pImage", F_Image.PictureCompanyGetbase64()),
                        };
                        Printing.SaveInvoice(DataGridItemsSource, p_kvp);
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
                                T_Helper.productPurchase.Delete(id_selectedProduct);
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
                        EditValue = T_Helper.productPurchase.Get(id_selectedProduct).MONEY_UNIT.ToString();
                        VisibilityEdit_Visible();
                    }
                    break;
                case "EditQuantity":
                    {
                        EditWhat = "EditQuantity";
                        EditValue = T_Helper.productPurchase.Get(id_selectedProduct).QUANTITY.ToString();
                        VisibilityEdit_Visible();
                    }
                    break;
                case "EditTaxPerce":
                    {
                        EditWhat = "EditTaxPerce";
                        EditValue = T_Helper.productPurchase.Get(id_selectedProduct).TAX_PERCE.ToString();
                        VisibilityEdit_Visible();
                    }
                    break;
                case "EditStamp":
                    {
                        EditWhat = "EditStamp";
                        EditValue = T_Helper.productPurchase.Get(id_selectedProduct).STAMP.ToString();
                        VisibilityEdit_Visible();
                    }
                    break;
                case "EditOk":
                    {
                        if (EditWhat.Equals("EditMoneyUnit")) T_Helper.productPurchase.EditMoneyUnitOfProductPurchase(Convert.ToDouble(EditValue), id_selectedProduct);
                        else if (EditWhat.Equals("EditQuantity")) T_Helper.productPurchase.EditQuantityOfProductPurchase(Convert.ToDouble(EditValue), id_selectedProduct);
                        else if (EditWhat.Equals("EditTaxPerce")) T_Helper.productPurchase.EditTaxPerceOfProductPurchase(Convert.ToDouble(EditValue), id_selectedProduct);
                        else if (EditWhat.Equals("EditStamp")) T_Helper.productPurchase.EditStampOfProductPurchase(Convert.ToDouble(EditValue), id_selectedProduct);
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
                case "EditWholesaler":
                    {
                        VisibilitySearchWholesaler_Visible();
                        TableWholesalerViewModel.Send(new KeyValuePair<string, string>(this.GetType().Name, ""));
                    }
                    break;
                //********************************************************************
                case "EditInvoice":
                    {
                        VisibilitySearchInvoice_Visible();
                        TableInvoicePurchaseViewModel.Send(new KeyValuePair<string, string>(this.GetType().Name, ""));
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
    partial class InvoicePurchaseViewModel : Bases.ViewModelBase
    {
        private void VisibilitySearchInvoice_Visible()
        {
            VisibilityEdit = Visibility.Collapsed;
            VisibilitySearchProduct = Visibility.Collapsed;
            VisibilitySearchWholesaler = Visibility.Collapsed;
            VisibilitySearchInvoice = Visibility.Visible;
        }
        private void VisibilitySearchProduct_Visible()
        {
            VisibilityEdit = Visibility.Collapsed;
            VisibilitySearchProduct = Visibility.Visible;
            VisibilitySearchWholesaler = Visibility.Collapsed;
            VisibilitySearchInvoice = Visibility.Collapsed;
        }
        void VisibilityEdit_Visible()
        {
            VisibilityEdit = Visibility.Visible;
            VisibilitySearchProduct = Visibility.Collapsed;
            VisibilitySearchWholesaler = Visibility.Collapsed;
            VisibilitySearchInvoice = Visibility.Collapsed;
        }
        private void VisibilitySearchWholesaler_Visible()
        {
            VisibilityEdit = Visibility.Collapsed;
            VisibilitySearchProduct = Visibility.Collapsed;
            VisibilitySearchWholesaler = Visibility.Visible;
            VisibilitySearchInvoice = Visibility.Collapsed;
        }
        private void Visibility_Collapsed()
        {
            VisibilityEdit = Visibility.Collapsed;
            VisibilitySearchProduct = Visibility.Collapsed;
            VisibilitySearchWholesaler = Visibility.Collapsed;
            VisibilitySearchInvoice = Visibility.Collapsed;
        }
    }
    //************************************************************************************* Property
    partial class InvoicePurchaseViewModel : Bases.ViewModelBase
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
