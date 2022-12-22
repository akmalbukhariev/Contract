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

namespace Contract.Pages.CreateContract
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCreateContract3 : IPage
    {
        private CreateContractInfo ContractInfo;
        public PageCreateContract3(CreateContractInfo createContract)
        {
            InitializeComponent();

            ContractInfo = new CreateContractInfo(createContract);

            lbContractNumber.Text = ContractNumberWorker.ExtractContractNumber(createContract.contract_number);
            lbContractPrice.Text = createContract.total_cost_text;
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

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseCreatePdf response = await HttpService.CreateContractPdf(ContractInfo.contract_number);
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

        private void Send_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView(boxSend);
            ClickAnimationView(stackSend);

            //ControlApp.ShowLoadingView(RSC.PleaseWait);
            //ResponseApprovedUnapprovedContract response = await Net.HttpService.SetUnapprovedContract(ContractInfo.contract_number);
            //ControlApp.CloseLoadingView();
            //
            //string strMessage = response.result ? RSC.SuccessfullyAdded : RSC.Failed;
            //await DisplayAlert(RSC.CreateContract, strMessage, RSC.Ok);
            //
            //Application.Current.MainPage = new TransitionNavigationPage(new PageMain());
        }

        private async void Cancel_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView(boxCancel);
            ClickAnimationView(stackCancel); 

            await Navigation.PushModalAsync(new PageCancelContract(ContractInfo,RSC.CreateContract ,true));
        }
    }
}