﻿using Contract.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contract.Model
{
    public class Customer : BaseModel
    {
        public bool IsThisEditClient { get => GetValue<bool>(); set => SetValue(value); }
        public bool ShowCircleImage { get => GetValue<bool>(); set => SetValue(value); }
        public bool ShowLetter { get => GetValue<bool>(); set => SetValue(value); }
        public string FirstLetter { get => GetValue<string>(); set => SetValue(value); }
        public string UserImage { get => GetValue<string>(); set => SetValue(value); }
        public string UserTitle { get => GetValue<string>(); set => SetValue(value); }
        public string UserStir { get => GetValue<string>(); set => SetValue(value); }
        public bool SwipeEnable { get => GetValue<bool>(); set => SetValue(value); }

        public Customer()
        {
            IsThisEditClient = true;
            ShowCircleImage = true;
            ShowLetter = false;
            FirstLetter = "";
            SwipeEnable = true;
            //UserImage = "rus_flag";
        }

        public ICommand ShowCustomerCommand => new Command(ShowCustomer);

        private void ShowCustomer()
        {
            ControlApp.Vibrate();
        }
    }
}
