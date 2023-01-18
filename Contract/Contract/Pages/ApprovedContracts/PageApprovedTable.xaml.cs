using Contract.Interfaces;
using Contract.Model;
using Contract.Net;
using Contract.Pages.CanceledContracts;
using Contract.ViewModel.Pages.CurrentContracts;
using LibContract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.ApprovedContracts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageApprovedTable : IPage
    { 
        public PageApprovedTable()
        {
            InitializeComponent();

            SetModel(new PageApprovedTableViewModel());

            for (int i = 0; i < grHeader.ColumnDefinitions.Count; i++)
            {
                listView.WidthRequest += grHeader.ColumnDefinitions[i].Width.Value;
            }
            listView.WidthRequest += 70;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Model.Parent = Parent;

            //if (ControlApp.CheckUserCompany())
            if (ControlApp.UserCompanyInfo == null)
            {

            }
            else
            {
                PModel.RequestInfo();
            }

            DependencyService.Get<IRotationService>().EnableRotation();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            DependencyService.Get<IRotationService>().DisableRotation();
        }

        private async void Eye_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
            ControlApp.Vibrate();

            ApprovedContract item = (ApprovedContract)((Image)sender).BindingContext;
            if (item == null) return;

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseCreatePdf response = await Net.HttpService.CreateContractPdf(item.ContractNnumberReal);
            if (response.result)
            {
                await Navigation.PushAsync(new CreateContract.PageShowContract($"{HttpService.DATA_URL}{response.pdf_url}"));
                //await DisplayAlert(RSC.CreateContract, RSC.SuccessfullyCompleted, RSC.Ok);
            }
            else
            {
                await DisplayAlert(RSC.CreateContract, response.message, RSC.Ok);
            }
            ControlApp.CloseLoadingView();
        }
         
        private async void Send_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
            ControlApp.Vibrate();

            ApprovedContract item = (ApprovedContract)((Image)sender).BindingContext;
            if (item == null) return;

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseCreatePdf response2 = await HttpService.CreateContractPdf(item.ContractNnumberReal);
            ControlApp.CloseLoadingView();
            if (response2.result)
            {
                await ControlApp.ShareUri($"{HttpService.DATA_URL}{response2.pdf_url}");
            }
            else
            {
                await DisplayAlert(RSC.CreateContract, RSC.Failed, RSC.Ok);
            }
        }

        private async void Cancel_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
            ControlApp.Vibrate();

            ApprovedContract item = (ApprovedContract)((Image)sender).BindingContext;
            if (item == null) return;

            LibContract.HttpModels.CreateContractInfo canceledContract = new LibContract.HttpModels.CreateContractInfo()
            { 
                contract_number = item.ContractNnumberReal,
                comment = ""
            };

            Model.SetTransitionType(TransitionType.SlideFromBottom);
            await Navigation.PushModalAsync(new PageCancelContract(canceledContract, RSC.ApprovedContracts1));
        }

        private PageApprovedTableViewModel PModel
        {
            get
            {
                return Model as PageApprovedTableViewModel;
            }
        }
    }
}