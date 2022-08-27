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

            model.Init();
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var tListView = sender as ListView;
            //tListView.SelectedItem = null;

            await Navigation.PushAsync(new PageAddOrEditCustomer());
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