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
            //model.Init();
        }

        public void IsThisPageEditable(bool yes)
        {
            model.IsThisEditClient = yes;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var tListView = sender as ListView;
            Customer item = (Customer)tListView.SelectedItem;
            //tListView.SelectedItem = null;

            foreach (Net.CompanyInfo info in model.ResponseClientCompanyInfo.data)
            {
                if (info.ctr_of_company.Trim().Equals(item.UserStir))
                {
                    ControlApp.SelectedClientCompanyInfo = new Net.CompanyInfo();
                    ControlApp.SelectedClientCompanyInfo.Copy(info);
                }
                break;
            }

            if (model.IsThisEditClient)
                await Navigation.PushAsync(new PageAddOrEditCustomer());
            else
                await Navigation.PopModalAsync();
        }

        private async void Edit_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
            ControlApp.Vibrate();

            Customer item = (Customer)((Image)sender).BindingContext;

            await Navigation.PushAsync(new PageAddOrEditCustomer());
        }
    }
}