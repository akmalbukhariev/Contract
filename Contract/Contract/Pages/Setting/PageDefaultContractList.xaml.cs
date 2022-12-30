using Contract.Model;
using Contract.Pages.CreateContract;
using Contract.ViewModel.Pages.Setting;
using LibContract.HttpModels;
using LibContract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.Setting
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageDefaultContractList : IPage
    {
        List<SwipeView> swipeViews { set; get; }
        public PageDefaultContractList()
        {
            InitializeComponent();

            SetModel(new PageDefaultContractListViewModel(Navigation));
            swipeViews = new List<SwipeView>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            PModel.RequestInfo();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            foreach (DefaultContract s in PModel.DataList)
            {
                s.Selected = false;
            }

            DefaultContract item = (DefaultContract)listView.SelectedItem;
            item.Selected = true; 
        }
         
        private async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            var swipeItem = sender as SwipeItem;
            var item = swipeItem.BindingContext as DefaultContract;

            if (item == null) return;

            await Navigation.PushAsync(new PageShowContract(item.Url));
        }

        private void SwipeView_SwipeStarted(object sender, SwipeStartedEventArgs e)
        {  
            if (swipeViews.Count == 1)
            {
                swipeViews[0].Close();
                swipeViews.Remove(swipeViews[0]);
            }
        }

        private void SwipeView_SwipeEnded(object sender, SwipeEndedEventArgs e)
        {
            swipeViews.Add((SwipeView)sender);
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            DefaultContract item = (DefaultContract)listView.SelectedItem;
            if (item == null) return;

            User user = new User()
            {
                phone_number = ControlApp.UserInfo.phone_number,
                default_template_id = item.id
            };

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseLogin response = await Net.HttpService.UpdateDefaultTemplate(user);
            ControlApp.CloseLoadingView();

            string strMessage = response.result ? RSC.SuccessfullyUpdated : RSC.Failed;
            await DisplayAlert(RSC.Templates, strMessage, RSC.Ok);

            ControlApp.UserInfo.default_template_id = item.id;
            //await Navigation.PopAsync();
        }

        private PageDefaultContractListViewModel PModel
        {
            get
            {
                return Model as PageDefaultContractListViewModel;
            }
        }
    }
}