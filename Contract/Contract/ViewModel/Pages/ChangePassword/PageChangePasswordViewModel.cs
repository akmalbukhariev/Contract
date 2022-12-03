using Contract.Control;
using LibContract.HttpResponse;
using Contract.Pages.Login;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contract.ViewModel.Pages.ChangePassword
{
    public class PageChangePasswordViewModel : BaseModel
    {
        public string EnterCurrentPassword { get => GetValue<string>(); set => SetValue(value); }
        public string EnterNewPassword { get => GetValue<string>(); set => SetValue(value); }
        public string RepeatNewPassword { get => GetValue<string>(); set => SetValue(value); }

        public PageChangePasswordViewModel(INavigation navigation) : base(navigation)
        {
            EnterCurrentPassword = "";
            EnterNewPassword = "";
            RepeatNewPassword = "";
        }

        public ICommand CommandSave => new Command(Update);

        async void Update()
        {
            if (string.IsNullOrEmpty(EnterNewPassword) || string.IsNullOrEmpty(EnterNewPassword) || string.IsNullOrEmpty(RepeatNewPassword))
            {
                await Application.Current.MainPage.DisplayAlert(RSC.Password, RSC.FieldEmpty, RSC.Ok);
                return;
            }
             
            if (!EnterNewPassword.Equals(RepeatNewPassword))
            {
                await Application.Current.MainPage.DisplayAlert(RSC.Password, RSC.PleaseCheckNewPassword, RSC.Ok);
                return;
            }

            LibContract.HttpModels.ChnagePassword user = new LibContract.HttpModels.ChnagePassword()
            {
                phone_number = ControlApp.UserInfo.phone_number,
                password = EnterCurrentPassword,
                new_password = EnterNewPassword
            };

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseLogin response = await Net.HttpService.UpdateUserPassword(user);
            ControlApp.CloseLoadingView();

            string strMessage = response.result ? RSC.SuccessfullyUpdated : $"{RSC.Failed} : {RSC.WrongCurrentPassword}";
            
            await Application.Current.MainPage.DisplayAlert(RSC.Password, strMessage, RSC.Ok);
            if (response.result)
                Application.Current.MainPage = new TransitionNavigationPage(new PageLogin());
        }
    }
}
