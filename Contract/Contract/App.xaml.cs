using Contract.Control;
using Contract.Notification;
using Contract.Pages.CreateContract;
using Contract.Pages.Customers;
using Contract.Pages.Login;
using Contract.Pages.Setting;
using Contract.Resources;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract
{
    //https://easyappicon.com/
    internal class RSC : AppResource
    {

    }

    public static class StringHelper
    {
        public static string RemoveWhitespace(this string input)
        {
            if (input == null)
                input = "";
            else
                input = input.Trim();

            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }
    }

    public partial class App : Application
    { 
        public App()
        {
            InitializeComponent(); 
        }

        protected override void OnStart()
        {
            //MainPage = new TransitionNavigationPage(new Pages.Introduction.PageIntroduction());
            //MainPage = new TransitionNavigationPage(new Pages.CreateContract.PageShowContract("http://65.20.68.60:5000/Upload/contract.html"));
            //return;
            PushNotification.Instance.Init();
            ControlApp.Instance.AppStarting = true;
            ControlApp.Instance.AppOnResume = false;
            ControlApp.Instance.AppOnSleep = false;

            Thread.CurrentThread.CurrentUICulture = CultureInfo.InstalledUICulture;

            ControlApp.Instance.LanguageId = AppSettings.GetLanguage();
            if (ControlApp.Instance.LanguageId == string.Empty)
            {
                MainPage = new TransitionNavigationPage(new PageLanguage());
            }
            else
            {
                AppSettings.SetLanguage(ControlApp.Instance.LanguageId);
                MainPage = new TransitionNavigationPage(new PageLogin());
            }
        }

        protected override void OnSleep()
        {
            ControlApp.Instance.AppOnSleep = true;
        }

        protected override void OnResume()
        {
            ControlApp.Instance.AppOnResume = true;
        }
    }
}
