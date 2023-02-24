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
    public class PageCustomerListViewModel : BaseCompanyInfoModel
    {
        public bool IsThisEditable { get => GetValue<bool>(); set => SetValue(value); } 

        public ObservableCollection<Customer> DataList { get; set; }
         
        public ItemClicked SelectedCustomer;
        public ResponseClientCompanyInfo ResponseClientCompanyInfo { get; set; }

        public PageCustomerListViewModel(INavigation navigation) : base(navigation)
        {
            DataList = new ObservableCollection<Customer>();
            IsThisEditable = true;
        }

        public ICommand RefreshCommand => new Command(Refresh);

        private void Refresh()
        {
            ControlApp.Vibrate();
            RequestInfo();
        }

        public void Add(Customer item)
        {
            DataList.Add(item);
        }

        public void Remove(Customer item)
        {
            DataList.Remove(item);
        }

        public async void RequestInfo()
        {
            DataList.Clear();

            if (!ControlApp.InternetOk()) return;

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseClientCompanyInfo = await HttpService.GetClientCompanyInfo(ControlApp.UserInfo.phone_number);
            ControlApp.CloseLoadingView();

            if (!ControlApp.CheckResponse(ResponseClientCompanyInfo))
            {
                IsRefreshing = false;
                return;
            }

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

                    item.EventShowInfoPressed += Item_EventShowInfoPressed;
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

            IsRefreshing = false;
        }

        public void Item_EventShowInfoPressed(object sender, bool longPressed)
        {
            SelectedCustomer(sender, longPressed); 
        } 
    }
}
