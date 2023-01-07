using Contract.Control;
using LibContract.HttpModels;
using LibContract.HttpResponse;
using Contract.Pages.CanceledContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LibContract;
using Contract.Net;
using System.Threading;

namespace Contract.Pages.CreateContract
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCreateContract3 : IPage
    {
        bool appeared = false;
        string contractUrl = "";

        private CreateContractInfo ContractInfo = null;
        public PageCreateContract3(CreateContractInfo createContract)
        {
            InitializeComponent();

            ContractInfo = new CreateContractInfo(createContract);

            lbContractNumber.Text = ContractNumberWorker.ExtractContractNumber(createContract.contract_number);
            lbContractPrice.Text = createContract.total_cost_text;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (appeared) return;
            
            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseCreatePdf response = await HttpService.CreateContractPdf(ContractInfo.contract_number);
            if (response.result)
            {
                contractUrl = response.pdf_url;
                //await DisplayAlert(RSC.CreateContract, RSC.SuccessfullyCompleted, RSC.Ok);
            }
            else
            {
                await DisplayAlert(RSC.CreateContract, response.message, RSC.Ok);
            }
            ControlApp.CloseLoadingView();
            appeared = true;
        }
  
        protected override bool OnBackButtonPressed()
        {
            Navigation.PopToRootAsync();
            return true;
        }

        private async void View_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView(boxView);
            ClickAnimationView(stackView);

            if (string.IsNullOrEmpty(contractUrl))
            {
                await DisplayAlert(RSC.CreateContract, RSC.CouldNotCreateContract, RSC.Ok);
                return;
            }

            await Navigation.PushAsync(new PageShowContract($"{HttpService.DATA_URL}{contractUrl}"));
        }

        private async void Send_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView(boxSend);
            ClickAnimationView(stackSend);

            if (string.IsNullOrEmpty(contractUrl))
            {
                await DisplayAlert(RSC.CreateContract, RSC.CouldNotCreateContract, RSC.Ok);
                return;
            }

            await ControlApp.ShareUri($"{HttpService.DATA_URL}{contractUrl}");
        }

        private async void Cancel_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView(boxCancel);
            ClickAnimationView(stackCancel); 

            await Navigation.PushModalAsync(new PageCancelContract(ContractInfo,RSC.CreateContract ,true));
        }
    }
}