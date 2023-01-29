using LibContract.HttpResponse;
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
        public string SequenceNumber { get => GetValue<string>(); set => SetValue(value); } 
        public string YourContractNumber { get => GetValue<string>(); set => SetValue(value); }
        #endregion

        private string OldOption = string.Empty;
        //private bool updateContractnumber = false;

        Model.ContractNumber ContractNumber;

        public PageEditContractNumberViewModel(INavigation navigation, Model.ContractNumber contractNumber) : base(navigation)
        {
            ContractNumber = contractNumber;
            EnableFomat_1 = true;
            EnableFomat_2 = false;
            EnableFomat_3 = false;

            CheckFormat_1 = true;
            CheckFormat_2 = false;
            CheckFormat_3 = false;

            SequenceNumber = Constants.ContractSequenceNumber;

            Option = "";
            OldOption = "";
            if (contractNumber != null)
                OldOption = Option = contractNumber.ContractNumberText.Replace(Constants.ContractSequenceNumber, "").Replace("-", "");
        }

        public ICommand CommandSaveUpdate => new Command(SaveUpdate);
          
        async void SaveUpdate()
        {
            if (!ControlApp.InternetOk()) return;

            int contractFormat = 1;

            if (CheckFormat_2)
            {
                contractFormat = 2;
            }
            else if (CheckFormat_3)
            {
                contractFormat = 3;
            }

            if (ContractNumber != null && OldOption.Trim().Equals(Option.Trim()))
            {
                await Navigation.PopAsync();
                return;
            }

            if (!CheckFormat_1 && string.IsNullOrEmpty(Option.Trim()))
            {
                await Application.Current.MainPage.DisplayAlert(RSC.ContractNumber, RSC.FieldEmpty, RSC.Ok);
                return;
            }
              
            LibContract.HttpModels.ContractNumberTemplate data = new LibContract.HttpModels.ContractNumberTemplate()
            { 
                user_phone_number = ControlApp.UserInfo.phone_number,
                option = Option,
                format = contractFormat,
                created_date = ContractNumber == null ? "" : ContractNumber.CreatedDate
            };

            if (ContractNumber != null)
                data.id = ContractNumber.Id;

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseContractNumberTemplate response = ContractNumber == null ? await Net.HttpService.SetContractNumber(data) :
                                                                               await Net.HttpService.UpdateContractNumber(data);
            ControlApp.CloseLoadingView();

            if (!ControlApp.CheckResponse(response)) return;

            if (response.result)
            {
                await Application.Current.MainPage.DisplayAlert(RSC.ContractNumber, ContractNumber == null ? RSC.SuccessfullyAdded : RSC.SuccessfullyUpdated, RSC.Ok);
                await Navigation.PopAsync();
            }
            else if (response.message.Equals("Exist"))
            {
                await Application.Current.MainPage.DisplayAlert(RSC.ContractNumber, RSC.ContractFormatExist, RSC.Ok);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(RSC.ContractNumber, RSC.Failed, RSC.Ok);
            }

        }
    }
}
