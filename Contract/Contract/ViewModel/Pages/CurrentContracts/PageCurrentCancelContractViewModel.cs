using Contract.HttpResponse;
using Contract.Net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contract.ViewModel.Pages.CurrentContracts
{
    class PageCurrentCancelContractViewModel : BaseModel
    {
        public string ContractNumber { get => GetValue<string>(); set => SetValue(value); }
        public string CommentText { get => GetValue<string>(); set => SetValue(value); }

        private HttpModels.CanceledContract CanceledContractInfo;
        public PageCurrentCancelContractViewModel(HttpModels.CanceledContract canceledContract, INavigation navigation) : base(navigation)
        {
            CanceledContractInfo = canceledContract;
            ContractNumber = $" {canceledContract.contract_number} ";
        }

        #region Commands
        public ICommand CommandCancel => new Command(Cancel);

        private async void Cancel()
        {
            if (string.IsNullOrEmpty(CommentText.Trim()))
            {
                await Application.Current.MainPage.DisplayAlert(RSC.Cancel, RSC.Cancel_Message_2, RSC.Ok);
                return;
            }

            HttpModels.CanceledContract data = new HttpModels.CanceledContract();
            data.Copy(CanceledContractInfo);
            data.comment = CommentText;

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseCanceledContract response = await HttpService.DeleteApplicableContractAndSetCanceledContract(data);
            if (!response.result)
            {
                ControlApp.CloseLoadingView();
                await Application.Current.MainPage.DisplayAlert(RSC.Cancel, RSC.Cancel_Message_1, RSC.Ok);
                return;
            }
              
            ControlApp.CloseLoadingView();

            ControlApp.CanIRemove = true;
            await Navigation.PopModalAsync();
        }
        #endregion
    }
}
