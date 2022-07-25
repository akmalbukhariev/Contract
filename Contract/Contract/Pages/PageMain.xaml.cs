using Contract.Model;
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
    public partial class PageMain : IPage
    {
        public delegate void ShowMenu(bool show);
        public event ShowMenu EventShowMenu;

        private PageMainViewModel model;
        public PageMain()
        {
            InitializeComponent();

            model = new PageMainViewModel();
            BindingContext = model;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            model.Parent = Parent;
            model.Init();
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = sender as ListView;
            ChildMenuItem item = listView.SelectedItem as ChildMenuItem;
            
            model.SetTransitionType();
            switch (item.ID)
            {
                case Constant.Menu1: 
                    await Navigation.PushAsync(new UnapprovedContracts.PageTable());
                    break;
                case Constant.Menu2:
                    await Navigation.PushAsync(new CurrentContracts.PageCurrentContractTable());
                    break;
                case Constant.Menu3:
                    await Navigation.PushAsync(new CanceledContracts.PageCanceledTable());
                    break;
            }

            listView.SelectedItem = null;
        }

        private void ShowMenu_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
            EventShowMenu?.Invoke(true);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            model.SetTransitionType();
            await Navigation.PushAsync(new CreateContract.PageCreateContract1());
        }
    }
}