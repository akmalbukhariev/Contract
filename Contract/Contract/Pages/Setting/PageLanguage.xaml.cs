using Contract.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.Setting
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageLanguage : IPage
    {
        public PageLanguage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            lbAppVersion.Text = RSC.AppVersion + " " + ControlApp.AppVersion;
        }

        private void Item_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((ViewLanguage)sender); 
        }

        private void DragGestureRecognizer_DragStarting(object sender, DragStartingEventArgs e)
        {

        }

        private void DropGestureRecognizer_DragOver(object sender, DragEventArgs e)
        {

        }
    }
}