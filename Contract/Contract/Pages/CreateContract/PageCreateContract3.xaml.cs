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
        private bool yes1 = true;
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

            YesNo1_Tapped(null, null);
            YesNo2_Tapped(null, null);
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
                ShowMenu1(true);
                if (!yes2)
                {
                    ShowMenu2(true);
                    stackCompanyName.IsVisible = false;
                }
            }
            else
            {
                imYesNo1.Source = "uz_Yes";
                yes1 = true;
                ShowMenu1(false);
                ShowMenu2(false);
            } 
        }

        private void YesNo2_Tapped(object sender, EventArgs e)
        {
            if (yes2)
            {
                imYesNo2.Source = "uz_No";
                yes2 = false;
                ShowMenu2(true);
                stackCompanyName.IsVisible = false;
            }
            else
            {
                imYesNo2.Source = "uz_Yes";
                yes2 = true;
                ShowMenu2(false);
                stackCompanyName.IsVisible = true;
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

        void ShowMenu1(bool show)
        {
            stackYesNo2.IsVisible = show;
            stackCompanyName.IsVisible = show; 
        }

        void ShowMenu2(bool show)
        {
            lbTitleBold.IsVisible = show;
            stack1.IsVisible = show;
            stack2.IsVisible = show;
            stack3.IsVisible = show;
            stack4.IsVisible = show;
            stack5.IsVisible = show;
            stack6.IsVisible = show;
            stack7.IsVisible = show;
            stack8.IsVisible = show;
            stack9.IsVisible = show;
            stack10.IsVisible = show;
            stack11.IsVisible = show;
            stack12.IsVisible = show;
            stack13.IsVisible = show;
            stack14.IsVisible = show;
            stack15.IsVisible = show;
            viewExplan.IsVisible = show;
        }
    }
}