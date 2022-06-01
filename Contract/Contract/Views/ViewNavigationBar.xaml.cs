using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewNavigationBar : ContentView
    {
        public ViewNavigationBar()
        {
            InitializeComponent();
        }

        private async void Back_Tapped(object sender, EventArgs e)
        {
            var im = (Image)sender;
            im.Source = "back_left_white";
            await Task.Delay(100);

            im.Source = "back_left_gray";
            await Task.Delay(200);

            await Navigation.PopAsync();
        }
    }
}