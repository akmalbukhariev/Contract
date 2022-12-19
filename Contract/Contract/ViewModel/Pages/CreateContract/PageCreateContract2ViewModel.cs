using LibContract.HttpResponse;
using Contract.Model;
using Contract.Pages.CreateContract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contract.ViewModel.Pages.CreateContract
{
    public class PageCreateContract2ViewModel : BaseModel
    {
        #region Properties
        public string ContractNumber { get => GetValue<string>(); set => SetValue(value); }
        public string ContractSequenceNumber { get; set; } = ""; 
        public string SelectedCurrency { get => GetValue<string>(); set => SetValue(value); }
        public int SelectedCurrency_index { get => GetValue<int>(); set => SetValue(value); }
        public string SelectedQQS { get => GetValue<string>(); set => SetValue(value); }
        public int SelectedQQS_index { get => GetValue<int>(); set => SetValue(value); }
        public bool IsExeciseTax { get => GetValue<bool>(); set => SetValue(value); }
        public string InterestText { get => GetValue<string>(); set => SetValue(value); }
        public string TotalCostText { get => GetValue<string>(); set => SetValue(value); }
        public bool Agree { get => GetValue<bool>(); set => SetValue(value); }
         
        public List<string> CurrencyList { get => GetValue<List<string>>(); set => SetValue(value); }
        public List<string> QQSList { get => GetValue<List<string>>(); set => SetValue(value); }

        public LibContract.HttpModels.ContractTemplate SelectedTemplate { get => GetValue<LibContract.HttpModels.ContractTemplate>(); set => SetValue(value); }
        #endregion

        public ResponseContractNumberTemplate ResponseContractNumberTemplateInfo = null;
        private LibContract.HttpModels.CompanyInfo ClientCompanyInfo = null;

        public ObservableCollection<ServicesInfo> ServicesList { get; set; }
        public ObservableCollection<LibContract.HttpModels.ContractTemplate> TemplateList { get; set; }

        public PageCreateContract2ViewModel(INavigation navigation, LibContract.HttpModels.CompanyInfo companyInfo): base(navigation)
        {
            ServicesList = new ObservableCollection<ServicesInfo>();
            ServicesList.Add(new ServicesInfo());

            TemplateList = new ObservableCollection<LibContract.HttpModels.ContractTemplate>();
            CurrencyList = new List<string>();
            QQSList = new List<string>();

            ClientCompanyInfo = new LibContract.HttpModels.ClientCompanyInfo(companyInfo);

            SelectedTemplate = null;
            #region 
            //TotalCostText = "000 sum";
            //SelectedServiceType = "";
            //SelectedServiceType_index = 0;
            //ContractNumber = "890701";
            //SelectedCurrency = "RUB";
            //SelectedCurrency_index = 1;
            //SelectedQQS = "15%";
            //SelectedQQS_index = 0;
            //IsExeciseTax = false;
            //InterestText = "13%";

            //ServicesInfo item1 = new ServicesInfo()
            //{
            //    NameOfService = "Test1",
            //    SelectedMeasure = "Liter",
            //    SelectedMeasure_index = 2,
            //    AmountText = "7",
            //    AmountOfPrice = "13",
            //};
            //ServicesInfo item2 = new ServicesInfo()
            //{
            //    NameOfService = "Test2",
            //    SelectedMeasure = "Liter",
            //    SelectedMeasure_index = 2,
            //    AmountText = "2",
            //    AmountOfPrice = "10",
            //};
            //ServicesInfo item3 = new ServicesInfo()
            //{
            //    NameOfService = "Test3",
            //    SelectedMeasure = "Liter",
            //    SelectedMeasure_index = 2,
            //    AmountText = "70",
            //    AmountOfPrice = "3",
            //};

            //ServicesList.Add(item1);
            //ServicesList.Add(item2);
            //ServicesList.Add(item3);
            #endregion
        }

        public async void RrequestInfo()
        {
            ControlApp.ShowLoadingView(RSC.PleaseWait);
            var response1 = await Net.HttpService.GetContractTemplate(ControlApp.UserInfo.phone_number);
            if (response1.result)
            {
                foreach (LibContract.HttpModels.ContractTemplate item in response1.data)
                {
                    TemplateList.Add(new LibContract.HttpModels.ContractTemplate(item));
                } 
            }

            var response2 = await Net.HttpService.GetNewContractNumber(ControlApp.UserInfo.phone_number);
            ContractSequenceNumber = response2.result ? response2.new_contract_sequence_number : "";

            ResponseContractNumberTemplateInfo = await Net.HttpService.GetContractNumber(ControlApp.UserInfo.phone_number);
              
            ControlApp.CloseLoadingView();
        }

        #region Command
        public ICommand CommandFinished => new Command(Finished);

        private async void Finished()
        {
            if (!ControlApp.InternetOk()) return;

            if (string.IsNullOrEmpty(ContractNumber))
            {
                await Application.Current.MainPage.DisplayAlert(RSC.CreateContract, RSC.FieldEmpty, RSC.Ok);
                return;
            }

            if (!Agree)
            {
                await Application.Current.MainPage.DisplayAlert(RSC.CreateContract, RSC.AgreeMessage, RSC.Ok);
                return;
            }
             
            string strNumber = Regex.Replace(ContractNumber, @"\s", "");
            string strContractNumber = $"{ControlApp.UserInfo.phone_number}_{strNumber.Replace("-", "_")}";

            var contractinfo = new LibContract.HttpModels.CreateContractInfo()
            {
                user_phone_number = ControlApp.UserInfo.phone_number,
                open_client_info = ControlApp.OpenClientInfo ? 1 : 0,
                open_search_client = ControlApp.OpenSearchClient ? 1 : 0,
                client_stir = ClientCompanyInfo.stir_of_company,
                client_company_name = ClientCompanyInfo.company_name,
                user_company_name = ControlApp.UserCompanyInfo.company_name,
                template_id = SelectedTemplate.id,
                contract_sequence_number = ContractSequenceNumber,
                contract_number = strContractNumber,
                contract_currency = SelectedCurrency,
                contract_currency_index = SelectedCurrency_index,
                amount_of_qqs = SelectedQQS,
                amount_of_qqs_index = SelectedQQS_index,
                is_execise_tax = IsExeciseTax ? 1 : 0,
                interest_text = InterestText,
                total_cost_text = TotalCostText,
                agree = Agree ? 1 : 0,
                created_date = "",
            };
             
            var serviceList = new List<LibContract.HttpModels.ServicesInfo>();
            foreach (ServicesInfo item in ServicesList)
            {
                LibContract.HttpModels.ServicesInfo newItem = new LibContract.HttpModels.ServicesInfo()
                {
                    contract_number = strContractNumber,
                    name_of_service = item.NameOfService,
                    unit_of_measure = item.SelectedMeasure,
                    unit_of_measure_index = item.SelectedMeasure_index,
                    amount_value = int.Parse(item.AmountText),
                    amount_value_price = item.AmountOfPrice,
                    currency = item.SelectedCurrency,
                    created_date = ""
                };
                serviceList.Add(new LibContract.HttpModels.ServicesInfo(newItem));
            }

            ControlApp.ShowLoadingView(RSC.PleaseWait);

            var responseCreate = await Net.HttpService.CreateContract(contractinfo);
            if (!responseCreate.result)
            {
                ControlApp.CloseLoadingView();
                await Application.Current.MainPage.DisplayAlert(RSC.CreateContract, RSC.Failed + ": " + responseCreate.message, RSC.Ok);
                return;
            }

            var responseService = await Net.HttpService.SetServiceInfo(serviceList);
            if (!responseService.result)
            {
                ControlApp.CloseLoadingView();
                responseCreate = await Net.HttpService.DeleteContract(ContractNumber);
                await Application.Current.MainPage.DisplayAlert(RSC.CreateContract, RSC.Failed + ": " + responseService.message, RSC.Ok);
                return;
            }

            var responseClient = await Net.HttpService.SetClientCompanyInfo(ClientCompanyInfo);
            ControlApp.CloseLoadingView();

            if (responseClient.result)
            {
                SetTransitionType();
                await Navigation.PushAsync(new PageCreateContract3(contractinfo));
            }
            else
            {
                responseService = await Net.HttpService.DeleteServiceinfo(ContractNumber);
                await Application.Current.MainPage.DisplayAlert(RSC.CreateContract, $"{RSC.Failed} : {responseClient.message}", RSC.Ok);
            }
        }
        #endregion
  
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
