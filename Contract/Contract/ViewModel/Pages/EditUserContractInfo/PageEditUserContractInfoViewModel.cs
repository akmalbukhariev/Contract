using LibContract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contract.ViewModel.Pages.EditUserContractInfo
{
    public class PageEditUserContractInfoViewModel: BaseCompanyInfoModel
    {   
        public delegate void RequestInfoFinished();
        public event RequestInfoFinished EventRequestInfoFinished;

        public PageEditUserContractInfoViewModel(INavigation navigation) : base(navigation)
        {
            //CompanyName = "Kontrakt Maker";
            //AddressOfCompany = "Uzbekistan";
            //AccountNumber = "126741";
            //CompanyStir = "111122";
            //NameOfBank = "ASAKA";
            //BankCode = "99115544";
            //AreYouQQSPayer = true;
            //QQSCode = "7744261";
            //PhoneNnumberOfCompany = "9989756321";
            ////PositionOfSignatory = "Akmal";
            ////PositionOfSignatory_index = 1;
            //FullNameOfSignatory = "Akmal Bukhariev";
            //IsAccountProvided = true;
            //AccountantName = "Rashid Vohidov";
            //IsCounselProvided = false;
            //CounselName = "Ikrom";
        }

        BaseCompanyInfoModel oldModel = null;//new BaseCompanyInfoModel();
        public ICommand CommandSave => new Command(Save);

        public async void RequestInfo()
        {
            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseUserCompanyInfo response = await Net.HttpService.GetUserCompanyInfo(ControlApp.UserInfo.phone_number);
            ControlApp.CloseLoadingView();

            if (response.result)
            {
                CompanyName = response.data.company_name;
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

                oldModel = Copy(this);

                EventRequestInfoFinished?.Invoke();
            }
        }

        private async void Save()
        {
            if (IsFieildEmpty())
            {
                await Application.Current.MainPage.DisplayAlert(RSC.MyCompany, RSC.FieldEmpty, RSC.Ok);
                return;
            }

            ResponseUserCompanyInfo response = null;
            ControlApp.ShowLoadingView(RSC.PleaseWait);

            if (oldModel == null)
            {
                response = await Net.HttpService.SetUserCompanyInfo(GetCompanyInfo());
            }
            else if (!Equals(oldModel))
            {
                response = await Net.HttpService.UpdateUserCompanyInfo(GetCompanyInfo());
            }
            ControlApp.CloseLoadingView();

            if (response != null && response.result)
            {
                await Application.Current.MainPage.DisplayAlert(RSC.MyCompany, RSC.SuccessfullyUpdated, RSC.Ok);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(RSC.MyCompany, $"{RSC.Failed} : {response.message}", RSC.Ok);
            }

            await Navigation.PopAsync();
        }
    }
}
