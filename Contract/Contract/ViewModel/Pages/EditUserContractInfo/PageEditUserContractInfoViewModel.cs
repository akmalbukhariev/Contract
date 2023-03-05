using Contract.Net;
using LibContract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contract.ViewModel.Pages.EditUserContractInfo
{
    public class PageEditUserContractInfoViewModel : BaseCompanyInfoModel
    {   
        public delegate void RequestInfoFinished();
        public event RequestInfoFinished EventRequestInfoFinished;

        public PageEditUserContractInfoViewModel(INavigation navigation) : base(navigation)
        {
            LogoImage = "plus"; 
        }

        BaseCompanyInfoModel oldModel = null;//new BaseCompanyInfoModel();
        public ICommand CommandSave => new Command(Save);

        public async void RequestInfo()
        {
            if (!ControlApp.InternetOk()) return;

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseUserCompanyInfo response = await Net.HttpService.GetUserCompanyInfo(ControlApp.UserInfo.phone_number);
            ControlApp.CloseLoadingView();

            if (!ControlApp.CheckResponse(response)) return;

            if (response.result)
            {
                Id = response.data.id;
                CompanyName = response.data.company_name;
                SelectedDocument = response.data.document;
                SelectedDocument_index = response.data.document_index;
                AddressOfCompany = response.data.address_of_company;
                AccountNumber = response.data.account_number;
                CompanyStir = response.data.stir_of_company;
                NameOfBank = response.data.name_of_bank;
                BankCode = response.data.bank_code;
                AreYouQQSPayer = response.data.are_you_qqs_payer == 1? true : false;
                QQSCode = response.data.qqs_number;
                PhoneNnumberOfCompany = response.data.company_phone_number;
                PositionOfSignatory = response.data.position_of_signer;
                PositionOfSignatory_index = response.data.position_of_signer_index;
                FullNameOfSignatory = response.data.name_of_signer;
                IsAccountProvided = response.data.is_accountant_provided == 1? true : false;
                AccountantName = response.data.accountant_name;
                IsCounselProvided = response.data.is_legal_counsel_provided == 1 ? true : false;
                CounselName = response.data.counsel_name;
                CreatedDate = response.data.created_date;

                oldModel = Copy();

                if (string.IsNullOrEmpty(response.data.company_logo_url))
                    LogoImage = "plus.png";
                else
                    LogoImage = $"{HttpService.DATA_URL}{response.data.company_logo_url}";

                EventRequestInfoFinished?.Invoke();
            }
        }

        private async void Save()
        {
            if (!ControlApp.InternetOk()) return;

            if (IsFieildEmpty())
            {
                await Application.Current.MainPage.DisplayAlert(RSC.MyCompany, RSC.FieldEmpty, RSC.Ok);
                return;
            }

            ResponseUserCompanyInfo response = null;
            ControlApp.ShowLoadingView(RSC.PleaseWait);

            bool hasFile = string.IsNullOrEmpty(LogoImageStr) ? false : true;

            if (oldModel == null)
            {
                CreatedDate = DateTime.Now.ToString(Constants.TimeFormat);
                response = await HttpService.SetUserCompanyInfo(GetCompanyInfo(), hasFile);

                if (!ControlApp.CheckResponse(response)) return;
            }
            else if (!Equals(oldModel) || hasFile)
            {
                response = await HttpService.UpdateUserCompanyInfo(GetCompanyInfo(), hasFile);

                if (!ControlApp.CheckResponse(response)) return;
            }
            ControlApp.CloseLoadingView();
             
            if (response != null && response.result)
            {
                await Application.Current.MainPage.DisplayAlert(RSC.MyCompany, RSC.SuccessfullyUpdated, RSC.Ok);
                ControlApp.UserCompanyInfo = new LibContract.HttpModels.CompanyInfo(GetCompanyInfo());
            }
            else if (response != null && !response.result)
            {
                await Application.Current.MainPage.DisplayAlert(RSC.MyCompany, $"{RSC.Failed}", RSC.Ok);
            }

            await Navigation.PopAsync();
        }
    }
}
