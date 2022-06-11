using Contract.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMenu : ContentPage
    {
        private PageMenuViewModel model;
        public PageMenu()
        {
            InitializeComponent();

            model = new PageMenuViewModel();
            BindingContext = model;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            model.InitMenu();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as Model.Menu;

            if (item.ChildMenuList == null || item.ChildMenuList.Count == 0) return;

            model.HodeOrShowMenu(item);
        }
    }
}