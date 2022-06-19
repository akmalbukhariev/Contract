using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contract.Pages
{
    public class IPage : ContentPage
    {
        public Control.ControlApp ControlApp => Control.ControlApp.Instance;
        public IPage()
        {
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected async void ClickAnimationView<T>(T view) where T : View
        {
            await view.ScaleTo(0.8, 200);
            await view.ScaleTo(1, 200, Easing.SpringOut);
        }

        protected async void ChangeClickBackColor(Label label, Color changeColor, Color orgColor)
        {
            label.TextColor = changeColor;
            await Task.Delay(100);

            label.TextColor = orgColor;
            await Task.Delay(200);
        }
    }
}