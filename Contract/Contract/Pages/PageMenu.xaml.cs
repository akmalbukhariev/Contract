using Contract.Control;
using Contract.Pages.EditUserContractInfo;
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
            if (!ControlApp.InternetOk()) return;

            var tListView = sender as ListView;
            tListView.SelectedItem = null;

            var item = e.Item as Model.Menu;

            //if (item.ChildMenuList != null && item.ChildMenuList.Count != 0)
            //{
            //    (Model as PageMenuViewModel).HodeOrShowMenu(item);
            //}

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
                    bool res = await Application.Current.MainPage.DisplayAlert(RSC.SignOut, RSC.SignOutText, RSC.Yes, RSC.No);
                    if (res)
                    {
                        Preferences.Set("AutoLogin", "");
                        ControlApp.UserInfo = null;
                        ControlApp.UserCompanyInfo = null;
                        Application.Current.MainPage = new TransitionNavigationPage(new Login.PageLogin());
                    }
                    break;
            }
        }

        private async void ChildMenu_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (!ControlApp.InternetOk()) return;

            var tListView = sender as ListView;
            tListView.SelectedItem = null;

            var item = e.Item as Model.ChildMenuItem;

            switch (item.ID)
            { 
                case Constants.Menu1_1:
                    if (ControlApp.UserCompanyInfo == null)
                    {
                        await DisplayAlert(RSC.MyCompany, RSC.FillInfoTitle, RSC.Ok);
                        await Navigation.PushAsync(new PageEditUserContractInfo());
                        return;
                    }
                    OnNavigatePage(new UnapprovedContracts.PageUnapprovedTable());
                    break;
                case Constants.Menu1_2:
                    if (ControlApp.UserCompanyInfo == null)
                    {
                        await DisplayAlert(RSC.MyCompany, RSC.FillInfoTitle, RSC.Ok);
                        await Navigation.PushAsync(new PageEditUserContractInfo());
                        return;
                    }
                    OnNavigatePage(new ApprovedContracts.PageApprovedTable());
                    break;
                case Constants.Menu1_3:
                    OnNavigatePage(new CanceledContracts.PageCanceledTable());
                    break;
                case Constants.Menu1_4:
                    if (ControlApp.UserCompanyInfo == null)
                    {
                        await DisplayAlert(RSC.MyCompany, RSC.FillInfoTitle, RSC.Ok);
                        await Navigation.PushAsync(new PageEditUserContractInfo());
                        return;
                    }
                    OnNavigatePage(new CreateContract.PageCreateContract1());
                    break;
                case Constants.Menu4_1:
                    OnNavigatePage(new TemplateContract.PageContractTemplateTable());
                    break;
                case Constants.Menu4_2:
                    OnNavigatePage(new TemplateContract.PageEditTemplateContract());
                    break;
                case Constants.Menu4_3:
                    OnNavigatePage(new ContractNumber.PageContractNumberList());
                    break;
                case Constants.Menu4_4:
                    OnNavigatePage(new ContractNumber.PageEditContractNumber());
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

        private async void Menu_Tapped(object sender, EventArgs e)
        {
            StackLayout menu = (StackLayout)sender;
            menu.BackgroundColor = Color.LightGray;
            await Task.Delay(100);

            menu.BackgroundColor = Color.White;
            await Task.Delay(200);

            if (!ControlApp.InternetOk()) return;
             
            var item = ((StackLayout)sender).BindingContext as Model.Menu;
            if (item == null) return; 

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
                    bool res = await Application.Current.MainPage.DisplayAlert(RSC.SignOut, RSC.SignOutText, RSC.Yes, RSC.No);
                    if (res)
                    {
                        Preferences.Set("AutoLogin", "");
                        ControlApp.UserInfo = null;
                        ControlApp.UserCompanyInfo = null;
                        Application.Current.MainPage = new TransitionNavigationPage(new Login.PageLogin());
                    }
                    break;
            }
        }

        private async void ChildMenu_Tapped(object sender, EventArgs e)
        {
            Label chMenu = (Label)sender;
            chMenu.BackgroundColor = Color.LightGray;
            await Task.Delay(100);

            chMenu.BackgroundColor = Color.White;
            await Task.Delay(200);

            if (!ControlApp.InternetOk()) return;

            var item = chMenu.BindingContext as Model.Menu;
            if (item == null) return;
             
            switch (item.ID)
            {
                case Constants.Menu1:
                    if (chMenu.Text.Equals(RSC.UnapprovedContracts1))
                    {
                        if (ControlApp.UserCompanyInfo == null)
                        {
                            await DisplayAlert(RSC.MyCompany, RSC.FillInfoTitle, RSC.Ok);
                            await Navigation.PushAsync(new PageEditUserContractInfo());
                            return;
                        }
                        OnNavigatePage(new UnapprovedContracts.PageUnapprovedTable());
                    }
                    else if (chMenu.Text.Equals(RSC.ApprovedContracts1))
                    {
                        if (ControlApp.UserCompanyInfo == null)
                        {
                            await DisplayAlert(RSC.MyCompany, RSC.FillInfoTitle, RSC.Ok);
                            await Navigation.PushAsync(new PageEditUserContractInfo());
                            return;
                        }
                        OnNavigatePage(new ApprovedContracts.PageApprovedTable());
                    }
                    else if (chMenu.Text.Equals(RSC.CanceledContracts))
                    {
                        OnNavigatePage(new CanceledContracts.PageCanceledTable());
                    }
                    else if (chMenu.Text.Equals(RSC.CreateContract))
                    {
                        if (ControlApp.UserCompanyInfo == null)
                        {
                            await DisplayAlert(RSC.MyCompany, RSC.FillInfoTitle, RSC.Ok);
                            await Navigation.PushAsync(new PageEditUserContractInfo());
                            return;
                        }
                        OnNavigatePage(new CreateContract.PageCreateContract1());
                    }
                    break;
                case Constants.Menu4:
                    if (chMenu.Text.Equals(RSC.ContractTemplates))
                    {
                        OnNavigatePage(new TemplateContract.PageContractTemplateTable());
                    }
                    else if (chMenu.Text.Equals(RSC.CreatingContractTemplate))
                    {
                        OnNavigatePage(new TemplateContract.PageEditTemplateContract());
                    }
                    else if (chMenu.Text.Equals(RSC.ContractNumberSequence))
                    {
                        OnNavigatePage(new ContractNumber.PageContractNumberList());
                    }
                    else if (chMenu.Text.Equals(RSC.CreateContractNumber))
                    {
                        OnNavigatePage(new ContractNumber.PageEditContractNumber());
                    }
                    break;
                case Constants.Menu5:
                    if (chMenu.Text.Equals(RSC.EditRequisiteInformation))
                    {
                        OnNavigatePage(new PageEditUserContractInfo());
                    }
                    else if (chMenu.Text.Equals(RSC.EditAccountPassword))
                    {
                        OnNavigatePage(new ChangePassword.PageChangePassword());
                    }
                    break;
            }
        }
    }
}