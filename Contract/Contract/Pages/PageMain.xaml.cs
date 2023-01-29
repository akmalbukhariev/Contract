using Contract.Model;
using Contract.Pages.EditUserContractInfo;
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
        public PageMain()
        {
            InitializeComponent();

            SetModel(new PageMainViewModel());
            PModel.Init();
        }
            
        protected async override void OnAppearing()
        {   
            base.OnAppearing();
            Model.Parent = Parent;

            lbMyCompany.Text = RSC.MyCompany;
            lbIdNumber.Text = RSC.IDNumber;
            btnCreateContract.Text = RSC.CreateContract;
            lbAllContracts.Text = RSC.AllContracts;

            PModel.Clean();
            PModel.Init();

            if (ControlApp.UserCompanyInfo != null)
                PModel.RequestInfo();

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            LibContract.HttpResponse.ResponseSignatureInfo response = await Net.HttpService.CheckSignature(ControlApp.UserInfo.phone_number);
            ControlApp.CloseLoadingView();

            if (!ControlApp.CheckResponse(response)) return;

            if (!response.result)
            {
                await DisplayAlert(RSC.YourSignature, RSC.EnterSignature, RSC.Ok);
                await Navigation.PushAsync(new UnapprovedContracts.PageSign());
            }

            //if (Notification.PushNotification.Instance.ClickedNotification)
            //{
            //    await Navigation.PushAsync(new UnapprovedContracts.PageUnapprovedTable());
            //    Notification.PushNotification.Instance.ClickedNotification = false;
            //}
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (!ControlApp.InternetOk()) return;

            if (ControlApp.UserCompanyInfo == null)
            {
                await DisplayAlert(RSC.MyCompany, RSC.FillInfoTitle, RSC.Ok);
                await Navigation.PushAsync(new PageEditUserContractInfo());
                return;
            }

            var listView = sender as ListView;
            ChildMenuItem item = listView.SelectedItem as ChildMenuItem;
             
            switch (item.ID)
            {
                case Constants.Menu1:
                    OnNavigatePage(new UnapprovedContracts.PageUnapprovedTable()); 
                    break;
                case Constants.Menu2:
                    OnNavigatePage(new ApprovedContracts.PageApprovedTable()); 
                    break;
                case Constants.Menu3:
                    OnNavigatePage(new CanceledContracts.PageCanceledTable()); 
                    break;
            }

            listView.SelectedItem = null;
        }

        private void ShowMenu_Tapped(object sender, EventArgs e)
        {
            if (!ControlApp.InternetOk()) return;

            ClickAnimationView((Image)sender);
            OnShowMenu(true);
        }

        private async void CreateContract_Clicked(object sender, EventArgs e)
        {
            if (!ControlApp.InternetOk()) return;

            if (ControlApp.UserCompanyInfo == null)
            {
                await DisplayAlert(RSC.MyCompany, RSC.FillInfoTitle, RSC.Ok);
                await Navigation.PushAsync(new PageEditUserContractInfo());
            }
            else
            {
                OnNavigatePage(new CreateContract.PageCreateContract1());
            }
        }

        private PageMainViewModel PModel
        {
            get
            {
                return (Model as PageMainViewModel);
            }
        }
    }
}