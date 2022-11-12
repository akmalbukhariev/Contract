using Contract.Control;
using Contract.HttpModels;
using Contract.HttpResponse;
using Contract.Net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

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
            ContractNumber = $" {ControlApp.ExtractContractNumber(contractInfo.contract_number)} ";
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
                    Application.Current.MainPage = new TransitionNavigationPage(new Contract.Pages.PageMain());
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
