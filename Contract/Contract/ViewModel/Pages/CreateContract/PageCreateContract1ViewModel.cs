using Contract.Pages.CreateContract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contract.ViewModel.Pages.CreateContract
{
    public class PageCreateContract1ViewModel : BaseModel
    {
        public bool OpenClientInfo { get => GetValue<bool>(); set => SetValue(value); }
        public bool OpenSearchClient { get => GetValue<bool>(); set => SetValue(value); }
        public int CustomerIndex { get => GetValue<int>(); set => SetValue(value); }
        public string CompanyName { get => GetValue<string>(); set => SetValue(value); }
        public string AddressOfCompany { get => GetValue<string>(); set => SetValue(value); }
        public string AccountNumber { get => GetValue<string>(); set => SetValue(value); }
        public string TINEnterprise { get => GetValue<string>(); set => SetValue(value); }
        public string NameOfBank { get => GetValue<string>(); set => SetValue(value); }
        public string BankCode { get => GetValue<string>(); set => SetValue(value); }
        public bool AreYouTaxPayer { get => GetValue<bool>(); set => SetValue(value); }
        public string VatCode { get => GetValue<string>(); set => SetValue(value); }
        public string PhoneNnumberOfCompany { get => GetValue<string>(); set => SetValue(value); }
        public int PositionOfSignatory { get => GetValue<int>(); set => SetValue(value); }
        public string FullNameOfSignatory { get => GetValue<string>(); set => SetValue(value); }
        public bool IsAccountProvided { get => GetValue<bool>(); set => SetValue(value); }
        public string AccountantName { get => GetValue<string>(); set => SetValue(value); }
        public bool IsCounselProvided { get => GetValue<bool>(); set => SetValue(value); }
        public string CounselName { get => GetValue<string>(); set => SetValue(value); } 
         
        public PageCreateContract1ViewModel(INavigation navigation) : base(navigation)
        {
            
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
