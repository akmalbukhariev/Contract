using Contract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contract.ViewModel.Pages.ContractNumber
{
    public class PageEditContractNumberViewModel : BaseModel
    {
        #region Properties
        public bool EnableFomat_1 { get => GetValue<bool>(); set => SetValue(value); }
        public bool EnableFomat_2 { get => GetValue<bool>(); set => SetValue(value); }
        public bool EnableFomat_3 { get => GetValue<bool>(); set => SetValue(value); }

        public bool CheckFormat_1 { get => GetValue<bool>(); set => SetValue(value); }
        public bool CheckFormat_2 { get => GetValue<bool>(); set => SetValue(value); }
        public bool CheckFormat_3 { get => GetValue<bool>(); set => SetValue(value); }

        public string Format_1 { get => GetValue<string>(); set => SetValue(value); }
        public string Format_2 { get => GetValue<string>(); set => SetValue(value); }
        public string Format_3 { get => GetValue<string>(); set => SetValue(value); }

        public string YearFormat_1 { get => GetValue<string>(); set => SetValue(value); }
        public string YearFormat_2 { get => GetValue<string>(); set => SetValue(value); }
        public string YearFormat_3 { get => GetValue<string>(); set => SetValue(value); }

        public string TimeFormat_1 { get => GetValue<string>(); set => SetValue(value); }
        public string TimeFormat_2 { get => GetValue<string>(); set => SetValue(value); }
        public string TimeFormat_3 { get => GetValue<string>(); set => SetValue(value); }

        public string YourContractNumber { get => GetValue<string>(); set => SetValue(value); }
        #endregion

        private bool updateContractnumber = false;
        public PageEditContractNumberViewModel(INavigation navigation) : base(navigation)
        {
            EnableFomat_1 = true;
            EnableFomat_2 = false;
            EnableFomat_3 = false;

            CheckFormat_1 = true;
            CheckFormat_2 = false;
            CheckFormat_3 = false;

            string strDate = DateTime.Now.ToString("yyyyMMdd");
            string strTime = DateTime.Now.ToString("hhmmss.fff").Replace(".", "");

            YearFormat_1 = $"{strDate} -";
            YearFormat_2 = $"- {strDate} -";
            YearFormat_3 = $"{strDate} -";

            TimeFormat_1 = $"- {strTime}";
            TimeFormat_2 = $"{strTime}";
            TimeFormat_3 = $"{strTime} -"; 
        }

        public ICommand CommandSave => new Command(Save);

        public async void RequestInfo()
        {
            updateContractnumber = false;

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseContractNumber response = await Net.HttpService.GetContractNumber(ControlApp.UserInfo.phone_number);
            ControlApp.CloseLoadingView();

            if (response.result)
            {
                switch (response.data.contract_format)
                {
                    case 1: CheckFormat_1 = true;  break;
                    case 2: CheckFormat_2 = true;  break;
                    case 3: CheckFormat_3 = true;  break;
                }

                Format_1 = response.data.contract_option;
                YearFormat_1 = $"{response.data.contract_date} - ";
                TimeFormat_1 = $"- {response.data.conatrct_time}";

                Format_2 = response.data.contract_option;
                YearFormat_2 = $"- {response.data.contract_date} -";
                TimeFormat_2 = response.data.conatrct_time;

                Format_3 = response.data.contract_option;
                YearFormat_3 = $"{response.data.contract_date} -";
                TimeFormat_3 = $"{response.data.conatrct_time} -";

                string strYearFormat_1 = YearFormat_1.Replace("-", "");
                string strYearFormat_2 = YearFormat_2.Replace("-", "");
                string strYearFormat_3 = YearFormat_3.Replace("-", "");

                string strTimeFormat_1 = TimeFormat_1.Replace("-", "");
                string strTimeFormat_2 = TimeFormat_2.Replace("-", "");
                string strTimeFormat_3 = TimeFormat_3.Replace("-", "");

                switch (response.data.contract_format)
                {
                    case 1:
                        YourContractNumber = $"{strYearFormat_1} - {Format_1} - {strTimeFormat_1}";
                        break;
                    case 2:
                        YourContractNumber = $"{Format_2} - {strYearFormat_2} - {strTimeFormat_2}";
                        break;
                    case 3:
                        YourContractNumber = $"{strYearFormat_3} - {strTimeFormat_3} - {Format_3}";
                        break;
                }

                updateContractnumber = true;
            }
        }

        async void Save()
        {
            string strDate = "";
            string strTime = "";
            string strOption = "";
            int contractFormat = 1;

            if (CheckFormat_1)
            {
                strDate = YearFormat_1;
                strTime = TimeFormat_1;
                strOption = Format_1;
            }
            else if (CheckFormat_2)
            {
                strDate = YearFormat_2;
                strTime = TimeFormat_2;
                strOption = Format_2;
                contractFormat = 2;
            }
            else if (CheckFormat_3)
            {
                strDate = YearFormat_3;
                strTime = TimeFormat_3;
                strOption = Format_3;
                contractFormat = 3;
            }

            if (string.IsNullOrEmpty(strOption))
            {
                await Application.Current.MainPage.DisplayAlert(RSC.ContractNumber, RSC.FieldEmpty, RSC.Ok);
                return;
            }

            strDate = strDate.Replace("-", "");
            strTime = strTime.Replace("-", "");
            strOption = strOption.Replace("-", "");

            HttpModels.ContractNumber data = new HttpModels.ContractNumber()
            {
                user_phone_number = ControlApp.UserInfo.phone_number,
                contract_date = strDate,
                conatrct_time = strTime,
                contract_option = strOption,
                contract_format = contractFormat
            };

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseContractNumber response = updateContractnumber ?
                                              await Net.HttpService.UpdateContractNumber(data) :
                                              await Net.HttpService.SetContractNumber(data);
            ControlApp.CloseLoadingView();

            if (response.result)
            {
                await Application.Current.MainPage.DisplayAlert(RSC.ContractNumber, updateContractnumber ? RSC.SuccessfullyUpdated : RSC.SuccessfullyAdded, RSC.Ok);
            }

            await Navigation.PopAsync();
        }
    }
}
