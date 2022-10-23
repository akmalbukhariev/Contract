using Contract.Control;
using Contract.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMenu : IPage
    {  
        public PageMenu()
        {
            InitializeComponent();

            SetModel(new PageMenuViewModel());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Model.Parent = Parent;
            (Model as PageMenuViewModel).Clean();
            (Model as PageMenuViewModel).InitMenu();
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var tListView = sender as ListView;
            tListView.SelectedItem = null;

            var item = e.Item as Model.Menu;

            if (item.ChildMenuList != null && item.ChildMenuList.Count != 0)
            {
                (Model as PageMenuViewModel).HodeOrShowMenu(item);
            }

            switch (item.ID)
            {
                case Constants.Menu1: break;
                case Constants.Menu2: break;
                case Constants.Menu3:
                    OnNavigatePage(new Customers.PageCustomerList());
                    break;
                case Constants.Menu4: break;
                case Constants.Menu5: break;
                case Constants.Menu6:
                    OnNavigatePage(new Setting.PageSetting());
                    break;
                case Constants.Menu7:
                    OnNavigatePage(new Setting.PageAbout());
                    break;
                case Constants.Menu8:
                    OnNavigatePage(new Setting.PageSuggestion());
                    break;
                case Constants.Menu9:
                    OnShowMenu(false);
                    bool res = await Application.Current.MainPage.DisplayAlert(RSC.SignOut, RSC.SignOutText, RSC.Ok, RSC.Cancel);
                    if (res)
                    {
                        Preferences.Set("AutoLogin", ""); 
                        Application.Current.MainPage = new TransitionNavigationPage(new Login.PageLogin());
                    }
                    break;
            }
        }

        private void ChildMenu_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var tListView = sender as ListView;
            tListView.SelectedItem = null;

            var item = e.Item as Model.ChildMenuItem;

            switch (item.ID)
            { 
                case Constants.Menu1_1: 
                    OnNavigatePage(new UnapprovedContracts.PageUnapprovedTable());
                    break;
                case Constants.Menu1_2:
                    OnNavigatePage(new ApprovedContracts.PageApprovedTable());
                    break;
                case Constants.Menu1_3:
                    OnNavigatePage(new CanceledContracts.PageCanceledTable());
                    break;
                case Constants.Menu1_4:
                    OnNavigatePage(new CreateContract.PageCreateContract1());
                    break;
                case Constants.Menu4_1:
                    OnNavigatePage(new TemplateContract.PageContractTemplate());
                    break;
                case Constants.Menu4_2:
                    OnNavigatePage(new TemplateContract.PageEditTemplateContract());
                    break;
                case Constants.Menu4_3:
                    OnNavigatePage(new ContractNumber.PageContractNumberList());
                    break;
                case Constants.Menu4_4:
                    OnNavigatePage(new ContractNumber.PageChangeContractNumber());
                    break;
                case Constants.Menu5_1:
                    OnNavigatePage(new EditUserContractInfo.PageEditUserContractInfo());
                    break;
                case Constants.Menu5_2:
                    OnNavigatePage(new ChangePassword.PageChangePassword());
                    break;
                case Constants.Menu5_3: break;
            }
        }
    }
}