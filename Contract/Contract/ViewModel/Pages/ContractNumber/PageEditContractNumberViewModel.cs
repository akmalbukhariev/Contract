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

        public string Option { get => GetValue<string>(); set => SetValue(value); } 
        public string SequenceNumber1 { get => GetValue<string>(); set => SetValue(value); }
        public string SequenceNumber2 { get => GetValue<string>(); set => SetValue(value); }
        public string SequenceNumber3 { get => GetValue<string>(); set => SetValue(value); }
        public string YourContractNumber { get => GetValue<string>(); set => SetValue(value); }
        #endregion

        private string OldOption = string.Empty;
        private bool updateContractnumber = false;
        public PageEditContractNumberViewModel(INavigation navigation) : base(navigation)
        {
            EnableFomat_1 = true;
            EnableFomat_2 = false;
            EnableFomat_3 = false;

            CheckFormat_1 = true;
            CheckFormat_2 = false;
            CheckFormat_3 = false;

            SequenceNumber1 = "00001";
            SequenceNumber2 = "- 00001";
            SequenceNumber3 = "00001 -";
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
                switch (response.data.format)
                {
                    case 1: CheckFormat_1 = true;  break;
                    case 2: CheckFormat_2 = true;  break;
                    case 3: CheckFormat_3 = true;  break;
                }

                OldOption = Option = response.data.option;
                SequenceNumber1 = response.data.sequence_number;
                SequenceNumber2 = $"- {SequenceNumber1}";
                SequenceNumber3 = $"{SequenceNumber1} -";
                   
                switch (response.data.format)
                {
                    case 1:
                        YourContractNumber = SequenceNumber1;
                        break;
                    case 2:
                        YourContractNumber = $"{Option}{SequenceNumber2}";
                        break;
                    case 3:
                        YourContractNumber = $"{SequenceNumber3} {Option}";
                        break;
                }

                updateContractnumber = true;
            }
        }

        async void Save()
        { 
            int contractFormat = 1;
            string strOption = "";
            string strSequenceNumber = "";

            if (CheckFormat_1)
            {
                strOption = "";
                strSequenceNumber = SequenceNumber1;
            }
            else if (CheckFormat_2)
            {
                strOption = Option;
                strSequenceNumber = SequenceNumber2;
                contractFormat = 2;
            }
            else if (CheckFormat_3)
            {
                strOption = Option;
                strSequenceNumber = SequenceNumber3;
                contractFormat = 3;
            }

            if (!CheckFormat_1 && string.IsNullOrEmpty(strOption.Trim()))
            {
                await Application.Current.MainPage.DisplayAlert(RSC.ContractNumber, RSC.FieldEmpty, RSC.Ok);
                return;
            }

            strSequenceNumber = strSequenceNumber.Replace("-", "");

            if (OldOption.Trim().Equals(strOption))
            {
                await Navigation.PopAsync();
                return;
            }

            HttpModels.ContractNumber data = new HttpModels.ContractNumber()
            {
                user_phone_number = ControlApp.UserInfo.phone_number,
                sequence_number = strSequenceNumber.Replace("-", ""),
                option = strOption,
                format = contractFormat
            };

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseContractNumber response = updateContractnumber ?
                                              await Net.HttpService.UpdateContractNumber(data) :
                                              await Net.HttpService.SetContractNumber(data);
            ControlApp.CloseLoadingView();

            if (response.result)
            {
                await Application.Current.MainPage.DisplayAlert(RSC.ContractNumber, updateContractnumber ? RSC.SuccessfullyUpdated : RSC.SuccessfullyAdded, RSC.Ok);
                await Navigation.PopAsync();
            }
            //else if (response.message.Equals("Exist"))
            //{
            //    await Application.Current.MainPage.DisplayAlert(RSC.ContractNumber, RSC.ContractFormatExist, RSC.Ok);
            //}
            else
            {
                await Application.Current.MainPage.DisplayAlert(RSC.ContractNumber, RSC.Failed, RSC.Ok);
            } 
        }
    }
}
