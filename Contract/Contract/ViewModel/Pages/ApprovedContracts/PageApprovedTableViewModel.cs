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

        public async void RequestInfo()
        {
            #region
            //CurrentContract item1 = new CurrentContract()
            //{
            //    No = "1.",
            //    Preparer = "Men",
            //    ContractNnumber = "22-001-12345",
            //    CompanyName = "Korxona nomi",
            //    ContractDate = "06.04.2022",
            //    ContractPrice = "100,000 sum",
            //    ContractPayment = "100 %",
            //    ContractPaymentColor = Color.FromHex("#C5E0B3"),
            //    ItemColor = Color.FromHex("#DEEAF6"),
            //    PreparerColor = Color.FromHex("#BDD6EE")
            //};
            //CurrentContract item2 = new CurrentContract()
            //{
            //    No = "2.",
            //    Preparer = "Kontragent",
            //    ContractNnumber = "22-001-12345",
            //    CompanyName = "Korxona nomi",
            //    ContractDate = "06.04.2022",
            //    ContractPrice = "100 $",
            //    ContractPayment = "90 %",
            //    ContractPaymentColor = Color.FromHex("#F7CAAC"),
            //    ItemColor = Color.FromHex("#FFFFFF"),
            //    PreparerColor = Color.FromHex("#FFF2CC")
            //};
            //CurrentContract item3 = new CurrentContract()
            //{
            //    No = "3.",
            //    Preparer = "Kontragent",
            //    ContractNnumber = "22-001-12345",
            //    CompanyName = "Korxona nomi",
            //    ContractDate = "06.04.2022",
            //    ContractPrice = "100 $",
            //    ContractPayment = "100 %",
            //    ContractPaymentColor = Color.FromHex("#538135"),
            //    ItemColor = Color.FromHex("#DEEAF6"),
            //    PreparerColor = Color.FromHex("#FFF2CC")
            //};

            //Add(item1);
            //Add(item2);
            //Add(item3);
            #endregion

            DataList.Clear();

            LibContract.HttpModels.ApprovedUnapprovedContract request = new LibContract.HttpModels.ApprovedUnapprovedContract()
            {
                user_phone_number = ControlApp.UserInfo.phone_number,
                user_stir = ControlApp.UserCompanyInfo.stir_of_company,
                is_approved = 1
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
                    ApprovedContract item = new ApprovedContract()
                    {
                        No = $"{no}.",
                        Preparer = info.user_phone_number.Equals(ControlApp.UserInfo.phone_number) ? RSC.Me : RSC.Contragent,
                        ContractNnumber = ContractNumberWorker.ExtractContractNumber(info.contract_number),
                        ContractNnumberReal = info.contract_number,
                        CompanyName = info.user_company_name,
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
        }

        public void Add(ApprovedContract item)
        {
            DataList.Add(item);
        }
    }
}
