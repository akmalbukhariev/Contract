using Contract.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contract.Pages
{
    public abstract class IPage : ContentPage
    {
        public delegate void NavigatePage(Page page);
        public event NavigatePage EventNavigatePage;

        public delegate void ShowMenu(bool show);
        public event ShowMenu EventShowMenu;

        public Control.ControlApp ControlApp => Control.ControlApp.Instance;

        protected BaseModel Model;

        public IPage()
        {
            BackgroundColor = Color.White;
             
            //Shell.SetNavBarIsVisible(this, false);
            //Shell.SetPresentationMode(this, PresentationMode.Animated);

            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected void SetModel(BaseModel model)
        {
            Model = model;
            BindingContext = Model;
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

        protected virtual void OnNavigatePage(Page page = null)
        {
            EventNavigatePage?.Invoke(page);
        }

        protected virtual void OnShowMenu(bool show)
        {
            EventShowMenu?.Invoke(show);
        }
    }
}