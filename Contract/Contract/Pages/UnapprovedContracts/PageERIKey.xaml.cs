﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.UnapprovedContracts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageERIKey : IPage
    {
        public PageERIKey()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            lbCompanyName.Text = RSC.CompanyName + " :";
            lbChiefName.Text = RSC.ChiefName + " :";
            lbCompanySTIRi.Text = RSC.CompanySTIRi + " :";
        }
    }
}