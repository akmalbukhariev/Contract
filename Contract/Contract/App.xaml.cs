using Contract.Resources;
using System;
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

            MainPage = new Pages.PageMasterDetail();
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
