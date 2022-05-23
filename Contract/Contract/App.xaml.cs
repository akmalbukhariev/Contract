﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Pages.PageLanguage();
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
