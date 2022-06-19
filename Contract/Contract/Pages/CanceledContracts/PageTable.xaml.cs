using Contract.ViewModel.Pages.CanceledContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.CanceledContracts
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
            ControlApp.Vibrate();
        }

        private void Send_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
            ControlApp.Vibrate();
        }

        private void Edit_Tapped(object sender, EventArgs e)
        {
            model.ShowExplanationBox = true;
            ClickAnimationView((Image)sender);
            ControlApp.Vibrate();
        }
    }
}