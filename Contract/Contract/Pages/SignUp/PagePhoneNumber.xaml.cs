using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.SignUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagePhoneNumber : IPage
    {
        public PagePhoneNumber()
        {
            InitializeComponent();
            SetModel(new ViewModel.BaseModel(Navigation));
            navigationBar.Title = RSC.SignUp;
            btnNext.Text = RSC.ButtonNext;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Model.Parent = Parent;
        }

        private async void Next_Clicked(object sender, EventArgs e)
        {
            Model.SetTransitionType();
            await Navigation.PushAsync(new PageNewPassword());
        }
    }
}