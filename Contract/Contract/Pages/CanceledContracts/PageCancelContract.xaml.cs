﻿using LibContract.HttpModels;
using Contract.ViewModel.Pages.CanceledContracts;
using Contract.ViewModel.Pages.CreateContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.CanceledContracts
{ 
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCancelContract : IPage
    {
        public PageCancelContract(CreateContractInfo contractInfo, string pageTitle, bool moveToMainPage = false)
        {
            InitializeComponent();
            
            navBar.Title = pageTitle;
            lbSpan1.Text = pageTitle.Contains(RSC.Approved) ? RSC.Approved : RSC.Unapproved;

            SetModel(new PageCancelContractViewModel(contractInfo, Navigation, moveToMainPage));
        }

        private PageCancelContractViewModel PModel
        {
            get
            {
                return Model as PageCancelContractViewModel;
            }
        }
    }
}