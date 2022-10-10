using Contract.Model;
using Contract.ViewModel.Pages.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.Customers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCustomerList : IPage
    {
        private bool _isSelectable = false;
        private PageCustomerListViewModel model;

        public PageCustomerList()
        {
            InitializeComponent();

            model = new PageCustomerListViewModel();
            BindingContext = model;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            model.RequestClientCompany(); 
        }

        public void IsThisPageSelectable(bool yes)
        {
            _isSelectable = yes;
            model.IsThisEditable = !yes;

            if (yes)
            {
                viewNavigationBar.IsThisModalPage = true;
            }
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var tListView = sender as ListView;
            Customer item = (Customer)tListView.SelectedItem;

            foreach (Net.CompanyInfo info in model.ResponseClientCompanyInfo.data)
            {
                string strStir = item.UserStir.Replace($"{RSC.STIR} :", "").Trim();
                if (info.stir_of_company.Trim().Equals(strStir))
                {
                    ControlApp.SelectedClientCompanyInfo = new Net.CompanyInfo();
                    ControlApp.SelectedClientCompanyInfo.Copy(info);
                     
                    break;
                }
            }

            if (_isSelectable)
                await Navigation.PopModalAsync();
            else
                await Navigation.PushAsync(new PageAddOrEditCustomer(true));
        }

        //private async void Edit_Tapped(object sender, EventArgs e)
        //{
        //    ClickAnimationView((Image)sender);
        //    ControlApp.Vibrate();

        //    Customer item = (Customer)((Image)sender).BindingContext;

        //    await Navigation.PushAsync(new PageAddOrEditCustomer(true));
        //}

        private async void Add_Clicked(object sender, EventArgs e)
        {
            model.SetTransitionType();
            await Navigation.PushAsync(new PageAddOrEditCustomer());
        }
    }
}