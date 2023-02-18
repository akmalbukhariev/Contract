using LibContract.HttpResponse;
using Contract.Net;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Windows.Input;

namespace Contract.ViewModel.Pages.Customers
{
    public class PageAddOrEditCustomerViewModel : BaseCompanyInfoModel 
    {
        public PageAddOrEditCustomerViewModel(INavigation navigation) : base(navigation)
        {
            LogoImage = "plus";

            CompanyName = "";
            SelectedDocument = "";
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
            SelectedDocument = other.SelectedDocument;
            SelectedDocument_index = other.SelectedDocument_index;
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
                    m1.SelectedDocument == m2.SelectedDocument &&
                    m1.SelectedDocument_index == m2.SelectedDocument_index &&
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
                    m1.SelectedDocument != m2.SelectedDocument ||
                    m1.SelectedDocument_index != m2.SelectedDocument_index ||
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
         
        public override bool Equals(object o)
        {  
           return true;  
        }  

        public override int GetHashCode()
        {
            return 0;
        }

        public ICommand CommandSearchStir => new Command(SearchStir);

        private async void SearchStir()
        {
            if (!ControlApp.InternetOk()) return;

            if (string.IsNullOrEmpty(CompanyStir))
            {
                await Application.Current.MainPage.DisplayAlert(RSC.CreateContract, RSC.EnterCompanyStir, RSC.Ok);
                return;
            }

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseUserCompanyInfo response = await HttpService.GetUserCompanyInfoToCreateContract(CompanyStir.RemoveWhitespace().Trim());
            ControlApp.CloseLoadingView();

            if (!ControlApp.CheckResponse(response)) return;

            if (!response.result)
            {
                await Application.Current.MainPage.DisplayAlert(RSC.CreateContract, RSC.CouldNotFind, RSC.Ok);
                return;
            }

            if (response.data.user_phone_number.Equals(ControlApp.UserInfo.phone_number))
            {
                await Application.Current.MainPage.DisplayAlert(RSC.CreateContract, RSC.CannotUseYourStirNumber, RSC.Ok);
                return;
            }

            SetCompanyInfo(response.data);
        }

        public void SetData()
        {
            if (ControlApp.SelectedClientCompanyInfo == null) return;
             
            SetCompanyInfo(ControlApp.SelectedClientCompanyInfo);

            if (string.IsNullOrEmpty(ControlApp.SelectedClientCompanyInfo.company_logo_url))
                LogoImage = "plus.png";
            else
                LogoImage = $"{HttpService.DATA_URL}{ControlApp.SelectedClientCompanyInfo.company_logo_url}";
        }

        public async void RequestAddInfo()
        {
            if (!ControlApp.InternetOk()) return;
             
            bool hasFile = string.IsNullOrEmpty(LogoImageStr) ? false : true;

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseClientCompanyInfo response = await HttpService.SetClientCompanyInfo(GetCompanyInfo(), hasFile);
            ControlApp.CloseLoadingView();

            if (!ControlApp.CheckResponse(response)) return;

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

            if (!ControlApp.CheckResponse(response)) return;

            string strMessage = response.result ? RSC.SuccessfullyUpdated : RSC.Failed + ", " + response.message;
            await Application.Current.MainPage.DisplayAlert(RSC.CreateContract, strMessage, RSC.Ok);

            if (response.result)
                await Navigation.PopAsync();
        } 
    }
}
