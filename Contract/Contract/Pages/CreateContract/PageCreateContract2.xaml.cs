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
    public partial class PageCreateContract2 : IPage
    {
        private bool yes1 = false;
        public PageCreateContract2()
        {
            InitializeComponent();
            SetModel(new ViewModel.BaseModel());
            YesNo1_Tapped(null, null);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Model.Parent = Parent;

            lbStep.Text = RSC.Step + " #2"; 
        }
        private void YesNo1_Tapped(object sender, EventArgs e)
        {
            if (yes1)
            {
                imYesNo1.Source = GetYesNoIcon(false);
                yes1 = false;
                entTax.IsVisible = false;
            }
            else
            {
                imYesNo1.Source = GetYesNoIcon(true);
                yes1 = true;
                entTax.IsVisible = true;
            }

            if (sender != null)
                ControlApp.Vibrate();
        }
         
        private void Minus_Clicked(object sender, EventArgs e)
        {
            ChangeBoxColor(boxMinus);
            
            if (lbAmount.Text == "0") return;

            lbAmount.Text = (int.Parse(lbAmount.Text) - 1).ToString();
        }

        private void Plus_Clicked(object sender, EventArgs e)
        {
            ChangeBoxColor(boxPlus);
            lbAmount.Text = (int.Parse(lbAmount.Text) + 1).ToString();
        }

        private void Add_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView(boxAdd);
            ClickAnimationView(stackAdd);
        }

        private void Copy_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView(boxCopy);
            ClickAnimationView(stackCopy);
        }

        private void Delete_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView(boxDelete);
            ClickAnimationView(stackDelete);
        }

        async void ChangeBoxColor(BoxView boxView)
        {
            boxView.BackgroundColor = Color.White;
            await Task.Delay(100);

            boxView.BackgroundColor = Color.FromHex("#E6E6E6");
            await Task.Delay(200);
        }

        private async void Finished_Clicked(object sender, EventArgs e)
        {
            Model.SetTransitionType();
            await Navigation.PushAsync(new PageCreateContract3());
        }
    }
}