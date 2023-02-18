using LibContract.HttpResponse;
using Contract.Model;
using Contract.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Contract.ViewModel.Pages.Customers
{
    public class PageCustomerListViewModel : BaseModel
    {
        public bool IsThisEditable { get => GetValue<bool>(); set => SetValue(value); } 

        public ObservableCollection<Customer> DataList { get; set; }

        public Customer SelectedCustomer { get => GetValue<Customer>(); set => SetValue(value); }

        public ResponseClientCompanyInfo ResponseClientCompanyInfo { get; set; }

        public PageCustomerListViewModel()
        {
            DataList = new ObservableCollection<Customer>();
            IsThisEditable = true;
        }
         
        public void Add(Customer item)
        {
            DataList.Add(item);
        }

        public void Remove(Customer item)
        {
            DataList.Remove(item);
        }

        public async void RequestClientCompany()
        {
            DataList.Clear();

            if (!ControlApp.InternetOk()) return;

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseClientCompanyInfo = await HttpService.GetClientCompanyInfo(ControlApp.UserInfo.phone_number);
            ControlApp.CloseLoadingView();

            if (!ControlApp.CheckResponse(ResponseClientCompanyInfo)) return;

            if (ResponseClientCompanyInfo.result)
            { 
                foreach (LibContract.HttpModels.CompanyInfo info in ResponseClientCompanyInfo.data)
                {
                    Customer item = new Customer()
                    {
                        IsThisEditClient = IsThisEditable,
                        UserImage = $"{HttpService.DATA_URL}{info.company_logo_url}",
                        UserTitle = info.company_name,
                        UserStir = $"{RSC.STIR} : {info.stir_of_company}"
                    };

                    if (string.IsNullOrEmpty(info.company_logo_url))
                    {
                        item.ShowCircleImage = false;
                        item.ShowLetter = true;
                        item.FirstLetter = info.company_name?.Length > 0? info.company_name[0].ToString() : "";
                    }

                    Add(item);
                }

                if (DataList.Count > 0)
                {
                    ShowEmptyMessage = false;
                    CloseEmptyMessage = true;

                    if (!IsThisEditable)
                        DataList.ForEach(item => item.SwipeEnable = false);
                }
                else
                {
                    ShowEmptyMessage = true;
                    CloseEmptyMessage = false;
                }
            }
        }
    }
}
