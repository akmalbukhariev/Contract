using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LibContract.HttpResponse;
using Contract.Model;
using Xamarin.Forms;
using LibContract;

namespace Contract.ViewModel.Pages.CurrentContracts
{ 
    public class PageApprovedTableViewModel : BaseModel
    {
        public bool ShowConfirmBox { get => GetValue<bool>(); set => SetValue(value); }

        public ObservableCollection<ApprovedContract> DataList { get; set; }

        public PageApprovedTableViewModel()
        { 
            DataList = new ObservableCollection<ApprovedContract>(); 
        }

        #region Commands
        public ICommand CommandCode => new Command(ClickCode);
        public ICommand CommandERI => new Command(ClickERI);

        private async void ClickCode()
        {
            await Task.Delay(100);
            ShowConfirmBox = false;
        }

        private async void ClickERI()
        {
            await Task.Delay(100);
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
                user_stir = ControlApp.UserCompanyInfo.stir_of_company,
                is_approved = 1
            };

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseApprovedUnapprovedContract response = await Net.HttpService.GetApprovedContract(request);
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
                    ApprovedContract item = new ApprovedContract()
                    {
                        No = $"{no}.",
                        Preparer = info.user_phone_number.Equals(ControlApp.UserInfo.phone_number) ? RSC.Me : RSC.Contragent,
                        ContractNnumber = ContractNumberWorker.ExtractContractNumber(info.contract_number),
                        ContractNnumberReal = info.contract_number,
                        CompanyName = info.client_company_name,
                        ContractDate = info.created_date,
                        ContractPrice = info.total_cost_text,
                        ContractPayment = "100 %",
                        ContractPaymentColor = Color.FromHex("#C5E0B3"),
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

            IsRefreshing = false;
        }

        public void Add(ApprovedContract item)
        {
            DataList.Add(item);
        }
    }
}
