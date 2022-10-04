using Contract.ViewModel.Pages.Customers;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.Customers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageAddOrEditCustomer : IPage
    {
        private bool yes1 = true;
        private bool yes2 = true;
        private bool yes3 = true;

        private bool IsThisEditClient = false;

        public PageAddOrEditCustomer(bool isThisEditClient = false)
        {
            InitializeComponent();

            IsThisEditClient = isThisEditClient;

            SetModel(new PageAddOrEditCustomerViewModel(Navigation));

            imYesNo1.Source = GetYesNoIcon(true);
            imYesNo2.Source = GetYesNoIcon(true);
            imYesNo3.Source = GetYesNoIcon(true);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (IsThisEditClient)
            {
                PModel.SetData();
            }
        }

        private void YesNo1_Tapped(object sender, EventArgs e)
        {
            if (yes1)
            {
                imYesNo1.Source = GetYesNoIcon(false);
                yes1 = false;
                stackYesNo1.IsVisible = false;
            }
            else
            {
                imYesNo1.Source = GetYesNoIcon(true);
                yes1 = true;
                stackYesNo1.IsVisible = true;
            }

            ControlApp.Vibrate();
        }

        private void YesNo2_Tapped(object sender, EventArgs e)
        {
            if (yes2)
            {
                imYesNo2.Source = GetYesNoIcon(false);
                yes2 = false;
                stackYesNo2.IsVisible = false;
            }
            else
            {
                imYesNo2.Source = GetYesNoIcon(true);
                yes2 = true;
                stackYesNo2.IsVisible = true;
            }

            ControlApp.Vibrate();
        }

        private void YesNo3_Tapped(object sender, EventArgs e)
        {
            if (yes3)
            {
                imYesNo3.Source = GetYesNoIcon(false);
                yes3 = false;
                stackYesNo3.IsVisible = false;
            }
            else
            {
                imYesNo3.Source = GetYesNoIcon(true);
                yes3 = true;
                stackYesNo3.IsVisible = true;
            }

            ControlApp.Vibrate();
        }

        private async void Logotip_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);

            string[] strButtons = new string[] { RSC.ChooseImage, RSC.TakePicture };
            string action = await Application.Current.MainPage.DisplayActionSheet(RSC.Image, RSC.Cancel, null, strButtons);
            if (string.IsNullOrEmpty(action) || action.Equals(RSC.Cancel)) return;

            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported || !CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert(RSC.NotSupported, RSC.DeviceMessage1, RSC.Ok);
                return;
            }

            if (action.Equals(RSC.ChooseImage))
            {
                var mediaOption = new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Medium
                };

                var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOption);
                if (selectedImageFile != null)
                {
                    PModel.LogoImage = ImageSource.FromStream(() => selectedImageFile.GetStream());
                    PModel.LogoImagePath = selectedImageFile.Path;
                }
            }
            else
            {
                var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions());

                if (photo != null)
                {
                    PModel.LogoImage = photo.Path;
                    PModel.LogoImagePath = photo.Path; 
                }
            } 
        }

        private void Finished_Clicked(object sender, EventArgs e)
        {
            if (IsThisEditClient)
            {
                PModel.RequestUpdateInfo();
            }
            else
            {
                PModel.RequestAddInfo();
            }
        }

        private PageAddOrEditCustomerViewModel PModel
        {
            get
            {
                return Model as PageAddOrEditCustomerViewModel;
            }
        }
    }
}