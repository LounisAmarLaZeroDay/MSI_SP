using Data.Model;
using Data.Utils;
using LaZeroDayCore.Controller;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using UserControls.Views;

namespace UserControls.ViewModels
{
    class TableUsersViewModel : Bases.ViewModelBase
    {
        public TableUsersViewModel()
        {
            Search();
            initDelegateCommand();
            initComboBoxOrderBy();
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
                        VisibilityTableEdit_Visible();
                        TableUsersEditViewModel.Send(null);
                    }
                    break;
                case "Edit":
                    {
                        VisibilityTableEdit_Visible();
                        if (DataGridSelectedItem != null) TableUsersEditViewModel.Send(DataGridSelectedItem);
                    }
                    break;
                case "Delete":
                    {
                        try
                        {
                            var delete = DialogAlert.Delete();
                            if (delete == MessageBoxResult.OK)
                            {
                                if (DataGridSelectedItem != null)
                                {
                                    T_Helper.user.Delete(F_File.GetPropertyInt(DataGridSelectedItem, "ID"));
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            F_File.LogError(e);
                            DialogError.Error();
                        }
                        Search();
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
        }
        #endregion
        //************************************************************************************* Search
        #region search
        static int PageThis = 0;
        public void Search()
        {
            try
            {
                DataGridItemsSource = T_Helper.user.search(SearchValue, ref PageThis, ComboBoxOrderBySelected).ToList();
                Label_Page = T_Helper.user.GetLableSearch();
            }
            catch { }
        }
        private string _searchValue;
        public string SearchValue
        {
            get { return _searchValue ?? ""; }
            set
            {
                SetProperty(ref _searchValue, value);
                PageThis = 0;
                Search();
            }
        }
        #endregion
        //************************************************************************************* ComboBoxOrderBy
        #region ComboBoxOrderBy
        public class OrderBy { public string Display { get; set; } public string Value { get; set; } }
        public void initComboBoxOrderBy()
        {
            string[] names = typeof(USER).GetProperties().Select(property => property.Name).ToArray();
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
        //*************************************************************************************
        private Object _DataGridSelectedItem;
        public Object DataGridSelectedItem
        {
            get { return _DataGridSelectedItem; }
            set { SetProperty(ref _DataGridSelectedItem, value); }
        }
    }
}