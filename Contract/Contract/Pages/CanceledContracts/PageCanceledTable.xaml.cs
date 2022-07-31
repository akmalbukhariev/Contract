using Contract.Interfaces;
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
    public partial class PageCanceledTable : IPage
    { 
        public PageCanceledTable()
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

            DependencyService.Get<IRotationService>().EnableRotation();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            DependencyService.Get<IRotationService>().DisableRotation();
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
            (Model as PageTableViewModel).ShowExplanationBox = true;
            ClickAnimationView((Image)sender);
            ControlApp.Vibrate();
        }
    }
}