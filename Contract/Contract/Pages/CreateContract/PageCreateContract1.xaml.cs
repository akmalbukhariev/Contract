﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.CreateContract
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCreateContract1 : IPage
    {
        private bool yes = true;
        public PageCreateContract1()
        {
            InitializeComponent();

            lbYesNo.Text = "Mijozning (kontragent) \n rekvizitlari ochiq qolsinmi?";
        }

        private void YesNo_Tapped(object sender, EventArgs e)
        {
            if (yes)
            {
                imYesNo.Source = "uz_No";
                yes = false;
            }
            else
            {
                imYesNo.Source = "uz_Yes";
                yes = true;
            }
        }
    }
}