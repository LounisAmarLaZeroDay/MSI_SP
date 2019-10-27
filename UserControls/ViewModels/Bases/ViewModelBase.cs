using Data.Utils;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UserControls.ViewModels.Bases
{
    public class ViewModelBase : BindableBase
    {
        #region int
        private int _ID = -1;
        public int ID { get { return _ID; } set { SetProperty(ref _ID, value); } }
        private int _TYPE;
        public int TYPE { get { return _TYPE; } set { SetProperty(ref _TYPE, value); } }
        private int _Invoice_ID;
        public int Invoice_ID { get { return _Invoice_ID; } set { SetProperty(ref _Invoice_ID, value); } }
        private int _Invoice_ALL;
        public int Invoice_ALL { get { return _Invoice_ALL; } set { SetProperty(ref _Invoice_ALL, value); } }
        private int _User_ID;
        public int User_ID { get { return _User_ID; } set { SetProperty(ref _User_ID, value); } }
        private int _Wholesaler_ID;
        public int Wholesaler_id { get { return _Wholesaler_ID; } set { SetProperty(ref _Wholesaler_ID, value); }}
        private int _Customer_ID;
        public int Customer_id { get { return _Customer_ID; } set { SetProperty(ref _Customer_ID, value); } }
        #endregion
        #region DateTime
        private DateTime? _BIRTHDAY;
        public DateTime? BIRTHDAY { get { return _BIRTHDAY ?? DateTime.Now; } set { SetProperty(ref _BIRTHDAY, value); } }
        private DateTime? _DATE_PRODUCTION;
        public DateTime? DATE_PRODUCTION { get { return _DATE_PRODUCTION ?? DateTime.Now; ; } set { SetProperty(ref _DATE_PRODUCTION, value); } }
        private DateTime? _DATE_PURCHASE;
        public DateTime? DATE_PURCHASE { get { return _DATE_PURCHASE ?? DateTime.Now; ; } set { SetProperty(ref _DATE_PURCHASE, value); } }
        private DateTime? _DATE_EXPIRATION;
        public DateTime? DATE_EXPIRATION { get { return _DATE_EXPIRATION ?? DateTime.Now; ; } set { SetProperty(ref _DATE_EXPIRATION, value); } }
        #endregion
        #region byte[]
        private byte[] _PICTURE_PRODUCT;
        public byte[] PICTURE_PRODUCT
        {
            get { return _PICTURE_PRODUCT; }
            set { SetProperty(ref _PICTURE_PRODUCT, value); }
        }
        private byte[] _PICTURE_WHOLESALER; 
        public byte[] PICTURE_WHOLESALER
        {
            get { return _PICTURE_WHOLESALER; }
            set { SetProperty(ref _PICTURE_WHOLESALER, value); }
        }
        private byte[] _PICTURE_CUSTOMER;
        public byte[] PICTURE_CUSTOMER
        {
            get { return _PICTURE_CUSTOMER; }
            set { SetProperty(ref _PICTURE_CUSTOMER, value); }
        }
        private byte[] _PICTURE_USER;
        public byte[] PICTURE_USER
        {
            get { return _PICTURE_USER; }
            set { SetProperty(ref _PICTURE_USER, value); }
        }
        private byte[] _PICTURE_COMPANY;
        public byte[] PICTURE_COMPANY
        {
            get { return _PICTURE_COMPANY; }
            set { SetProperty(ref _PICTURE_COMPANY, value); }
        }
        #endregion
        #region Visibility
        private Visibility _VisibilityValidate = Visibility.Collapsed;
        public Visibility VisibilityValidate
        {
            get { return _VisibilityValidate; }
            set { SetProperty(ref _VisibilityValidate, value); }
        }
        //
        private Visibility _VisibilityTableEdit = Visibility.Collapsed;
        public Visibility VisibilityTableEdit
        {
            get { return _VisibilityTableEdit; }
            set { SetProperty(ref _VisibilityTableEdit, value); }
        }
        //
        private Visibility _VisibilitySearchCustomer = Visibility.Collapsed;
        public Visibility VisibilitySearchCustomer
        {
            get { return _VisibilitySearchCustomer; }
            set { SetProperty(ref _VisibilitySearchCustomer, value); }
        }
        //
        private Visibility _VisibilitySearchProduct = Visibility.Collapsed;
        public Visibility VisibilitySearchProduct
        {
            get { return _VisibilitySearchProduct; }
            set { SetProperty(ref _VisibilitySearchProduct, value); }
        }
        //
        private Visibility _VisibilitySearchWholesaler = Visibility.Collapsed;
        public Visibility VisibilitySearchWholesaler
        {
            get { return _VisibilitySearchWholesaler; }
            set { SetProperty(ref _VisibilitySearchWholesaler, value); }
        }
        //
        private Visibility _EditVisibility = Visibility.Collapsed;
        public Visibility VisibilityEdit
        {
            get { return _EditVisibility; }
            set { SetProperty(ref _EditVisibility, value);}
        }
        //
        private Visibility _VisibilitySearchInvoice = Visibility.Collapsed;
        public Visibility VisibilitySearchInvoice
        {
            get { return _VisibilitySearchInvoice; }
            set { SetProperty(ref _VisibilitySearchInvoice, value);}
        }
        //
        private Visibility _VisibilityLogout = Visibility.Collapsed;
        public Visibility VisibilityLogout
        {
            get { return _VisibilityLogout; }
            set { SetProperty(ref _VisibilityLogout, value);}
        }
        #endregion
        #region double
        private double _MONEY_ACCOUNT;
        public double MONEY_ACCOUNT { get { return _MONEY_ACCOUNT; } set { SetProperty(ref _MONEY_ACCOUNT, value); } }
        private double _QUANTITY;
        public double QUANTITY { get { return _QUANTITY; } set { SetProperty(ref _QUANTITY, value); } }
        private double _QUANTITY_MIN;
        public double QUANTITY_MIN
        {
            get { return _QUANTITY_MIN; }
            set { SetProperty(ref _QUANTITY_MIN, value); }
        }
        private double _TAX_PERCE;
        public double TAX_PERCE
        {
            get { return _TAX_PERCE; }
            set { SetProperty(ref _TAX_PERCE, value); }
        }
        private double _MONEY_PURCHASE;
        public double MONEY_PURCHASE
        {
            get { return _MONEY_PURCHASE; }
            set { SetProperty(ref _MONEY_PURCHASE, value); }
        }
        private double _MONEY_SELLING_1;
        public double MONEY_SELLING_1
        {
            get { return _MONEY_SELLING_1; }
            set { SetProperty(ref _MONEY_SELLING_1, value); }
        }
        private double _MONEY_SELLING_MIN;
        public double MONEY_SELLING_MIN
        {
            get { return _MONEY_SELLING_MIN; }
            set { SetProperty(ref _MONEY_SELLING_MIN, value); }
        }
        private double _MONEY_PAID_ALL;
        public double MONEY_PAID_ALL
        {
            get { return _MONEY_PAID_ALL; }
            set { SetProperty(ref _MONEY_PAID_ALL, value); }
        }
        #endregion
        #region IEnumerable
        private System.Collections.IEnumerable _DataGridItemsSource;
        public System.Collections.IEnumerable DataGridItemsSource
        {
            get { return _DataGridItemsSource; }
            set { SetProperty(ref _DataGridItemsSource, value); }
        }
        #endregion
        #region string
        private string _NAME;
        public string NAME                    { get { return _NAME ?? ""; } set { SetProperty(ref _NAME, value); } }
        private string _DESCRIPTION;
        public string DESCRIPTION             { get { return _DESCRIPTION ?? ""; } set { SetProperty(ref _DESCRIPTION, value); } }
        private string _CODE;
        public string CODE                    { get { return _CODE ?? ""; } set { SetProperty(ref _CODE, value); } }
        private string _FIRSTNAME;
        public string FIRSTNAME               { get { return _FIRSTNAME ?? ""; } set { SetProperty(ref _FIRSTNAME, value); } }
        private string _LASTNAME;
        public string LASTNAME                { get { return _LASTNAME ?? ""; } set { SetProperty(ref _LASTNAME, value); } }
        private string _GENDER;
        public string GENDER                  { get { return _GENDER ?? ""; } set { SetProperty(ref _GENDER, value); } }
        private string _ADDRESS;
        public string ADDRESS                 { get { return _ADDRESS ?? ""; } set { SetProperty(ref _ADDRESS, value); } }
        private string _CITY;
        public string CITY                    { get { return _CITY ?? ""; } set { SetProperty(ref _CITY, value); } }
        private string _COUNTRY;
        public string COUNTRY                 { get { return _COUNTRY ?? ""; } set { SetProperty(ref _COUNTRY, value); } }
        private string _PHONE;
        public string PHONE                   { get { return _PHONE ?? ""; } set { SetProperty(ref _PHONE, value); } }
        private string _FAX;
        public string FAX                     { get { return _FAX ?? ""; } set { SetProperty(ref _FAX, value); } }
        private string _WEBSITE;
        public string WEBSITE                 { get { return _WEBSITE ?? ""; } set { SetProperty(ref _WEBSITE, value); } }
        private string _EMAIL;
        public string EMAIL                   { get { return _EMAIL ?? ""; } set { SetProperty(ref _EMAIL, value); } }

        private string _PASSWORD_CONFIRMATION;
        public string PASSWORD_CONFIRMATION   { get { return _PASSWORD_CONFIRMATION ?? ""; } set { SetProperty(ref _PASSWORD_CONFIRMATION, value); } }
        private string _PASSWORD;
        public string PASSWORD                { get { return _PASSWORD ?? ""; } set { SetProperty(ref _PASSWORD, value); } }
        private string _ACCESS;
        public string ACCESS                  { get { return _ACCESS ?? ""; } set { SetProperty(ref _ACCESS, value); } }
        private string _Label_Page;
        public string Label_Page              { get { return _Label_Page ?? ""; } set { SetProperty(ref _Label_Page, value); } }
        private string _User_Name;
        public string User_Name               { get { return _User_Name ?? ""; } set { SetProperty(ref _User_Name, value); }}
        private string _Customer_Name;
        public string Customer_Name { get { return _Customer_Name ?? ""; } set { SetProperty(ref _Customer_Name, value); } }
        private string _Wholesaler_Name;
        public string Wholesaler_Name { get { return _Wholesaler_Name ?? ""; } set { SetProperty(ref _Wholesaler_Name, value); } }

        private string _ValueCodeSearchProduct;
        public string ValueCodeSearchProduct  { get { return _ValueCodeSearchProduct ?? ""; } set { SetProperty(ref _ValueCodeSearchProduct, value);} }
        //
        private string _EditValue = "0";
        public string EditValue               { get { return _EditValue ?? ""; } set { SetProperty(ref _EditValue, value); } }
        #endregion
    }
}
