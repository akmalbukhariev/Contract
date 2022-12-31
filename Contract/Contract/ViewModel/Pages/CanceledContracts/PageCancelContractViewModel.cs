using Contract.Control;
using LibContract.HttpModels;
using LibContract.HttpResponse;
using Contract.Net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using LibContract;

namespace Contract.ViewModel.Pages.CanceledContracts
{
    public class PageCancelContractViewModel : BaseModel
    {
        public bool MoveToMainPage = false;
        public string ContractNumber { get => GetValue<string>(); set => SetValue(value); }
        public string CommentText { get => GetValue<string>(); set => SetValue(value); }

        private CreateContractInfo CanceledContractInfo;
        public PageCancelContractViewModel(CreateContractInfo contractInfo, INavigation navigation, bool moveToMainPage = false) 
            : base(navigation)
        {
            CanceledContractInfo = contractInfo;
            ContractNumber = $" {ContractNumberWorker.ExtractContractNumber(contractInfo.contract_number)} ";
            MoveToMainPage = moveToMainPage;
        }

        #region Commands
        public ICommand CommandCancel => new Command(Cancel);

        private async void Cancel()
        {
            if (string.IsNullOrEmpty(CommentText))
            {
                await Application.Current.MainPage.DisplayAlert(RSC.Cancel, RSC.Cancel_Message_2, RSC.Ok);
                return;
            }
 
            CanceledContractInfo.comment = CommentText;

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseCreateContract response = await HttpService.CancelContract(CanceledContractInfo);
            ControlApp.CloseLoadingView();

            if (response.result)
            {
                ControlApp.CanIRemove = true;
                await Application.Current.MainPage.DisplayAlert(RSC.Cancel, RSC.Cancel_Message_3, RSC.Ok);

                if (MoveToMainPage)
                    Application.Current.MainPage = new TransitionNavigationPage(new Contract.Pages.PageMasterDetail());
                else
                    await Navigation.PopModalAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(RSC.Cancel, RSC.Cancel_Message_1, RSC.Ok);
            }  
        }
        #endregion
    }
}
