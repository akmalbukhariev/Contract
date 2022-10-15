using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Contract.HttpResponse;
using Contract.Model;
using Xamarin.Forms;

namespace Contract.ViewModel.Pages.CurrentContracts
{ 
    public class PageTableViewModel : BaseModel
    {
        public bool ShowConfirmBox { get => GetValue<bool>(); set => SetValue(value); }

        public ObservableCollection<CurrentContract> DataList { get; set; }

        public PageTableViewModel()
        { 
            DataList = new ObservableCollection<CurrentContract>(); 
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

            this.DataList.Clear();

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseApplicableContract response = await Net.HttpService.GetApplicableContract(ControlApp.UserInfo.phone_number);
            ControlApp.CloseLoadingView();

            if (response.result)
            {
                int no = 0;
                foreach (HttpModels.ApplicableContract info in response.data)
                {
                    no++;
                    CurrentContract item = new CurrentContract()
                    {
                        No = $"{no.ToString()}.",
                        Preparer = info.preparer,
                        ContractNnumber = info.contract_number,
                        CompanyName = info.company_contractor_name,
                        ContractDate = info.date_of_contract,
                        ContractPrice = info.contract_price,
                        ContractPayment = info.payment_percent,
                        ContractPaymentColor = Color.FromHex("#C5E0B3"),
                        ItemColor = Color.FromHex("#DEEAF6"),
                        PreparerColor = Color.FromHex("#BDD6EE")
                    };

                    Add(item);
                }
            }
        }

        public void Add(CurrentContract item)
        {
            DataList.Add(item);
        }
    }
}
