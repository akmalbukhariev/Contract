using LibContract.HttpResponse;
using Contract.Interfaces;
using Contract.ViewModel.Pages.ContractNumber;
using Contract.ViewModel.Pages.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.ContractNumber
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageContractNumberList : IPage
    { 
        public PageContractNumberList()
        {
            InitializeComponent();

            SetModel(new PageContractNumberListViewModel()); 

            for (int i = 0; i < grHeader.ColumnDefinitions.Count; i++)
            {
                listView.WidthRequest += grHeader.ColumnDefinitions[i].Width.Value;
            }
            listView.WidthRequest += 50;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            PModel.RequestInfo();
            DependencyService.Get<IRotationService>().EnableRotation();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            DependencyService.Get<IRotationService>().DisableRotation();
        }

        private void Send_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
            ControlApp.Vibrate();

            if (!ControlApp.InternetOk()) return;
        }

        private async void Edit_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
            ControlApp.Vibrate();

            if (!ControlApp.InternetOk()) return;

            Model.ContractNumber item = (Model.ContractNumber)((Image)sender).BindingContext;
            if (item == null) return;

            await Navigation.PushAsync(new PageEditContractNumber(item));
        }

        private async void Cancel_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
            ControlApp.Vibrate();

            if (!ControlApp.InternetOk()) return;

            if (!await Application.Current.MainPage.DisplayAlert(RSC.ContractNumber, RSC.DeleteMessage, RSC.Ok, RSC.Cancel)) return;
            
            Model.ContractNumber item = (Model.ContractNumber)((Image)sender).BindingContext;
            if (item == null) return;

            LibContract.HttpModels.ContractNumberTemplate data = new LibContract.HttpModels.ContractNumberTemplate()
            {
                id = item.Id,
                user_phone_number = ControlApp.UserInfo.phone_number,
                option = item.ContractNumberText.Replace(Constants.ContractSequenceNumber, "").Replace("-", ""),
                format = item.Format,
                created_date = item.CreatedDate,
                is_deleted = 1
            };

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseContractNumberTemplate response = await Net.HttpService.UpdateContractNumber(data);
            ControlApp.CloseLoadingView();

            if (!ControlApp.CheckResponse(response)) return;

            string strMessage = response.result ? RSC.SuccessfullyCompleted : RSC.Failed;
            await DisplayAlert(RSC.ContractNumber, strMessage, RSC.Ok);

            PModel.RequestInfo();
        }

        public PageContractNumberListViewModel PModel
        {
            get
            {
                return Model as PageContractNumberListViewModel;
            }
        }
    }
}