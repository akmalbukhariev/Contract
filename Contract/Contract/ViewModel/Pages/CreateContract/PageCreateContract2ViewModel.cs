using Contract.Model;
using Contract.Pages.CreateContract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contract.ViewModel.Pages.CreateContract
{
    public class PageCreateContract2ViewModel : BaseModel
    {
        #region Properties
        public string SelectedServiceType { get => GetValue<string>(); set => SetValue(value); }
        public string ContractNumber { get => GetValue<string>(); set => SetValue(value); }
        public string SelectedCurrency { get => GetValue<string>(); set => SetValue(value); }
        public string SelectedQQS { get => GetValue<string>(); set => SetValue(value); }
        public bool IsExeciseTax { get => GetValue<bool>(); set => SetValue(value); }
        public string InterestText { get => GetValue<string>(); set => SetValue(value); }
        public string TotalCostText { get => GetValue<string>(); set => SetValue(value); }
        public bool Agree { get => GetValue<bool>(); set => SetValue(value); }

        public List<string> ServiceTypeList { get => GetValue<List<string>>(); set => SetValue(value); }
        public List<string> CurrencyList { get => GetValue<List<string>>(); set => SetValue(value); }
        public List<string> QQSList { get => GetValue<List<string>>(); set => SetValue(value); }
        #endregion

        private Net.CreateContract createContract = new Net.CreateContract();

        public ObservableCollection<ServicesInfo> ServicesList { get; set; }

        public PageCreateContract2ViewModel(INavigation navigation, Net.CompanyInfo companyInfo) : base(navigation)
        {
            this.ServicesList = new ObservableCollection<ServicesInfo>();
            InitServiceList();

            TotalCostText = "000 sum";

            ServiceTypeList = new List<string>();
            CurrencyList = new List<string>();
            QQSList = new List<string>();

            createContract.client_company_info = new Net.CompanyInfo(companyInfo);
        }

        #region Command
        public ICommand CommandFinished => new Command(Finished);

        private async void Finished()
        {
            if (!ControlApp.InternetOk()) return;

            createContract.contract_info = new Net.CreateContractInfo()
            {
                user_phone_number = ControlApp.UserInfo.phone_number,
                open_client_info  = ControlApp.OpenClientInfo? 1: 0,
                open_search_client = ControlApp.OpenSearchClient? 1 : 0,
                client_stir = createContract.client_company_info.stir_of_company,
                service_type  = SelectedServiceType,
                contract_number  = ContractNumber,
                contract_currency  = SelectedCurrency,
                amount_of_qqs  = SelectedQQS,
                is_execise_tax  = IsExeciseTax? 1 : 0,
                interest_text  = InterestText,
                total_cost_text = TotalCostText,
                agree = Agree? 1 : 0,
                created_date = ""
           };
            createContract.service_list = new List<Net.ServicesInfo>();

            foreach (ServicesInfo item in ServicesList)
            {
                Net.ServicesInfo newItem = new Net.ServicesInfo()
                {
                    name_of_service = item.NameOfService,
                    unit_of_measure = item.SelectedMeasure,
                    amount_value = int.Parse(item.AmountText),
                    currency_text = item.SelectedMeasure,
                    contract_number = ContractNumber,
                    created_date = ""
                };
                createContract.service_list.Add(new Net.ServicesInfo(newItem));
            }

            SetTransitionType();
            await Navigation.PushAsync(new PageCreateContract3());
        }
        #endregion

        public void InitServiceList()
        {
            ServicesInfo service = new ServicesInfo();
            AddService(service);
        }

        public void AddService(ServicesInfo service)
        {
            service.SelectedCurrency = SelectedCurrency;
            this.ServicesList.Add(service);
        }

        public void UpdateServiceList()
        {
            foreach (ServicesInfo item in ServicesList)
            {
                item.SelectedCurrency = SelectedCurrency;
            }
        }

        public void RemoveService(ServicesInfo service)
        {
            this.ServicesList.Remove(service);
        }
    }
}
