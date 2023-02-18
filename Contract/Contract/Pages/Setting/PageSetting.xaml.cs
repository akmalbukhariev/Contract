using LibContract.HttpModels;
using LibContract.HttpResponse;
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

            navigationBar.Title = RSC.Settings;
            lbLanguage.Text = RSC.Language;
            lbSignature.Text = RSC.YourSignature;
            lbNotification.Text = RSC.Notification;

            //lbTemplate.Text = RSC.SelectDefaultTemplate;
            //lbNight.Text = RSC.NightView;
            //lbAbout.Text = RSC.About;

            //imYesNo1.Source = GetYesNoIcon(true);
            //imYesNo2.Source = GetYesNoIcon(true);

            if (ControlApp.UserInfo.on_notification == 0)
            {
                imYesNo1.Source = GetYesNoIcon(false);
                yes1 = false;
            }
            else
            {
                imYesNo1.Source = GetYesNoIcon(true);
                yes1 = true;
            }
        }

        private async void YesNo1_Tapped(object sender, EventArgs e)
        {
            if (yes1)
            {
                imYesNo1.Source = GetYesNoIcon(false);
                yes1 = false;
            }
            else
            {
                imYesNo1.Source = GetYesNoIcon(true);
                yes1 = true;
            }

            ControlApp.Vibrate();

            User data = new User()
            {
                phone_number = ControlApp.UserInfo.phone_number,
                on_notification = yes1 ? 1 : 0
            };

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseLogin response = await Net.HttpService.SetNotificationOnOff(data);
            ControlApp.CloseLoadingView();

            if (!ControlApp.CheckResponse(response)) return;

            if (response.result)
                ControlApp.UserInfo.on_notification = data.on_notification;
            else
                await DisplayAlert(RSC.Notification, RSC.Failed, RSC.Ok);
        }

        //private void YesNo2_Tapped(object sender, EventArgs e)
        //{
        //    if (yes2)
        //    {
        //        imYesNo2.Source = GetYesNoIcon(false);
        //        yes2 = false;
        //    }
        //    else
        //    {
        //        imYesNo2.Source = GetYesNoIcon(true);
        //        yes2 = true;
        //    }

        //    ControlApp.Vibrate();
        //}

        private async void TableCell_Tapped(object sender, EventArgs e)
        {
            Model.SetTransitionType();
            if (sender == cellLanguage)
            {
                await Navigation.PushAsync(new PageLanguage(true));
            } 
            else if (sender == cellSignature)
            {
                await Navigation.PushAsync(new UnapprovedContracts.PageSign());
            }
            //else if (sender == cellAbout)
            //{
            //    await Navigation.PushAsync(new PageAbout());
            //} 
        }
    }
}