using Contract.Net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contract.ViewModel.Pages.UnapprovedContracts
{
    public class PageUnapprovedCancelContractViewModel : BaseModel
    {
        public string ContractNumber { get => GetValue<string>(); set => SetValue(value); }
        public string CommentText { get => GetValue<string>(); set => SetValue(value); }

        private CanceledContract CanceledContractInfo;
        public PageUnapprovedCancelContractViewModel(CanceledContract canceledContract, INavigation navigation) : base(navigation)
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

            CanceledContract data = new CanceledContract();
            data.Copy(CanceledContractInfo);
            data.comment = CommentText;

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseCanceledContract response = await HttpService.DeleteUnapprovedContractAndSetCanceledContract(data);
            if (!response.result)
            {
                ControlApp.CloseLoadingView();
                await Application.Current.MainPage.DisplayAlert(RSC.Cancel, RSC.Cancel_Message_1, RSC.Ok);
                return;
            }

            //UnapprovedContract unapprovedContract = new UnapprovedContract()
            //{
            //    user_phone_number = data.user_phone_number,
            //    preparer = data.preparer,
            //    contract_number = data.contract_number,
            //    company_contractor_name = data.company_contractor_name,
            //    date_of_contract = data.date_of_contract,
            //    contract_price = data.contract_price
            //};
            //ResponseUnapprovedContract response2 = await HttpService.DeleteUnapprovedContract(unapprovedContract);
            //if (!response2.result)
            //{
            //    ControlApp.CloseLoadingView();
            //    await Application.Current.MainPage.DisplayAlert(RSC.Cancel, RSC.Cancel_Message_1, RSC.Ok);
            //    return;
            //}

            ControlApp.CloseLoadingView();

            ControlApp.CanIRemove = true;
            await Navigation.PopModalAsync();
        }
        #endregion
    }
}
