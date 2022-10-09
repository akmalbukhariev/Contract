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
            LogoImage = "plus";

            CompanyName = "";
            AddressOfCompany = "";
            AccountNumber = "";
            CompanyStir = "";
            NameOfBank = "";
            BankCode = "";
            PhoneNnumberOfCompany = "";
            PositionOfSignatory = "";
            FullNameOfSignatory = "";
            QQSCode = "";
            AccountantName = "";
            CounselName = "";

            AreYouQQSPayer = true;
            IsAccountProvided = true;
            IsCounselProvided = true;
        }

        public PageAddOrEditCustomerViewModel(PageAddOrEditCustomerViewModel other)
        {
            CompanyName = other.CompanyName;
            AddressOfCompany = other.AddressOfCompany;
            AccountNumber = other.AccountNumber;
            CompanyStir = other.CompanyStir;
            NameOfBank = other.NameOfBank;
            BankCode = other.BankCode;
            AreYouQQSPayer = other.AreYouQQSPayer;
            QQSCode = other.QQSCode;
            PhoneNnumberOfCompany = other.PhoneNnumberOfCompany;
            PositionOfSignatory = other.PositionOfSignatory;
            FullNameOfSignatory = other.FullNameOfSignatory;
            IsAccountProvided = other.IsAccountProvided;
            AccountantName = other.AccountantName;
            IsCounselProvided = other.IsCounselProvided;
            CounselName = other.CounselName;
            CreatedDate = CreatedDate;
            LogoImage = other.LogoImage;
            LogoImageStr = other.LogoImageStr;
        }

        public static bool operator ==(PageAddOrEditCustomerViewModel m1, PageAddOrEditCustomerViewModel m2)
        {
            return (m1.CompanyName == m2.CompanyName &&
                    m1.AddressOfCompany == m2.AddressOfCompany &&
                    m1.AccountNumber == m2.AccountNumber &&
                    m1.CompanyStir == m2.CompanyStir &&
                    m1.NameOfBank == m2.NameOfBank &&
                    m1.BankCode == m2.BankCode &&
                    m1.AreYouQQSPayer == m2.AreYouQQSPayer &&
                    m1.QQSCode == m2.QQSCode &&
                    m1.PhoneNnumberOfCompany == m2.PhoneNnumberOfCompany &&
                    m1.PositionOfSignatory == m2.PositionOfSignatory &&
                    m1.FullNameOfSignatory == m2.FullNameOfSignatory &&
                    m1.IsAccountProvided == m2.IsAccountProvided &&
                    m1.AccountantName == m2.AccountantName &&
                    m1.IsCounselProvided == m2.IsCounselProvided &&
                    m1.CounselName == m2.CounselName &&
                    m1.LogoImage == m2.LogoImage &&
                    m1.LogoImageStr == m2.LogoImageStr);
        }

        public static bool operator !=(PageAddOrEditCustomerViewModel m1, PageAddOrEditCustomerViewModel m2)
        {
            return (m1.CompanyName != m2.CompanyName ||
                    m1.AddressOfCompany != m2.AddressOfCompany ||
                    m1.AccountNumber != m2.AccountNumber ||
                    m1.CompanyStir != m2.CompanyStir ||
                    m1.NameOfBank != m2.NameOfBank ||
                    m1.BankCode != m2.BankCode ||
                    m1.AreYouQQSPayer != m2.AreYouQQSPayer ||
                    m1.QQSCode != m2.QQSCode ||
                    m1.PhoneNnumberOfCompany != m2.PhoneNnumberOfCompany ||
                    m1.PositionOfSignatory != m2.PositionOfSignatory ||
                    m1.FullNameOfSignatory != m2.FullNameOfSignatory ||
                    m1.IsAccountProvided != m2.IsAccountProvided ||
                    m1.AccountantName != m2.AccountantName ||
                    m1.IsCounselProvided != m2.IsCounselProvided ||
                    m1.CounselName != m2.CounselName ||
                    m1.LogoImage != m2.LogoImage ||
                    m1.LogoImageStr != m2.LogoImageStr);
        }

        public bool IsFieildEmpty()
        {   
            bool res1 = (string.IsNullOrEmpty(CompanyName?.Trim()) ||
                         string.IsNullOrEmpty(AddressOfCompany?.Trim()) ||
                         string.IsNullOrEmpty(AccountNumber?.Trim()) ||
                         string.IsNullOrEmpty(CompanyStir?.Trim()) ||
                         string.IsNullOrEmpty(NameOfBank?.Trim()) ||
                         string.IsNullOrEmpty(BankCode?.Trim()) ||
                         string.IsNullOrEmpty(PhoneNnumberOfCompany?.Trim()) ||
                         string.IsNullOrEmpty(PositionOfSignatory?.Trim()) ||
                         string.IsNullOrEmpty(FullNameOfSignatory?.Trim()));

            bool res2 = AreYouQQSPayer;
            bool res3 = IsAccountProvided;
            bool res4 = IsCounselProvided;

            if (AreYouQQSPayer)
                res2 = string.IsNullOrEmpty(QQSCode?.Trim());

            if (IsAccountProvided)
                res3 = string.IsNullOrEmpty(AccountantName?.Trim());

            if (IsCounselProvided)
                res4 = string.IsNullOrEmpty(CounselName?.Trim());

            return (res1 || res2 || res3 || res4);
        }

        public override bool Equals(object o)  
        {  
           return true;  
        }  

        public override int GetHashCode()
        {
            return 0;
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
            PositionOfSignatory = ControlApp.SelectedClientCompanyInfo.position_of_signer;
            FullNameOfSignatory = ControlApp.SelectedClientCompanyInfo.name_of_signer;
            IsAccountProvided = ControlApp.SelectedClientCompanyInfo.is_accountant_provided == 1? true : false;
            AccountantName = ControlApp.SelectedClientCompanyInfo.accountant_name;
            IsCounselProvided = ControlApp.SelectedClientCompanyInfo.is_legal_counsel_provided == 1 ? true : false;
            CounselName = ControlApp.SelectedClientCompanyInfo.counsel_name;
            CreatedDate = ControlApp.SelectedClientCompanyInfo.created_date;
            LogoImage = $"{HttpService.DATA_URL}{ControlApp.SelectedClientCompanyInfo.company_logo_url}";
        }

        public async void RequestAddInfo()
        {
            if (!ControlApp.InternetOk()) return;

            #region For test
            //CompanyInfo infoTest = new CompanyInfo()
            //{
            //    user_phone_number = "12",
            //    company_name = "Daved",
            //    address_of_company = "XXXXXX",
            //    account_number = "844444",
            //    stir_of_company = "33333555500011",
            //    name_of_bank = "ZZZZZ",
            //    bank_code = "9994421100",
            //    are_you_qqs_payer = 1,
            //    qqs_number = "33322200000",
            //    company_phone_number = "9201254",
            //    position_of_signer = "PPPPPP",
            //    name_of_signer = "Shag'zod",
            //    is_accountant_provided = 0,
            //    accountant_name = "YYYYYYY",
            //    is_legal_counsel_provided = 0,
            //    counsel_name = "GGGGGGGGGGG",
            //    company_logo_url = LogoImageStr,
            //    created_date = ""
            //};
            #endregion

            bool hasFile = string.IsNullOrEmpty(LogoImageStr) ? false : true;

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseClientCompanyInfo response = await HttpService.SetClientCompanyInfo(GetCompanyInfo(), hasFile);
            ControlApp.CloseLoadingView();

            string strMessage = response.result ? RSC.SuccessfullyAdded : RSC.Failed + ", " + response.message;
            await Application.Current.MainPage.DisplayAlert(RSC.CreateContract, strMessage, RSC.Ok);

            if (response.result)
                await Navigation.PopAsync();
        }

        public async void RequestUpdateInfo()
        {
            if (!ControlApp.InternetOk()) return;

            bool hasFile = string.IsNullOrEmpty(LogoImageStr) ? false : true;

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseClientCompanyInfo response = await HttpService.UpdateClientCompanyInfo(GetCompanyInfo(), hasFile);
            ControlApp.CloseLoadingView();

            string strMessage = response.result ? RSC.SuccessfullyUpdated : RSC.Failed + ", " + response.message;
            await Application.Current.MainPage.DisplayAlert(RSC.CreateContract, strMessage, RSC.Ok);

            if (response.result)
                await Navigation.PopAsync();
        } 
    }
}
