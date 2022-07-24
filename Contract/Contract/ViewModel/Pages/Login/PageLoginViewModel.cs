using Contract.Control;
using Contract.Pages;
using Contract.Pages.SignUp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contract.ViewModel.Pages.Login
{
    public class PageLoginViewModel : BaseModel
    {
        public PageLoginViewModel(INavigation navigation) : base(navigation)
        {
            
        }

        public ICommand CommandLogin => new Command(Login);
        public ICommand CommandSignUp => new Command(SignUp);

        private void Login()
        {
             Application.Current.MainPage = new TransitionNavigationPage(new PageMasterDetail()); 
        }

        private async void SignUp()
        {
            SetTransitionType(TransitionType.SlideFromRight);
            await Navigation.PushAsync(new PagePhoneNumber()); 
        }
    }
}
