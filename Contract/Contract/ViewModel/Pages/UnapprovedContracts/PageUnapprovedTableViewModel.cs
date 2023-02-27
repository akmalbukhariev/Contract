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
using LibContract;

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
            await Navigation.PushModalAsync(new PageSign());
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

        public ICommand RefreshCommand => new Command(Refresh);

        private void Refresh()
        {
            ControlApp.Vibrate();
            RequestInfo();
        }

        public async void RequestInfo()
        { 
            DataList.Clear();

            if (!ControlApp.InternetOk()) return;

            LibContract.HttpModels.ApprovedUnapprovedContract request = new LibContract.HttpModels.ApprovedUnapprovedContract()
            {
                user_phone_number = ControlApp.UserInfo.phone_number,
                user_stir = ControlApp.UserCompanyInfo.stir_of_company
            };

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseApprovedUnapprovedContract response = await Net.HttpService.GetUnapprovedContract(request);
            ControlApp.CloseLoadingView();

            if (!ControlApp.CheckResponse(response))
            {
                IsRefreshing = false;
                return;
            }

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
                        ContractNnumber = ContractNumberWorker.ExtractContractNumber(info.contract_number),
                        ContractNnumberReal = info.contract_number,
                        ClientStir = info.user_phone_number.Equals(ControlApp.UserInfo.phone_number) ? info.client_stir : info.user_stir,
                        CompanyName = info.user_phone_number.Equals(ControlApp.UserInfo.phone_number) ? info.client_company_name : info.user_company_name,
                        ContractDate = ControlApp.GetDateFromStr(info.created_date),
                        ContractPrice = info.total_cost_text,
                        ItemColor = info.user_phone_number.Equals(ControlApp.UserInfo.phone_number) ? Color.FromHex("#DEEAF6") : Color.FromHex("#FFFFFF"),
                        PreparerColor = info.user_phone_number.Equals(ControlApp.UserInfo.phone_number) ? Color.FromHex("#BDD6EE") : Color.FromHex("#FFF2CC")
                    };

                    if (item.Preparer.Equals(RSC.Me) && info.is_approved == 1)
                    {
                        item.ShowBusy = true;
                        item.ShowCheck = false;
                    }

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

            IsRefreshing = false;
        }
         
        public void Add(UnapprovedContract item)
        {
            DataList.Add(item);
        }
    }
}
