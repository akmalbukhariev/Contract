using Acr.UserDialogs;
using Contract.Interfaces;
using Contract.Model;
using Contract.Pages.CanceledContracts;
using Contract.ViewModel.Pages.UnapprovedContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.UnapprovedContracts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageUnapprovedTable : IPage
    { 
        public PageUnapprovedTable()
        {
            InitializeComponent();

            SetModel(new PageUnapprovedTableViewModel(Navigation));
             
            for (int i = 0; i < grHeader.ColumnDefinitions.Count; i++)
            {
                listView.WidthRequest += grHeader.ColumnDefinitions[i].Width.Value;
            }
            listView.WidthRequest += 70; 
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Model.Parent = Parent;
            PModel.RequestInfo();

            DependencyService.Get<IRotationService>().EnableRotation();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            DependencyService.Get<IRotationService>().DisableRotation();
        }

        private void Eye_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
            ControlApp.Vibrate();
        }

        private void Check_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender); 
            ControlApp.Vibrate();

            PModel.ShowConfirmBox = true;
        }

        private void Send_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
            ControlApp.Vibrate();
        }

        private async void Cancel_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
            ControlApp.Vibrate();

            UnapprovedContract item = (UnapprovedContract)((Image)sender).BindingContext;
            if (item == null) return;

            HttpModels.CreateContractInfo canceledContract = new HttpModels.CreateContractInfo()
            {
                contract_number = item.ContractNnumber,
                comment = ""
            };

            Model.SetTransitionType(TransitionType.SlideFromBottom);
            await Navigation.PushModalAsync(new PageCancelContract(canceledContract, RSC.UnapprovedContracts1));
        }

        private PageUnapprovedTableViewModel PModel
        {
            get
            {
                return Model as PageUnapprovedTableViewModel;
            }
        }
    }
}