﻿using Contract.Control;
using Contract.Net;
using Contract.Pages;
using Contract.Pages.SignUp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contract.ViewModel.Pages.Login
{
    public class PageLoginViewModel : BaseModel
    {
        public bool CheckAutoLogin { get => GetValue<bool>(); set => SetValue(value); }
        public string PhoneNumber { get => GetValue<string>(); set => SetValue(value); }
        public string Password { get => GetValue<string>(); set => SetValue(value); }
        public PageLoginViewModel(INavigation navigation) : base(navigation)
        {
            
        }

        public ICommand CommandLogin => new Command(Login);
        public ICommand CommandSignUp => new Command(SignUp);

        private async void Login()
        {
            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseUser response = await HttpService.GetUser("12");
            ControlApp.CloseLoadingView();

            if (response.result)
            {
                
            }
             //Application.Current.MainPage = new TransitionNavigationPage(new PageMasterDetail()); 
        }

        private async void SignUp()
        {
            SetTransitionType(TransitionType.SlideFromRight);
            await Navigation.PushAsync(new PagePhoneNumber()); 
        }
    }
}
