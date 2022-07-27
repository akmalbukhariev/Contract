using Contract.ViewModel.Pages.CurrentContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.CurrentContracts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCurrentContractTable : IPage
    { 
        public PageCurrentContractTable()
        {
            InitializeComponent();

            SetModel(new PageTableViewModel());
            (Model as PageTableViewModel).Init();

            for (int i = 0; i < grHeader.ColumnDefinitions.Count; i++)
            {
                listView.WidthRequest += grHeader.ColumnDefinitions[i].Width.Value;
            }
            listView.WidthRequest += 70;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
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

        private async void Cancel_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
            ControlApp.Vibrate();

            Model.SetTransitionType();
            await Navigation.PushModalAsync(new PageCurrentCancelContract());
        }
    }
}