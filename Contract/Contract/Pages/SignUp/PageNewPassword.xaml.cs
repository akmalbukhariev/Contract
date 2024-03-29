﻿using Contract.Control;
using LibContract.HttpResponse;
using Contract.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.SignUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageNewPassword : IPage
    {
        public PageNewPassword()
        {
            InitializeComponent();

            SetModel(new ViewModel.BaseModel(Navigation));
            navigationBar.Title = RSC.SignUp;
            btnNext.Text = RSC.ButtonNext;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Model.Parent = Parent;
        }

        private async void Next_Clicked(object sender, EventArgs e)
        {
            if (!ControlApp.InternetOk()) return;

            if (string.IsNullOrEmpty(password1.Text) || string.IsNullOrEmpty(password2.Text))
            {
                await DisplayAlert(RSC.Password, RSC.SignUp_Message_2, RSC.Ok);
                return;
            }

            if (!password1.Text.Equals(password2.Text))
            {
                await DisplayAlert(RSC.Password, RSC.SignUp_Message_3, RSC.Ok); ;
                return;
            }

            var userInfo = new LibContract.HttpModels.User()
            {
                phone_number = ControlApp.LoginInfo.phone_number,
                password = password1.Text,
                lan_id = ControlApp.LanguageId,
                reg_date = DateTime.Now.ToString(Constants.TimeFormat)
            };

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseSignUp response = await HttpService.SignUp(userInfo);
            ControlApp.CloseLoadingView();

            if (!ControlApp.CheckResponse(response)) return;

            if (!response.result)
            {
                await DisplayAlert(RSC.SignUp, RSC.Failed, RSC.Ok);
                return;
            }

            Notification.PushNotification.Instance.SetToken(userInfo.phone_number);
            await DisplayAlert(RSC.SignUp, RSC.SignUp_Message_4, RSC.Ok);

            Model.SetTransitionType();
            await Navigation.PopToRootAsync(); 
        }
    }
}