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
        public bool IsThisEditClient { get => GetValue<bool>(); set => SetValue(value); }
        public ObservableCollection<Customer> DataList { get; set; }

        public Customer SelectedCustomer { get => GetValue<Customer>(); set => SetValue(value); }

        public ResponseClientCompanyInfo ResponseClientCompanyInfo { get; set; }

        public PageCustomerListViewModel()
        {
            DataList = new ObservableCollection<Customer>();
            IsThisEditClient = true;
        }

        public void Init()
        {
            //Customer item1 = new Customer()
            //{
            //    UserImage = "cola",
            //    UserTitle = "COCA COLA Ichimligi",
            //    UserStir = "STIR: 000 000 000"
            //};

            //Customer item2 = new Customer()
            //{
            //    UserImage = "cola",
            //    UserTitle = "Samsung Electronics MChJ",
            //    UserStir = "STIR: 000 000 000"
            //};

            //Customer item3 = new Customer()
            //{
            //    UserImage = "cola",
            //    UserTitle = "Uzbekistan Airways",
            //    UserStir = "STIR: 000 000 000"
            //};

            //Customer item4 = new Customer()
            //{
            //    UserImage = "cola",
            //    UserTitle = "VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV",
            //    UserStir = "STIR: 000 000 000"
            //};

            //Customer item5 = new Customer()
            //{
            //    UserImage = "eng_flag",
            //    UserTitle = "VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV",
            //    UserStir = "STIR: 000 000 000"
            //};

            //Customer item6 = new Customer()
            //{
            //    UserImage = "rus_flag",
            //    UserTitle = "VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV",
            //    UserStir = "STIR: 000 000 000"
            //};

            //Customer item7 = new Customer()
            //{
            //    UserImage = "uzb_flag",
            //    UserTitle = "VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV",
            //    UserStir = "STIR: 000 000 000"
            //};

            //Add(item1);
            //Add(item2);
            //Add(item3);
            //Add(item4);
            //Add(item5);
            //Add(item6);
            //Add(item7);
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
            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseClientCompanyInfo = await HttpService.GetClientCompanyInfo("12");
            ControlApp.CloseLoadingView();

            if (ResponseClientCompanyInfo.result)
            { 
                foreach (CompanyInfo info in ResponseClientCompanyInfo.data)
                {
                    Customer item = new Customer()
                    {
                        IsThisEditClient = IsThisEditClient,
                        UserImage = "rus_flag",
                        UserTitle = info.company_name,
                        UserStir = info.ctr_of_company
                    };

                    Add(item);
                }
            }
        }
    }
}
