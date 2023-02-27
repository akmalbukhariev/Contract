using Contract.Model;
using Contract.ViewModel.Pages.Customers;
using LibContract.HttpModels;
using LibContract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.Customers
{ 
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCustomerList : IPage
    {
        private bool _isSelectable = false;

        List<SwipeView> swipeViews { set; get; }

        public PageCustomerList()
        {
            InitializeComponent();
             
            SetModel(new PageCustomerListViewModel(Navigation));
            PModel.SelectedCustomer = PressedSelectedItem;

            swipeViews = new List<SwipeView>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            PModel.RequestInfo(); 
        }

        public void IsThisPageSelectable(bool yes)  
        {
            _isSelectable = yes;
            PModel.IsThisEditable = !yes;

            if (yes)
            {
                viewNavigationBar.IsThisModalPage = true;
            } 
        }
  
        private void Delete_Invoked(object sender, EventArgs e)
        {
            var swipeItem = sender as SwipeItem;
            var item = swipeItem.BindingContext as Customer;

            if (item == null) return;

            DeleteItem(item);
        }

        private void SwipeView_SwipeStarted(object sender, SwipeStartedEventArgs e)
        {
            if (swipeViews.Count == 1)
            {
                swipeViews[0].Close();
                swipeViews.Remove(swipeViews[0]);
            }
        }

        private void SwipeView_SwipeEnded(object sender, SwipeEndedEventArgs e)
        {
            swipeViews.Add((SwipeView)sender);
        }
         
        private async void Add_Clicked(object sender, EventArgs e)
        {
            if (!ControlApp.InternetOk()) return;

            PModel.SetTransitionType();
            await Navigation.PushAsync(new PageAddOrEditCustomer());
        }

        private async void BoxMainInfo_Tapped(object sender, EventArgs e)
        {
            grMainInfo.IsVisible = false;
            await grInfo.ScaleTo(0.8, 200);
        }

        Customer _item;
        private async void PressedSelectedItem(object sender, bool longPressed)
        {
            Grid grid = (Grid)sender;
            _item = (Customer)(grid).BindingContext;

            await grid.ScaleTo(0.95, 200);
            await grid.ScaleTo(1, 200);

            ControlApp.SelectedClientCompanyInfo = GetCompanyInfo(_item);
            
            if (!longPressed && _isSelectable)
                await Navigation.PopModalAsync();
            
           if (longPressed)
            {
                if (_isSelectable) return;

                ControlApp.Vibrate();
                grMainInfo.IsVisible = true;

                lbInfoCompany.Text = _item.UserTitle;
                lbInfoStir.Text = _item.UserStir;
                boxInfoFirstLetter.IsVisible = _item.ShowLetter;
                lbInfoFirstLetter.Text = _item.FirstLetter;
                imInfoCompany.IsVisible = _item.ShowCircleImage;
                imInfoCompany.Source = _item.UserImage;

                PModel.CompanyName = ControlApp.SelectedClientCompanyInfo.company_name;
                PModel.SelectedDocument = ControlApp.SelectedClientCompanyInfo.document;
                PModel.AddressOfCompany = ControlApp.SelectedClientCompanyInfo.address_of_company;
                PModel.AccountNumber = ControlApp.SelectedClientCompanyInfo.account_number;
                PModel.CompanyStir = ControlApp.SelectedClientCompanyInfo.stir_of_company;
                PModel.NameOfBank = ControlApp.SelectedClientCompanyInfo.name_of_bank;
                PModel.BankCode = ControlApp.SelectedClientCompanyInfo.bank_code;
                PModel.QQSCode = ControlApp.SelectedClientCompanyInfo.qqs_number;
                PModel.PhoneNnumberOfCompany = ControlApp.SelectedClientCompanyInfo.company_phone_number;
                PModel.PositionOfSignatory = ControlApp.SelectedClientCompanyInfo.position_of_signer;
                PModel.FullNameOfSignatory = ControlApp.SelectedClientCompanyInfo.name_of_signer;
                PModel.AccountantName = ControlApp.SelectedClientCompanyInfo.accountant_name;
                PModel.CounselName = ControlApp.SelectedClientCompanyInfo.counsel_name;

                await grInfo.ScaleTo(1, 200);
            }
            else
            {
                await Navigation.PushAsync(new PageAddOrEditCustomer(true));
            }
        }
         
        private CompanyInfo GetCompanyInfo(Customer item)
        {
            LibContract.HttpModels.CompanyInfo result = new LibContract.HttpModels.CompanyInfo();

            foreach (LibContract.HttpModels.CompanyInfo info in PModel.ResponseClientCompanyInfo.data)
            {
                string strStir = item.UserStir.Replace($"{RSC.STIR} :", "").Trim();
                if (info.stir_of_company.Trim().Equals(strStir))
                {
                    result.Copy(info);
                    break;
                }
            }

            return result;
        }

        private PageCustomerListViewModel PModel
        {
            get
            {
                return Model as PageCustomerListViewModel;
            }
        }

        private async void Delete_Tapped(object sender, EventArgs e)
        {
            boxDelete.BackgroundColor = Color.Black;
            await Task.Delay(5);

            boxDelete.BackgroundColor = (Color)App.Current.Resources["AppColor"];
            await Task.Delay(5);

            if (_item == null) return;

            DeleteItem(_item);
        }

        async void DeleteItem(Customer item)
        {
            bool res = await DisplayAlert(RSC.Customers, RSC.DeleteMessage, RSC.Yes, RSC.No);
            if (res)
            {
                BoxMainInfo_Tapped(null, null);
                DeleteCompanyInfo data = new DeleteCompanyInfo()
                {
                    stir_of_company = item.UserStir.Replace($"{RSC.STIR} :","").Trim(),
                    user_phone_number = ControlApp.UserInfo.phone_number
                };

                ControlApp.ShowLoadingView(RSC.PleaseWait);
                ResponseClientCompanyInfo response = await Net.HttpService.DeleteClientCompanyInfo(data);
                ControlApp.CloseLoadingView();

                if (!response.result)
                {
                    await DisplayAlert(RSC.Delete, RSC.Failed, RSC.Ok);
                }
                else
                {
                    PModel.RequestInfo();
                }
            }
        } 
    } 
}