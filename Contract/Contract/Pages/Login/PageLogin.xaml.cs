using Contract.ViewModel.Pages.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageLogin : IPage
    {
        public PageLogin()
        {
            InitializeComponent();
            SetModel(new PageLoginViewModel(Navigation));

            entId.Entry.ClearButtonVisibility = ClearButtonVisibility.WhileEditing;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Model.Parent = Parent;
        }
        private async void AutoLogin_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (!(Model as PageLoginViewModel).CheckAutoLogin) return;

            bool res = await DisplayAlert(RSC.AutoLogin, RSC.AutoLogMessage, RSC.Ok, RSC.Cancel);
            if (!res)
            {
                (Model as PageLoginViewModel).CheckAutoLogin = false;
                Preferences.Set("AutoLogin", "");
                return;
            }
        }

        private void ClickFindId(object sender, EventArgs e)
        {
            ChangeClickBackColor(lbFindId, Color.White, Color.FromHex("#3F6C6C"));
        }

        private void ClickFindPassword(object sender, EventArgs e)
        {
            ChangeClickBackColor(lbFindPassword, Color.White, Color.FromHex("#3F6C6C"));
        }
    }   
}