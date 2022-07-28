using Contract.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contract.Pages
{
    public partial class PageMasterDetail : MasterDetailPage
    {
        private PageMain main;
        public PageMasterDetail()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            main = new PageMain();
            this.Detail = new TransitionNavigationPage(main);

            pMenu.EventNavigatePage += EventNavigatePage;
            main.EventNavigatePage += EventNavigatePage;
            main.EventShowMenu += EventShowMenu;
            pMenu.EventShowMenu += EventShowMenu;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            IsGestureEnabled = true;
        }

        private void EventShowMenu(bool show)
        {
            IsPresented = show;
        }

        private async void EventNavigatePage(Page page = null)
        {
            IsPresented = false;
            IsGestureEnabled = false;

            if (page == null) return;

            var transitionNavigationPage = Parent as TransitionNavigationPage;
            if (transitionNavigationPage != null)
                transitionNavigationPage.TransitionType = TransitionType.SlideFromRight;

            await Navigation.PushAsync(page);

            //this.Detail = new TransitionNavigationPage(page);
        } 
    }
}
