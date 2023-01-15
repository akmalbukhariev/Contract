using Contract.Notification;
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
        bool appeared = false;
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

            if (ControlApp.AppOnResume)
            {
                ControlApp.AppOnResume = false;
                return;
            }

            appeared = false;
            PModel.CheckAutoLogin = false;
            string[] ll = Preferences.Get("AutoLogin", "").Split('/');
            if (ll.Length == 2)
            {
                PModel.PhoneNumber= ll[0];
                PModel.Password = ll[1];
                PModel.CheckAutoLogin = true;

                appeared = true;
                PModel.Login();
            }
            else
            {
                appeared = true;
            }
        }

        private async void AutoLogin_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (!appeared) return;

            if (!PModel.CheckAutoLogin) return;

            bool res = await DisplayAlert(RSC.AutoLogin, RSC.AutoLogMessage, RSC.Ok, RSC.Cancel);
            if (!res)
            {
                PModel.CheckAutoLogin = false;
                Preferences.Set("AutoLogin", "");
                return;
            }
        }

        private void ClickFindPassword(object sender, EventArgs e)
        {
            
        }

        PageLoginViewModel PModel
        {
            get
            {
                return Model as PageLoginViewModel;
            }
        }
    }   
}