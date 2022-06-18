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
    public partial class ViewConfirm : ContentView
    {
        public ViewConfirm()
        {
            InitializeComponent();
        }

        private void Eye_Tapped(object sender, EventArgs e)
        {
            ChangeBack((Grid)sender);
        }

        private void Finger_Tapped(object sender, EventArgs e)
        {
            ChangeBack((Grid)sender);
        }

        async void ChangeBack(Grid grid)
        {
            grid.BackgroundColor = Color.Gray;
            await Task.Delay(100);

            grid.BackgroundColor= Color.White;
            await Task.Delay(200);
        }
    }
}