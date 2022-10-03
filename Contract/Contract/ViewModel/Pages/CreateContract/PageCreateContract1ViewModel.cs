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
        public string ClientCompanyImage { get => GetValue<string>(); set => SetValue(value); }
        public string ClientCompanyName { get => GetValue<string>(); set => SetValue(value); }
        public string ClientCompanyStir { get => GetValue<string>(); set => SetValue(value); }
        public LayoutOptions ClientHorizontalOption { get => GetValue<LayoutOptions>(); set => SetValue(value); }


        public bool OpenClientInfo { get => GetValue<bool>(); set => SetValue(value); }
        public bool OpenSearchClient { get => GetValue<bool>(); set => SetValue(value); }
        public int CustomerIndex { get => GetValue<int>(); set => SetValue(value); }
         
        #endregion

        public PageCreateContract1ViewModel(INavigation navigation) : base(navigation)
        {
            ShowClientCompanyImage = false;
            ClientCompanyName = RSC.SelectClientCompany;
            ClientHorizontalOption = LayoutOptions.CenterAndExpand;
        }

        #region Command
        public ICommand CommandSave => new Command(Save);

        private async void Save()
        {
            SetTransitionType();
            await Navigation.PushAsync(new PageCreateContract2());
        }
        #endregion
         
    }
}
