using Contract.HttpResponse;
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
        public int SelectedServiceType_index { get => GetValue<int>(); set => SetValue(value); }
        public string ContractNumber { get => GetValue<string>(); set => SetValue(value); }
        public string SelectedCurrency { get => GetValue<string>(); set => SetValue(value); }
        public int SelectedCurrency_index { get => GetValue<int>(); set => SetValue(value); }
        public string SelectedQQS { get => GetValue<string>(); set => SetValue(value); }
        public int SelectedQQS_index { get => GetValue<int>(); set => SetValue(value); }
        public bool IsExeciseTax { get => GetValue<bool>(); set => SetValue(value); }
        public string InterestText { get => GetValue<string>(); set => SetValue(value); }
        public string TotalCostText { get => GetValue<string>(); set => SetValue(value); }
        public bool Agree { get => GetValue<bool>(); set => SetValue(value); }

        public List<string> ServiceTypeList { get => GetValue<List<string>>(); set => SetValue(value); }
        public List<string> CurrencyList { get => GetValue<List<string>>(); set => SetValue(value); }
        public List<string> QQSList { get => GetValue<List<string>>(); set => SetValue(value); }
        #endregion

        private HttpModels.CompanyInfo ClientCompanyInfo = null;

        public ObservableCollection<ServicesInfo> ServicesList { get; set; }

        public PageCreateContract2ViewModel(INavigation navigation, HttpModels.CompanyInfo companyInfo) : base(navigation)
        {
            this.ServicesList = new ObservableCollection<ServicesInfo>();
            InitServiceList();

            TotalCostText = "000 sum";

            ServiceTypeList = new List<string>();
            CurrencyList = new List<string>();
            QQSList = new List<string>();

            ClientCompanyInfo = new HttpModels.ClientCompanyInfo(companyInfo);
        }

        #region Command
        public ICommand CommandFinished => new Command(Finished);

        private async void Finished()
        {
            if (!ControlApp.InternetOk()) return;

            HttpModels.CreateContract contractinfo = new HttpModels.CreateContract()
            {
                contract_info = new HttpModels.CreateContractInfo()
                {
                    user_phone_number = ControlApp.UserInfo.phone_number,
                    open_client_info = ControlApp.OpenClientInfo ? 1 : 0,
                    open_search_client = ControlApp.OpenSearchClient ? 1 : 0,
                    client_stir = ClientCompanyInfo.stir_of_company,
                    client_company_name = ClientCompanyInfo.company_name,
                    user_company_name = ControlApp.UserCompanyInfo.company_name,
                    service_type = SelectedServiceType,
                    service_type_index = SelectedServiceType_index,
                    contract_number = ContractNumber,
                    contract_currency = SelectedCurrency,
                    contract_currency_index = SelectedCurrency_index,
                    amount_of_qqs = SelectedQQS,
                    amount_of_qqs_index = SelectedQQS_index,
                    is_execise_tax = IsExeciseTax ? 1 : 0,
                    interest_text = InterestText,
                    total_cost_text = TotalCostText,
                    agree = Agree ? 1 : 0,
                    created_date = ""
                }
            };
             
            List<HttpModels.ServicesInfo> serviceList = new List<HttpModels.ServicesInfo>();
            foreach (ServicesInfo item in ServicesList)
            {
                HttpModels.ServicesInfo newItem = new HttpModels.ServicesInfo()
                {
                    contract_number = ContractNumber,
                    name_of_service = item.NameOfService,
                    unit_of_measure = item.SelectedMeasure,
                    unit_of_measure_index = item.SelectedMeasure_index,
                    amount_value = int.Parse(item.AmountText),
                    amount_value_price = item.AmountOfPrice,
                    currency = item.SelectedCurrency,
                    created_date = ""
                };
                serviceList.Add(new HttpModels.ServicesInfo(newItem));
            }

            ControlApp.ShowLoadingView(RSC.PleaseWait);

            ResponseCreateContract responseCreate = await Net.HttpService.CreateContract(contractinfo);
            if (!responseCreate.result)
            {
                ControlApp.CloseLoadingView();
                await Application.Current.MainPage.DisplayAlert(RSC.CreateContract, RSC.Failed, RSC.Ok);
                return;
            }

            ResponseServiceInfo responseService = await Net.HttpService.SetServiceInfo(serviceList);
            if (!responseService.result)
            {
                ControlApp.CloseLoadingView();
                responseCreate = await Net.HttpService.DeleteContract(ContractNumber);
                await Application.Current.MainPage.DisplayAlert(RSC.CreateContract, RSC.Failed, RSC.Ok);
                return;
            }

            ResponseClientCompanyInfo responseClient = await Net.HttpService.SetClientCompanyInfo(ClientCompanyInfo);
            ControlApp.CloseLoadingView();

            if (responseClient.result)
            {
                SetTransitionType();
                await Navigation.PushAsync(new PageCreateContract3(ContractNumber, TotalCostText));
            }
            else
            {
                responseService = await Net.HttpService.DeleteServiceinfo(ContractNumber);
                await Application.Current.MainPage.DisplayAlert(RSC.CreateContract, RSC.Failed, RSC.Ok);
            }
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
