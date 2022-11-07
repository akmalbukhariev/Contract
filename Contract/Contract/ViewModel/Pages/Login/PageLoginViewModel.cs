using Contract.Control;
using Contract.HttpResponse;
using Contract.Net;
using Contract.Pages;
using Contract.Pages.SignUp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Contract.ViewModel.Pages.Login
{
    public class PageLoginViewModel : BaseModel
    {
        public bool CheckAutoLogin { get => GetValue<bool>(); set => SetValue(value); }
        public string PhoneNumber { get => GetValue<string>(); set => SetValue(value); }
        public string Password { get => GetValue<string>(); set => SetValue(value); }
        public PageLoginViewModel(INavigation navigation) : base(navigation)
        {
            
        }

        public ICommand CommandLogin => new Command(Login);
        public ICommand CommandSignUp => new Command(SignUp);

        public async void Login()
        {
            if (!ControlApp.InternetOk()) return;

            ControlApp.ShowLoadingView(RSC.PleaseWait);

            var data = new HttpModels.Login()
            {
                phone_number = PhoneNumber,
                password = Password
            };
            ResponseLogin response = await HttpService.Login(data);
            ControlApp.CloseLoadingView();

            if (!response.result)
            {
                await Application.Current.MainPage.DisplayAlert(RSC.Login, RSC.Login_Message_1, RSC.Ok);
                return;
            }

            ControlApp.UserInfo = response.data;
            Application.Current.MainPage = new TransitionNavigationPage(new PageMasterDetail());

            if (CheckAutoLogin)
                Preferences.Set("AutoLogin", $"{PhoneNumber}/{Password}");
            else
                Preferences.Set("AutoLogin", "");
        }

        private async void SignUp()
        {
            SetTransitionType(TransitionType.SlideFromRight);
            await Navigation.PushAsync(new PagePhoneNumber()); 
        }
    }
}
