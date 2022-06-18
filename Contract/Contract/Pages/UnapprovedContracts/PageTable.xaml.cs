using Acr.UserDialogs;
using Contract.ViewModel.Pages.UnapprovedContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.UnapprovedContracts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageTable : IPage
    {
        private PageTableViewModel model;
        public PageTable()
        {
            InitializeComponent();

            model = new PageTableViewModel();
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
        }

        private void Check_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);

            backBox.IsVisible = true;
            viewConfirm.IsVisible = true;
        }

        private void Send_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
        }

        private void Cancel_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
        }
    }
}