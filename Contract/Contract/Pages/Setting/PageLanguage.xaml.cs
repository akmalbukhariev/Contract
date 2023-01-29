using Contract.Resources;
using Contract.Views;
using LibContract.HttpModels;
using LibContract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.Setting
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageLanguage : IPage
    {
        private bool _showNavigation = false;
        public PageLanguage(bool showNavigation = false)
        {
            InitializeComponent();

            SetModel(new ViewModel.BaseModel());

            _showNavigation = showNavigation;
            boxNavigation.IsVisible = showNavigation;
            navigationBar.IsVisible = showNavigation;

            if (!showNavigation)
            {
                grMain.RowDefinitions[0].Height = 10;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            lbAppVersion.Text = RSC.AppVersion + " " + ControlApp.AppVersion;
        }

        private async void Item_Tapped(object sender, EventArgs e)
        { 
            viewUzbKiril.UnSelected();
            viewUzbLatin.UnSelected();
            viewRus.UnSelected();
            viewEng.UnSelected();

            ViewLanguage view = (ViewLanguage)sender;
             
            await view.ScaleTo(0.8, 200);
            view.Selected();
            await view.ScaleTo(1, 200, Easing.SpringOut);

            Setlanguage(sender);

            if (_showNavigation)
            {
                await Navigation.PopAsync();
            }
            else
            {
                Model.SetTransitionType();
                await Navigation.PushAsync(new Introduction.PageIntroduction());
            }
        }

        private async void Setlanguage(object sender)
        { 
            string strLanguage = GetLatinLanguageName(sender);
            AppSettings.SetLanguage(strLanguage);

            ControlApp.LanguageId = strLanguage;

            if (ControlApp.UserInfo == null) return;

            User user = new User()
            {
                phone_number = ControlApp.UserInfo.phone_number,
                lan_id = strLanguage
            };

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseLogin response = await Net.HttpService.UpdateLanguageCode(user);
            ControlApp.CloseLoadingView();

            if (!ControlApp.CheckResponse(response)) return;

            navigationBar.Title = RSC.Language;
            lbAppVersion.Text = RSC.AppVersion + " " + ControlApp.AppVersion;
        }

        private string GetLatinLanguageName(object sender)
        {
            string strResult = string.Empty;

            if (sender == viewUzbKiril)
            {
                strResult = "Uzbek (Cyrillic)";
            }
            else if (sender == viewUzbLatin)
            {
                strResult = "Uzbek";
            }
            else if (sender == viewRus)
            {
                strResult = "Russian";
            }
            else if (sender == viewEng)
            {
                strResult = "English";
            }

            return strResult;
        }
    }
}