using Data.Model;
using Data.Utils;
using LaZeroDayCore.Controller;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UserControls.ViewModels.Bases;

namespace UserControls.ViewModels
{
    class TableInvoicePurchaseViewModel:Bases.ViewModelBase
    {
        public TableInvoicePurchaseViewModel()
        {
            initDelegateCommand();
            DateBegin = T_Helper.InvoicePurchase.GetBeginDate();
            DateEnd = T_Helper.InvoicePurchase.GetEndDate().AddDays(1);
            initEventHandler();
            initComboBoxOrderBy();
            Search();
        }
        //****************************************************************************************************** Messanger
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
                    var v = (KeyValuePair<string, string>)p_message;
                    ReceiverFrom = v.Key;
                    //MessageBox.Show("ReceiverFrom: " + ReceiverFrom);
                }
                catch { }
            }
            Visibility_Collapsed();
            Search();
        }
        private void VisibilityTableEdit_Visible()
        {
            VisibilityTableEdit = Visibility.Visible;
        }
        private void Visibility_Collapsed()
        {
            VisibilityTableEdit = Visibility.Collapsed;
        }
        #endregion
        //****************************************************************************************************** command
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
                case "PageBefore":
                    {
                        PageThis--;
                        Search();
                    }
                    break;
                case "PageNext":
                    {
                        PageThis++;
                        Search();
                    }
                    break;
                case "Add":
                    {
                        Search();
                    }
                    break;
                case "Edit":
                    {
                        Search();
                    }
                    break;
                case "Delete":
                    {
                        Search();
                    }
                    break;
                case "MouseDoubleClick":
                    {
                        if (DataGridSelectedItem != null)
                        {
                            int invoice_id = F_File.GetPropertyInt(DataGridSelectedItem, "ID");
                            if (ReceiverFrom == KeyStatic.class_.InvoicePurchaseViewModel)
                            {
                                InvoicePurchaseViewModel.Send(new KeyValuePair<string, int>(KeyStatic.view_.invoice_id, invoice_id));
                                ReceiverFrom = "";
                            }
                            else
                            {
                                //MessageBox.Show("ReceiverFrom: error: "+ ReceiverFrom);
                            }
                        }
                    }
                    break;
                default: break;
            }
        }
        #endregion
        //****************************************************************************************************** search
        #region search
        static int PageThis = 0;
        public void Search()
        {
            try
            {
                DataGridItemsSource = T_Helper.InvoicePurchase.search(SearchValue, ref PageThis, ComboBoxOrderBySelected, T_Helper.user.thisUser.ID, -1, ((DateTime)DateBegin), (DateTime)DateEnd).ToList();
                //DataGridItemsSource = T_Helper.InvoicePurchase.search(SearchValue, ref PageThis, ComboBoxOrderBySelected, -1, -1, ((DateTime)DateBegin), (DateTime)DateEnd).ToList();
                Label_Page = T_Helper.InvoicePurchase.GetLableSearch();
            }
            catch (Exception e)
            {
                F_File.LogError(e);
            }
        }
        //
        private string _SearchValue;
        public string SearchValue
        {
            get { return _SearchValue ?? ""; }
            set
            {
                SetProperty(ref _SearchValue, value);
                PageThis = 0;
                Search();
            }
        }
        #endregion
        //****************************************************************************************************** ComboBoxOrderBy
        #region ComboBoxOrderBy
        public class OrderBy { public string Display { get; set; } public string Value { get; set; } }
        public void initComboBoxOrderBy()
        {
            string[] names = typeof(INVOICES_PURCHASES).GetProperties().Select(property => property.Name).ToArray();
            ComboBoxOrderBySource = new List<OrderBy>();
            for (int i = 0; i < names.Length; i++)
            {
                ComboBoxOrderBySource.Add(new OrderBy { Display = names[i], Value = names[i] });
            }
            ComboBoxOrderBySelected = "ID";
        }
        private string _ComboBoxOrderBySelected;
        public string ComboBoxOrderBySelected
        {
            get { return _ComboBoxOrderBySelected ?? "ID"; }
            set
            {
                SetProperty(ref _ComboBoxOrderBySelected, value);
                Search();
            }
        }
        private List<OrderBy> _ComboBoxOrderBySource;
        public List<OrderBy> ComboBoxOrderBySource
        {
            get { return _ComboBoxOrderBySource; }
            set
            {
                SetProperty(ref _ComboBoxOrderBySource, value);
            }
        }
        #endregion
        //******************************************************************************************************
        private DateTime? _DateBegin;
        public DateTime? DateBegin
        {
            get { return _DateBegin ?? DateTime.Now; }
            set { SetProperty(ref _DateBegin, value); Search(); }
        }
        private DateTime? _DateEnd;
        public DateTime? DateEnd
        {
            get { return _DateEnd ?? DateTime.Now; }
            set { SetProperty(ref _DateEnd, value); Search(); }
        }

        private Object _DataGridSelectedItem;
        public Object DataGridSelectedItem
        {
            get { return _DataGridSelectedItem; }
            set { SetProperty(ref _DataGridSelectedItem, value); }
        }
    }
}
