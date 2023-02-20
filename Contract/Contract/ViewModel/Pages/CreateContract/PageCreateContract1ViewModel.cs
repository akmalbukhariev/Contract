using Contract.Net;
using Contract.Pages.CreateContract;
using LibContract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms; 

namespace Contract.ViewModel.Pages.CreateContract
{
    public class PageCreateContract1ViewModel : BaseCompanyInfoModel
    {
        #region Properties
        public bool ShowClientCompanyImage { get => GetValue<bool>(); set => SetValue(value); }
        public bool ShowLetter { get => GetValue<bool>(); set => SetValue(value); }
        public string FirstLetter { get => GetValue<string>(); set => SetValue(value); }
        public string ClientCompanyImage { get => GetValue<string>(); set => SetValue(value); }
        public string ClientCompanyName { get => GetValue<string>(); set => SetValue(value); }
        public string ClientCompanyStir { get => GetValue<string>(); set => SetValue(value); }
        #endregion

        public bool ShowQQS = false;
        public List<string> positionList = new List<string>();
        public PageCreateContract1ViewModel(INavigation navigation) : base(navigation)
        {
            ShowClientCompanyImage = true;
            ClientCompanyName = RSC.SelectClientCompany;
            ClientCompanyImage = "no_image";

            positionList.Add("Очиқ қолдириш");
            positionList.Add("Ochiq qoldirish");
            positionList.Add("Оставьте его открытым");
            positionList.Add("Leave it open");
        }

        #region Command
        public ICommand CommandSave => new Command(Save);
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

        private async void Save()
        {
            if (!ControlApp.InternetOk()) return;

            if (!ControlApp.OpenClientInfo && !ControlApp.OpenSearchClient && (CompanyStir == "" || CompanyStir == null))
            {
                await Application.Current.MainPage.DisplayAlert(RSC.CreateContract, $"{RSC.EnterCompanyStir}", RSC.Ok);
                return;
            }

            if (!ControlApp.OpenClientInfo && ControlApp.OpenSearchClient && ControlApp.SelectedClientCompanyInfo == null)
            {
                await Application.Current.MainPage.DisplayAlert(RSC.CreateContract, RSC.SelectClientCompany, RSC.Ok);
                return;
            }

            if (!ControlApp.OpenClientInfo && !ControlApp.OpenSearchClient)
            {
                if (AccountNumber == null || AccountNumber.RemoveWhitespace().Length < 20)
                {
                    await Application.Current.MainPage.DisplayAlert(RSC.CreateContract, $"{RSC.Check}: {RSC.AccountNumber}", RSC.Ok);
                    return;
                }

                if (CompanyStir == null || CompanyStir.RemoveWhitespace().Length < 9)
                {
                    await Application.Current.MainPage.DisplayAlert(RSC.CreateContract, $"{RSC.Check}: {RSC.CompanySTIRi}", RSC.Ok);
                    return;
                }

                if (BankCode == null || BankCode.RemoveWhitespace().Length < 5)
                {
                    await Application.Current.MainPage.DisplayAlert(RSC.CreateContract, $"{RSC.Check}: {RSC.BankCode}", RSC.Ok);
                    return;
                }

                if (ShowQQS && (QQSCode == null || QQSCode.RemoveWhitespace().Length < 12))
                {
                    await Application.Current.MainPage.DisplayAlert(RSC.CreateContract, $"{RSC.Check}: {RSC.QQS_Code}", RSC.Ok);
                    return;
                }
            }
             
            LibContract.HttpModels.CompanyInfo companyInfo = (!ControlApp.OpenClientInfo && ControlApp.OpenSearchClient) ? ControlApp.SelectedClientCompanyInfo : GetCompanyInfo();
            if (positionList.Contains(PositionOfSignatory))
                companyInfo.position_of_signer = "________________";

            SetTransitionType();
            await Navigation.PushAsync(new PageCreateContract2(companyInfo));
        }
        #endregion 
    }
}
