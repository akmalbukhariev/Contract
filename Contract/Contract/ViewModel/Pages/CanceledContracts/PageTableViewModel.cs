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

namespace Contract.ViewModel.Pages.CanceledContracts
{ 
    public class PageTableViewModel : BaseModel
    {
        public bool ShowExplanationBox { get => GetValue<bool>(); set => SetValue(value); }
        public string ExplanationText { get => GetValue<string>(); set => SetValue(value); }

        public ObservableCollection<CanceledContract> DataList { get; set; }

        public PageTableViewModel()
        { 
            DataList = new ObservableCollection<CanceledContract>(); 
        }

        #region Commands
        
        public ICommand CommandExplanationOK => new Command(ExplanationOK);
 
        private void ExplanationOK()
        {
            ShowExplanationBox = false;
        }
        #endregion

        public async void RequestInfo()
        { 
            this.DataList.Clear();

            HttpModels.CreateContractInfo request = new HttpModels.CreateContractInfo();
            request.user_stir = "111122";
            request.user_phone_number = "12";
            request.is_canceled = 1;

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseCanceledContract response = await Net.HttpService.GetCanceledContract(request);
            ControlApp.CloseLoadingView();

            if (response.result)
            {
                int no = 0;
                foreach (HttpModels.CreateContractInfo info in response.data)
                { 
                    no++;
                    CanceledContract item = new CanceledContract()
                    {
                        No = $"{no}.",
                        Preparer = info.user_phone_number.Equals(ControlApp.UserInfo.phone_number)? RSC.Me : RSC.Contragent,
                        ContractNnumber = info.contract_number,
                        CompanyName = info.user_company_name,
                        ContractDate = info.created_date,
                        ContractPrice = info.total_cost_text,
                        ContractPayment = "100 %",
                        CommentText = info.comment,
                        ContractPaymentColor = Color.FromHex("#C5E0B3"),
                        ItemColor = info.user_phone_number.Equals(ControlApp.UserInfo.phone_number)? Color.FromHex("#DEEAF6") : Color.FromHex("#FFFFFF"),
                        PreparerColor = info.user_phone_number.Equals(ControlApp.UserInfo.phone_number)? Color.FromHex("#BDD6EE") : Color.FromHex("#FFF2CC")
                    };

                    Add(item);
                }
            }
        }

        public void Add(CanceledContract item)
        {
            DataList.Add(item);
        }
    }
}
