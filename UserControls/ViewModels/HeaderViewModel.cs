using Data.Utils;
using LaZeroDayCore.Controller;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace UserControls.ViewModels
{
    partial class HeaderViewModel:Bases.ViewModelBase
    {
        public HeaderViewModel()
        {
            initDelegateCommand();
            loop();
            initEventHandler();
            PICTURE_COMPANY = F_Image.PictureCompanyGet();
        }
        //****************************************************************************************************** command
        #region commandExecute
        void initDelegateCommand() => commandExecute = new DelegateCommand<Object>(execute);
        public DelegateCommand<Object> commandExecute { get; set; }
        private void execute<Object>(Object obj)
        {
            string st = obj as string;
            switch (st)
            { 
                case "MainMenu":
                    {
                        if (T_Helper.user.thisUser == null) UserControlsModule.NavigateContent("Login");
                        else UserControlsModule.NavigateContent("MainMenu");
                    }
                    break;
                case "Settings":
                    {
                        UserControlsModule.NavigateContent("Settings");
                    }
                    break;
                case "Logout":
                    {
                        T_Helper.user.thisUser = null;
                        Send("Logout");
                        UserControlsModule.NavigateContent("Login");
                    }
                    break;
                case "Close":
                    {
                        DialogAlert.exitYasNo();
                    }
                    break;
                default: break;
            }
        }
        #endregion
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
            if (T_Helper.user.thisUser != null)
            {
                User_Name = T_Helper.user.thisUser.NAME;
                PICTURE_USER = T_Helper.user.thisUser.PICTURE;
                VisibilityLogout = Visibility.Visible;
            }
            else
            {
                User_Name = "";
                PICTURE_USER = new byte[] { };
                VisibilityLogout = Visibility.Collapsed;
            }
        }
        #endregion
    }
    //****************************************************************************************************** Timer
    partial class HeaderViewModel : Bases.ViewModelBase
    {
        DispatcherTimer timer = new DispatcherTimer();
        void loop()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(InvalidateSampleData);
            //timer.Start();
        }
        private void InvalidateSampleData(object state, EventArgs e)
        {
            DigitalTimer = F_Time.DateTime2String_yyyy_MM_dd_HH_mm_ss(DateTime.Now);
        }
        
    }
    //****************************************************************************************************** Proprity
    partial class HeaderViewModel : Bases.ViewModelBase
    {
        private string _DigitalTimer;
        public string DigitalTimer
        {
            get { return _DigitalTimer ?? ""; }
            set
            {
                SetProperty(ref _DigitalTimer, value);
            }
        }
    }
}
