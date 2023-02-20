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
        public string TypedCurrency { get => GetValue<string>(); set => SetValue(value); }
        public bool ShowTypeCurrency { get => GetValue<bool>(); set => SetValue(value); }
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
            ShowTypeCurrency = false;
            ServicesList = new ObservableCollection<ServicesInfo>();
            ServicesList.Add(new ServicesInfo());

            TemplateList = new ObservableCollection<LibContract.HttpModels.ContractTemplate>();
            CurrencyList = new List<string>();
            QQSList = new List<string>();

            ClientCompanyInfo = new LibContract.HttpModels.ClientCompanyInfo(companyInfo);

            SelectedTemplate = null; 
        }

        public async void RrequestInfo()
        {
            if (!ControlApp.InternetOk()) return;

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            var response1 = await Net.HttpService.GetContractTemplate(ControlApp.UserInfo.phone_number);
            if (!ControlApp.CheckResponse(response1)) return;

            if (response1.result)
            {
                foreach (LibContract.HttpModels.ContractTemplate item in response1.data)
                {
                    TemplateList.Add(new LibContract.HttpModels.ContractTemplate(item));
                } 
            }

            var response2 = await Net.HttpService.GetNewContractNumber(ControlApp.UserInfo.phone_number);
            if (!ControlApp.CheckResponse(response2)) return;

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
                user_stir = ControlApp.UserCompanyInfo.stir_of_company,
                client_stir = ClientCompanyInfo.stir_of_company,
                client_company_name = ClientCompanyInfo.company_name,
                user_company_name = ControlApp.UserCompanyInfo.company_name,
                template_id = SelectedTemplate.id,
                contract_sequence_number = ContractSequenceNumber,
                contract_number = strContractNumber,
                contract_currency = ShowTypeCurrency? TypedCurrency : SelectedCurrency,
                contract_currency_index = SelectedCurrency_index,
                amount_of_qqs = SelectedQQS,
                amount_of_qqs_index = SelectedQQS_index,
                is_execise_tax = IsExeciseTax ? 1 : 0,
                interest_text = InterestText,
                total_cost_text = TotalCostText,
                agree = Agree ? 1 : 0,
                created_date = "",
            };

            contractinfo.contract_currency = contractinfo.contract_currency.Replace("(UZS)", "");
            contractinfo.total_cost_text = contractinfo.total_cost_text.Replace("(UZS)", "");

            var serviceList = new List<LibContract.HttpModels.ServicesInfo>();
            foreach (ServicesInfo item in ServicesList)
            {
                LibContract.HttpModels.ServicesInfo newItem = new LibContract.HttpModels.ServicesInfo()
                {
                    contract_number = strContractNumber,
                    name_of_service = item.NameOfService,
                    unit_of_measure = item.ShowTypeMeasure? item.TypedMeasure : item.SelectedMeasure,
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
            if (!ControlApp.CheckResponse(responseCreate)) return;

            if (!responseCreate.result)
            {
                ControlApp.CloseLoadingView();
                await Application.Current.MainPage.DisplayAlert(RSC.CreateContract, RSC.Failed + ": " + responseCreate.message, RSC.Ok);
                return;
            }

            var responseService = await Net.HttpService.SetServiceInfo(serviceList);
            if (!ControlApp.CheckResponse(responseService)) return;

            if (!responseService.result)
            {
                ControlApp.CloseLoadingView();
                responseCreate = await Net.HttpService.DeleteContract(ContractNumber);
                await Application.Current.MainPage.DisplayAlert(RSC.CreateContract, RSC.Failed + ": " + responseService.message, RSC.Ok);
                return;
            }

            var responseGetClients = await Net.HttpService.GetClientCompanyInfo(ControlApp.UserInfo.phone_number);
            if (!ControlApp.CheckResponse(responseGetClients)) return;

            if (responseGetClients.result)
            {
                var found = responseGetClients.data.Find(item => item.stir_of_company.Equals(ClientCompanyInfo.stir_of_company));
                if (found == null)
                {
                    ClientCompanyInfo.id = 0;
                    await Net.HttpService.SetClientCompanyInfoToCreateContract(ClientCompanyInfo);
                }
                else
                {
                    ClientCompanyInfo.id = found.id;
                    await Net.HttpService.UpdateClientCompanyInfo(ClientCompanyInfo);
                }
            }
            else
            {
                await Net.HttpService.SetClientCompanyInfoToCreateContract(ClientCompanyInfo);
            }

            ControlApp.CloseLoadingView();
            SetTransitionType();
            await Navigation.PushAsync(new PageCreateContract3(contractinfo)); 
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
                item.SelectedCurrency = ShowTypeCurrency? TypedCurrency : SelectedCurrency;
            }
        }

        public void RemoveService(ServicesInfo service)
        {
            this.ServicesList.Remove(service);
        }
    }
}
