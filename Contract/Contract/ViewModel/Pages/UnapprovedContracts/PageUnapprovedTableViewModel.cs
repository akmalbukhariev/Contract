using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LibContract.HttpResponse;
using Contract.Model;
using Contract.Pages.UnapprovedContracts;
using Xamarin.Forms;

namespace Contract.ViewModel.Pages.UnapprovedContracts
{ 
    public class PageUnapprovedTableViewModel : BaseModel
    {
        public bool ShowConfirmBox { get => GetValue<bool>(); set => SetValue(value); }

        public ObservableCollection<UnapprovedContract> DataList { get; set; }

        public PageUnapprovedTableViewModel(INavigation navigation) : base(navigation)
        { 
            DataList = new ObservableCollection<UnapprovedContract>(); 
        }

        #region Commands
        public ICommand CommandCode => new Command(ClickCode);
        public ICommand CommandERI => new Command(ClickERI);
        public ICommand CommandBoxView => new Command(ClickBoxView);

        private async void ClickCode()
        {
            await Task.Delay(100);
            ShowConfirmBox = false;

            SetTransitionType(TransitionType.SlideFromBottom);
            await Navigation.PushModalAsync(new PageContractSpecialCode());
        }

        private async void ClickERI()
        {
            await Task.Delay(100);
            ShowConfirmBox = false;

            SetTransitionType(TransitionType.SlideFromBottom);
            await Navigation.PushModalAsync(new PageERIKey());
        }

        private void ClickBoxView()
        {
            ShowConfirmBox = false;
        }
        #endregion

        public async void RequestInfo()
        { 
            this.DataList.Clear();

            LibContract.HttpModels.ApprovedUnapprovedContract request = new LibContract.HttpModels.ApprovedUnapprovedContract()
            {
                user_phone_number = ControlApp.UserInfo.phone_number,
                user_stir = ControlApp.UserCompanyInfo.stir_of_company,
                is_approved = 0
            };

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseApprovedUnapprovedContract response = await Net.HttpService.GetApprovedOrUnapprovedContract(request);
            ControlApp.CloseLoadingView();

            if (response.result)
            {
                int no = 0;
                foreach (LibContract.HttpModels.CreateContractInfo info in response.data)
                {
                    no++;
                    UnapprovedContract item = new UnapprovedContract()
                    {
                        No = $"{no}.",
                        Preparer = info.user_phone_number.Equals(ControlApp.UserInfo.phone_number) ? RSC.Me : RSC.Contragent,
                        ContractNnumber = info.contract_number,
                        CompanyName = info.user_company_name,
                        ContractDate = info.created_date,
                        ContractPrice = info.total_cost_text,
                        ItemColor = info.user_phone_number.Equals(ControlApp.UserInfo.phone_number) ? Color.FromHex("#DEEAF6") : Color.FromHex("#FFFFFF"),
                        PreparerColor = info.user_phone_number.Equals(ControlApp.UserInfo.phone_number) ? Color.FromHex("#BDD6EE") : Color.FromHex("#FFF2CC")
                    };

                    Add(item);
                }

                if (DataList.Count > 0)
                {
                    ShowEmptyMessage = false;
                    CloseEmptyMessage = true;
                }
                else
                {
                    ShowEmptyMessage = true;
                    CloseEmptyMessage = false;
                }
            }
        }
         
        public void Add(UnapprovedContract item)
        {
            DataList.Add(item);
        }
    }
}
