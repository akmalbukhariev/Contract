using Contract.ViewModel.Pages.ContractNumber;
using Contract.ViewModel.Pages.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.ContractNumber
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageContractNumberList : IPage
    {
        private PageContractNumberListViewModel model;
        public PageContractNumberList()
        {
            InitializeComponent();

            model = new PageContractNumberListViewModel();
            BindingContext = model;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            model.Init();

            for (int i = 0; i < grHeader.ColumnDefinitions.Count; i++)
            {
                listView.WidthRequest += grHeader.ColumnDefinitions[i].Width.Value;
            }
            listView.WidthRequest += 30;
        }
         
        private void Send_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
            ControlApp.Vibrate();
        }

        private void Edit_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
            ControlApp.Vibrate();
        }

        private void Cancel_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
            ControlApp.Vibrate();
        }
    }
}