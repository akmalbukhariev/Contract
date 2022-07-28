using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.Setting
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSetting : IPage
    {
        private bool yes1 = false;
        private bool yes2 = true;

        public PageSetting()
        {
            InitializeComponent();

            SetModel(new ViewModel.BaseModel());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Model.Parent = Parent;
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

        private async void TableCell_Tapped(object sender, EventArgs e)
        {
            Model.SetTransitionType();
            if (sender == cellLanguage)
            {
                await Navigation.PushAsync(new PageLanguage(true));
            }
            else if (sender == cellAbout)
            {
                await Navigation.PushAsync(new PageAbout());
            }
        }
    }
}