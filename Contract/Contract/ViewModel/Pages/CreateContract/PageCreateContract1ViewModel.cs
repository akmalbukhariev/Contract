using Contract.Net;
using Contract.Pages.CreateContract;
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

        public PageCreateContract1ViewModel(INavigation navigation) : base(navigation)
        {
            ShowClientCompanyImage = true;
            ClientCompanyName = RSC.SelectClientCompany;
            ClientCompanyImage = "no_image";
        }

        #region Command
        public ICommand CommandSave => new Command(Save);

        private async void Save()
        {
            #region Check
            //if (!ControlApp.OpenClientInfo && ControlApp.OpenSearchClient && ControlApp.SelectedClientCompanyInfo == null)
            //{
            //    await Application.Current.MainPage.DisplayAlert(RSC.CreateContract, $"{RSC.Please} {RSC.SelectClientCompany}", RSC.Ok);
            //    return;
            //}

            //if (!ControlApp.OpenClientInfo && !ControlApp.OpenSearchClient && !IsFieildEmpty())
            //{

            //    return;
            //} 
            #endregion

            LibContract.HttpModels.CompanyInfo companyInfo = ControlApp.OpenSearchClient ? ControlApp.SelectedClientCompanyInfo : GetCompanyInfo();
            SetTransitionType();
            await Navigation.PushAsync(new PageCreateContract2(companyInfo));
        }
        #endregion
         
    }
}
