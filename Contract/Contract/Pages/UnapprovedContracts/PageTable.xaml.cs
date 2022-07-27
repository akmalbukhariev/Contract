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
        public PageTable()
        {
            InitializeComponent();

            SetModel(new PageTableViewModel(Navigation));
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
            Model.Parent = Parent; 
        }

        private void Eye_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
            ControlApp.Vibrate();
        }

        private void Check_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender); 
            ControlApp.Vibrate();

            (Model as PageTableViewModel).ShowConfirmBox = true;
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

            Model.SetTransitionType(TransitionType.SlideFromBottom);
            await Navigation.PushModalAsync(new PageUnapprovedCancelContract());
        }
    }
}