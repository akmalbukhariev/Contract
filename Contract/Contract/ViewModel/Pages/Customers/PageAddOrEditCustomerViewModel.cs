using Contract.Net;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Contract.ViewModel.Pages.Customers
{
    public class PageAddOrEditCustomerViewModel : BaseCompanyInfoModel
    {
        public PageAddOrEditCustomerViewModel(INavigation navigation) : base(navigation)
        {
            
        }

        public void SetData()
        {
            if (ControlApp.SelectedClientCompanyInfo == null) return;

            CompanyName = ControlApp.SelectedClientCompanyInfo.company_name;
            AddressOfCompany = ControlApp.SelectedClientCompanyInfo.address_of_company;
            AccountNumber = ControlApp.SelectedClientCompanyInfo.account_number;
            CompanyStir = ControlApp.SelectedClientCompanyInfo.stir_of_company;
            NameOfBank = ControlApp.SelectedClientCompanyInfo.name_of_bank;
            BankCode = ControlApp.SelectedClientCompanyInfo.bank_code;
            AreYouQQSPayer = ControlApp.SelectedClientCompanyInfo.are_you_qqs_payer == 1? true : false;
            QQSCode = ControlApp.SelectedClientCompanyInfo.qqs_number;
            PhoneNnumberOfCompany = ControlApp.SelectedClientCompanyInfo.company_phone_number;
            PositionOfSignatory = 0;//ControlApp.SelectedClientCompanyInfo.position_of_signer;
            FullNameOfSignatory = ControlApp.SelectedClientCompanyInfo.name_of_signer;
            IsAccountProvided = ControlApp.SelectedClientCompanyInfo.is_accountant_provided == 1? true : false;
            AccountantName = ControlApp.SelectedClientCompanyInfo.accountant_name;
            IsCounselProvided = ControlApp.SelectedClientCompanyInfo.is_legal_counsel_provided == 1 ? true : false;
            CounselName = ControlApp.SelectedClientCompanyInfo.counsel_name;
            CreatedDate = ControlApp.SelectedClientCompanyInfo.created_date;
        }

        public async void RequestAddInfo()
        {
            ControlApp.ShowLoadingView(RSC.PleaseWait); 
            ResponseClientCompanyInfo response = await HttpService.SetClientCompanyInfo(GetCompanyInfo());
            ControlApp.CloseLoadingView();

            string strMessage = response.result ? RSC.SuccessfullyAdded : RSC.Failed;
            await Application.Current.MainPage.DisplayAlert(RSC.CreateContract, strMessage, RSC.Ok);

            if (response.result)
                await Navigation.PopAsync();
        }

        public async void RequestUpdateInfo()
        {
            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseClientCompanyInfo response = await HttpService.UpdateClientCompanyInfo(GetCompanyInfo());
            ControlApp.CloseLoadingView();

            string strMessage = response.result ? RSC.SuccessfullyUpdated : RSC.Failed;
            await Application.Current.MainPage.DisplayAlert(RSC.CreateContract, strMessage, RSC.Ok);

            if (response.result)
                await Navigation.PopAsync();
        }
    }
}
