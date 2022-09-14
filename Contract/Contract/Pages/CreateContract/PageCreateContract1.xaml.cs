using Contract.ViewModel.Pages.CreateContract;
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
    public partial class PageCreateContract1 : IPage
    {
        private bool yes1 = true;
        private bool yes2 = true;
        private bool yes3 = true;
        private bool yes4 = true;
        private bool yes5 = true;
        public PageCreateContract1()
        {
            InitializeComponent();
            SetModel(new PageCreateContract1ViewModel(Navigation));

            lbYesNo1.Text = RSC.Question1;
            lbYesNo2.Text = RSC.Question2;
            lbTitleBold.Text = RSC.Question3;

            YesNo1_Tapped(null, null);
            YesNo2_Tapped(null, null);

            //imYesNo1.Source = GetYesNoIcon(true);
            //imYesNo2.Source = GetYesNoIcon(true);
            imYesNo3.Source = GetYesNoIcon(true);
            imYesNo4.Source = GetYesNoIcon(true);
            imYesNo5.Source = GetYesNoIcon(true);
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
                imYesNo1.Source = GetYesNoIcon(false);
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
                imYesNo1.Source = GetYesNoIcon(true);
                yes1 = true;
                ShowMenu1(false);
                ShowMenu2(false);
            }

            if (sender != null)
                ControlApp.Vibrate();

            PModel.OpenClientInfo = yes1;
        }

        private void YesNo2_Tapped(object sender, EventArgs e)
        {
            if (yes2)
            {
                imYesNo2.Source = GetYesNoIcon(false);
                yes2 = false;
                ShowMenu2(true);
                stackCompanyName.IsVisible = false;
            }
            else
            {
                imYesNo2.Source = GetYesNoIcon(true);
                yes2 = true;
                ShowMenu2(false);
                stackCompanyName.IsVisible = true;
            }

            if (sender != null)
                ControlApp.Vibrate();

            PModel.OpenSearchClient = yes1;
        }

        private void YesNo3_Tapped(object sender, EventArgs e)
        {
            if (yes3)
            {
                imYesNo3.Source = GetYesNoIcon(false);
                yes3 = false;
                stack8.IsVisible = false;
            }
            else
            {
                imYesNo3.Source = GetYesNoIcon(true);
                yes3 = true;
                stack8.IsVisible = true;
            }

            ControlApp.Vibrate();
            PModel.AreYouTaxPayer = yes3;
        }

        private void YesNo4_Tapped(object sender, EventArgs e)
        {
            if (yes4)
            {
                imYesNo4.Source = GetYesNoIcon(false);
                yes4 = false;
                stack13.IsVisible = false;
            }
            else
            {
                imYesNo4.Source = GetYesNoIcon(true);
                yes4 = true;
                stack13.IsVisible = true;
            }

            ControlApp.Vibrate();
            PModel.IsAccountProvided = yes4;
        }

        private void YesNo5_Tapped(object sender, EventArgs e)
        {
            if (yes5)
            {
                imYesNo5.Source = GetYesNoIcon(false);
                yes5 = false;
                stack15.IsVisible = false;
            }
            else
            {
                imYesNo5.Source = GetYesNoIcon(true);
                yes5 = true;
                stack15.IsVisible = true;
            }

            ControlApp.Vibrate();
            PModel.IsCounselProvided = yes5;
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

        private PageCreateContract1ViewModel PModel
        {
            get
            {
                return Model as PageCreateContract1ViewModel;
            }
        }
    }
}