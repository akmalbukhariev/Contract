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
    public partial class PageContractTemplate : IPage
    {
        private PageContractTemplateViewModel model;
        public PageContractTemplate()
        {
            InitializeComponent();

            model = new PageContractTemplateViewModel();
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
            listView.WidthRequest += 70;
        }

        private void Eye_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
            ControlApp.Vibrate();
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