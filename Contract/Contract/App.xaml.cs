using Contract.Control;
using Contract.Pages.CreateContract;
using Contract.Pages.Customers;
using Contract.Pages.Login;
using Contract.Pages.Setting;
using Contract.Resources;
using System;
using System.Globalization;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract
{
    internal class RSC : AppResource
    {

    }

    public partial class App : Application
    { 
        public App()
        {
            InitializeComponent(); 
        }

        protected override void OnStart()
        {
            //MainPage = new TransitionNavigationPage(new Pages.TemplateContract.PageContractTemplateTable());
            //return;
            ControlApp.Instance.AppStarting = true;
            ControlApp.Instance.AppOnResume = false;
            ControlApp.Instance.AppOnSleep = false;

            Thread.CurrentThread.CurrentUICulture = CultureInfo.InstalledUICulture;

            string strLanguage = AppSettings.GetLanguage();
            if (strLanguage == string.Empty)
            {
                MainPage = new TransitionNavigationPage(new PageLanguage());
            }
            else
            {
                AppSettings.SetLanguage(strLanguage);
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
