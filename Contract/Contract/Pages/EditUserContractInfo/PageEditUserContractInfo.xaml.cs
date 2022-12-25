﻿using Contract.ViewModel.Pages.EditUserContractInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.EditUserContractInfo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageEditUserContractInfo : IPage
    {
        private bool yes1 = true;
        private bool yes2 = true;
        private bool yes3 = true;

        public PageEditUserContractInfo()
        {
            InitializeComponent();

            SetModel(new PageEditUserContractInfoViewModel(Navigation));
            PModel.EventRequestInfoFinished += EventRequestInfoFinished;

            imYesNo1.Source = GetYesNoIcon(true);
            imYesNo2.Source = GetYesNoIcon(true);
            imYesNo3.Source = GetYesNoIcon(true);
        }
         
        protected override void OnAppearing()
        {
            base.OnAppearing();
            PModel.PositionList = GetPositionList;
            PModel.DocumentList = GetDocumentList;
            PModel.RequestInfo(); 
        }

        private void EventRequestInfoFinished()
        {
            yes1 = !PModel.AreYouQQSPayer;
            yes2 = !PModel.IsAccountProvided;
            yes3 = !PModel.IsCounselProvided;

            Device.BeginInvokeOnMainThread(new Action(() =>
            {
                YesNo1_Tapped(null, null);
                YesNo2_Tapped(null, null);
                YesNo3_Tapped(null, null);
            }));
        }

        private void YesNo1_Tapped(object sender, EventArgs e)
        {
            if (yes1)
            {
                imYesNo1.Source = GetYesNoIcon(false);
                yes1 = false;
                stackYesNo1.IsVisible = false;
            }
            else
            {
                imYesNo1.Source = GetYesNoIcon(true);
                yes1 = true;
                stackYesNo1.IsVisible = true;
            }

            if (sender != null)
                ControlApp.Vibrate();

            PModel.AreYouQQSPayer = yes1;
        }

        private void YesNo2_Tapped(object sender, EventArgs e)
        {
            if (yes2)
            {
                imYesNo2.Source = GetYesNoIcon(false);
                yes2 = false;
                stackYesNo2.IsVisible = false;
            }
            else
            {
                imYesNo2.Source = GetYesNoIcon(true);
                yes2 = true;
                stackYesNo2.IsVisible = true;
            }

            if (sender != null)
                ControlApp.Vibrate();

            PModel.IsAccountProvided = yes2;
        }

        private void YesNo3_Tapped(object sender, EventArgs e)
        {
            if (yes3)
            {
                imYesNo3.Source = GetYesNoIcon(false);
                yes3 = false;
                stackYesNo3.IsVisible = false;
            }
            else
            {
                imYesNo3.Source = GetYesNoIcon(true);
                yes3 = true;
                stackYesNo3.IsVisible = true;
            }

            if (sender != null)
                ControlApp.Vibrate();

            PModel.IsCounselProvided = yes3;
        }

        private PageEditUserContractInfoViewModel PModel
        {
            get
            {
                return Model as PageEditUserContractInfoViewModel;
            }
        }
    }
}