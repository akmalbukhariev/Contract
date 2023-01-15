using Contract.Net;
using Contract.Pages.UnapprovedContracts;
using LibContract.HttpResponse;
using Plugin.FirebasePushNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Contract.Notification
{
    public class PushNotification
    {
        public bool ClickedNotification = false;
        public string Message = "";
        public string Token = "";
        private static PushNotification _instance = null;
        public static PushNotification Instance
        {
            get 
            {
                if (_instance == null)
                {
                    _instance = new PushNotification();
                }

                return _instance;
            }
        }
        public PushNotification()
        {
            
        }

        public void Init()
        {
            CrossFirebasePushNotification.Current.Subscribe(LibContract.Constants.All);
            CrossFirebasePushNotification.Current.OnTokenRefresh += Current_OnTokenRefresh;
            CrossFirebasePushNotification.Current.OnNotificationOpened += Current_OnNotificationOpened;
            CrossFirebasePushNotification.Current.OnNotificationAction += Current_OnNotificationAction;
            CrossFirebasePushNotification.Current.OnNotificationReceived += Current_OnNotificationReceived;
        }

        public async void SetToken(string phoneNumber)
        {
            if (Token == "") return;

            LibContract.HttpModels.User user = new LibContract.HttpModels.User()
            {
                phone_number = phoneNumber,
                token = Token
            };

            ResponseLogin responseToken = await HttpService.UpdateNotificationToken(user);
            if (!responseToken.result)
                await Application.Current.MainPage.DisplayAlert("Token", $"Token: {RSC.Failed}", RSC.Ok);
        }

        private void Current_OnTokenRefresh(object source, FirebasePushNotificationTokenEventArgs e)
        {
            Token = e.Token;

            if (Control.ControlApp.Instance.UserInfo == null || Control.ControlApp.Instance.UserInfo.phone_number == "") return;
            SetToken(Control.ControlApp.Instance.UserInfo.phone_number); 
        }

        private void Current_OnNotificationOpened(object source, FirebasePushNotificationResponseEventArgs e)
        {
            ClickedNotification = true;
        }

        private void Current_OnNotificationAction(object source, FirebasePushNotificationResponseEventArgs e)
        {

        }

        private void Current_OnNotificationReceived(object source, FirebasePushNotificationDataEventArgs e)
        {
            string strTitle = string.Empty;
            string strBody = string.Empty; 

            if (e.Data != null && e.Data.Count >= 2)
            {
                strBody = e.Data.ElementAt(0).Key == "body" ? e.Data.ElementAt(0).Value as string : "";
                strTitle = e.Data.ElementAt(1).Key == "title" ? e.Data.ElementAt(1).Value as string : "";

                Control.ControlApp.Instance.ShowNotification(strBody, strTitle);
            }
        }
    }
}
