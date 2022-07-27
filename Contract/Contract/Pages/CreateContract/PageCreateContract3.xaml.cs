using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.CreateContract
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCreateContract3 : IPage
    {
        private bool yes1 = false;
        private bool yes2 = true;
        private bool yes3 = true;
        private bool yes4 = true;
        private bool yes5 = true;
        public PageCreateContract3()
        {
            InitializeComponent();
            SetModel(new ViewModel.BaseModel());

            lbYesNo1.Text = RSC.Question1;
            lbYesNo2.Text = RSC.Question2;
            lbTitleBold.Text = RSC.Question3;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Model.Parent = Parent;
            lbStep.Text = RSC.Step + " #1";
        }

        private void YesNo1_Tapped(object sender, EventArgs e)
        {
            if (yes1)
            {
                imYesNo1.Source = "uz_No";
                yes1 = false;
            }
            else
            {
                imYesNo1.Source = "uz_Yes";
                yes1 = true;
            }
        }

        private void YesNo2_Tapped(object sender, EventArgs e)
        {
            if (yes2)
            {
                imYesNo2.Source = "uz_No";
                yes2 = false;
            }
            else
            {
                imYesNo2.Source = "uz_Yes";
                yes2 = true;
            }
        }

        private void YesNo3_Tapped(object sender, EventArgs e)
        {
            if (yes3)
            {
                imYesNo3.Source = "uz_No";
                yes3 = false;
            }
            else
            {
                imYesNo3.Source = "uz_Yes";
                yes3 = true;
            }
        }

        private void YesNo4_Tapped(object sender, EventArgs e)
        {
            if (yes4)
            {
                imYesNo4.Source = "uz_No";
                yes4 = false;
            }
            else
            {
                imYesNo4.Source = "uz_Yes";
                yes4 = true;
            }
        }

        private void YesNo5_Tapped(object sender, EventArgs e)
        {
            if (yes5)
            {
                imYesNo5.Source = "uz_No";
                yes5 = false;
            }
            else
            {
                imYesNo5.Source = "uz_Yes";
                yes5 = true;
            }
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            Model.SetTransitionType();
            await Navigation.PushAsync(new PageCreateContract4());
        }
    }
}