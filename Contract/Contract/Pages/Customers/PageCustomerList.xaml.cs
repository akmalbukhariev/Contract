using Contract.Model;
using Contract.ViewModel.Pages.Customers;
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
             
            SetModel(new PageCustomerListViewModel());

            swipeViews = new List<SwipeView>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            PModel.RequestClientCompany(); 
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

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (!ControlApp.InternetOk()) return;

            var tListView = sender as ListView;
            Customer item = (Customer)tListView.SelectedItem;

            foreach (LibContract.HttpModels.CompanyInfo info in PModel.ResponseClientCompanyInfo.data)
            {
                string strStir = item.UserStir.Replace($"{RSC.STIR} :", "").Trim();
                if (info.stir_of_company.Trim().Equals(strStir))
                {
                    ControlApp.SelectedClientCompanyInfo = new LibContract.HttpModels.CompanyInfo();
                    ControlApp.SelectedClientCompanyInfo.Copy(info);
                     
                    break;
                }
            }

            if (_isSelectable)
                await Navigation.PopModalAsync();
            else
                await Navigation.PushAsync(new PageAddOrEditCustomer(true));
        }

        private async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            var swipeItem = sender as SwipeItem;
            var item = swipeItem.BindingContext as Customer;

            if (item == null) return;

            bool res = await DisplayAlert(RSC.Customers, RSC.DeleteMessage, RSC.Yes, RSC.No);
            if (!res)
            {
                
            }
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

        //private async void Edit_Tapped(object sender, EventArgs e)
        //{
        //    ClickAnimationView((Image)sender);
        //    ControlApp.Vibrate();

        //    Customer item = (Customer)((Image)sender).BindingContext;

        //    await Navigation.PushAsync(new PageAddOrEditCustomer(true));
        //}

        private async void Add_Clicked(object sender, EventArgs e)
        {
            if (!ControlApp.InternetOk()) return;

            PModel.SetTransitionType();
            await Navigation.PushAsync(new PageAddOrEditCustomer());
        }

        private PageCustomerListViewModel PModel
        {
            get
            {
                return Model as PageCustomerListViewModel;
            }
        }
    }
}