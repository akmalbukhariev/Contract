﻿using Contract.ViewModel.Pages.ChangePassword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.ChangePassword
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageChangePassword : IPage
    {
        public PageChangePassword()
        {
            InitializeComponent();

            SetModel(new PageChangePasswordViewModel(Navigation));
        }
    }
}