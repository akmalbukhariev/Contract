using Contract.Model;
using Contract.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseClientCompanyInfo = await HttpService.GetClientCompanyInfo("12");
            ControlApp.CloseLoadingView();

            if (ResponseClientCompanyInfo.result)
            { 
                foreach (CompanyInfo info in ResponseClientCompanyInfo.data)
                {
                    Customer item = new Customer()
                    {
                        IsThisEditClient = IsThisEditable,
                        UserImage = "rus_flag",
                        UserTitle = info.company_name,
                        UserStir = info.stir_of_company
                    };

                    Add(item);
                }
            }
        }
    }
}
