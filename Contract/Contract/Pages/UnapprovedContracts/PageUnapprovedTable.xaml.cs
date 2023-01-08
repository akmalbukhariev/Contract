using Acr.UserDialogs;
using Contract.Interfaces;
using Contract.Model;
using Contract.Net;
using Contract.Pages.CanceledContracts;
using Contract.ViewModel.Pages.UnapprovedContracts;
using LibContract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.UnapprovedContracts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageUnapprovedTable : IPage
    {
        string contractNumberReal = "";
        public PageUnapprovedTable()
        {
            InitializeComponent();

            SetModel(new PageUnapprovedTableViewModel(Navigation));
             
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
                PModel.RequestInfo();

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

            UnapprovedContract item = (UnapprovedContract)((Image)sender).BindingContext;
            if (item == null) return;

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseCreatePdf response = await Net.HttpService.CreateContractPdf(item.ContractNnumberReal);
            if (response.result)
            {
                await DisplayAlert(RSC.CreateContract, RSC.SuccessfullyCompleted, RSC.Ok);
            }
            else
            {
                await DisplayAlert(RSC.CreateContract, response.message, RSC.Ok);
            }
            ControlApp.CloseLoadingView();
        }

        private async void Check_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender); 
            ControlApp.Vibrate();

            UnapprovedContract item = (UnapprovedContract)((Image)sender).BindingContext;
            if (item == null) return;

            bool res = await Application.Current.MainPage.DisplayAlert(RSC.Approve, RSC.ApproveMessage, RSC.Ok, RSC.Cancel);
            if (res)
            {
                contractNumberReal = item.ContractNnumberReal;
                PModel.ShowConfirmBox = true;
            }
        }

        private async void Send_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
            ControlApp.Vibrate();

            UnapprovedContract item = (UnapprovedContract)((Image)sender).BindingContext;
            if (item == null) return;

            ResponseCreatePdf response2 = await HttpService.CreateContractPdf(item.ContractNnumberReal);
            if (response2.result)
            { 
                //await DisplayAlert(RSC.CreateContract, RSC.SuccessfullyCompleted, RSC.Ok);
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

            UnapprovedContract item = (UnapprovedContract)((Image)sender).BindingContext;
            if (item == null) return;

            LibContract.HttpModels.CreateContractInfo canceledContract = new LibContract.HttpModels.CreateContractInfo()
            {
                contract_number = item.ContractNnumberReal,
                comment = ""
            };

            Model.SetTransitionType(TransitionType.SlideFromBottom);
            await Navigation.PushModalAsync(new PageCancelContract(canceledContract, RSC.UnapprovedContracts1));
        }
         
        private async void Button_Clicked(object sender, EventArgs e)
        { 
            if (btnYes == sender)
            {
                if (string.IsNullOrEmpty(entText.Text.Trim()))
                {
                    await DisplayAlert(RSC.PhoneNumber, RSC.FieldEmpty, RSC.Ok);
                    return;
                }

                PModel.ShowConfirmBox = false;

                ControlApp.ShowLoadingView(RSC.PleaseWait);
                ResponseUser response1 = await HttpService.GetUser(entText.Text);
                if (!response1.result)
                {
                    ControlApp.CloseLoadingView();
                    await DisplayAlert(RSC.ContractNumber, RSC.CouldNotFindContragent, RSC.Ok);
                    return;
                }

                LibContract.HttpModels.ApprovedUnapprovedContract request = new LibContract.HttpModels.ApprovedUnapprovedContract()
                {
                    contract_number = contractNumberReal,
                    user_phone_number = ControlApp.UserInfo.phone_number,
                    contragent_phone_number = entText.Text.Trim(),
                    user_stir = ControlApp.UserCompanyInfo.stir_of_company,
                    is_approved = 1
                };

                ResponseApprovedUnapprovedContract response2 = await HttpService.SetApprovedContract(request);
                
                string strMessage = response2.result ? RSC.SuccessfullyCompleted : RSC.Failed;
                await DisplayAlert(RSC.Approve, strMessage, RSC.Ok);

                if(response2.result)
                    PModel.RequestInfo();

                ControlApp.CloseLoadingView();
            }
            else if (btnNo == sender)
            {
                PModel.ShowConfirmBox = false;
            }
        }

        private PageUnapprovedTableViewModel PModel
        {
            get
            {
                return Model as PageUnapprovedTableViewModel;
            }
        }
    }
}