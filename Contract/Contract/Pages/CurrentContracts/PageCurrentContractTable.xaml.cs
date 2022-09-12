using Contract.Interfaces;
using Contract.Model;
using Contract.ViewModel.Pages.CurrentContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.CurrentContracts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCurrentContractTable : IPage
    { 
        public PageCurrentContractTable()
        {
            InitializeComponent();

            SetModel(new PageTableViewModel());

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
         
        private void Send_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
            ControlApp.Vibrate();
        }

        private async void Cancel_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
            ControlApp.Vibrate();

            CurrentContract item = (CurrentContract)((Image)sender).BindingContext;
            if (item == null) return;

            Net.CanceledContract canceledContract = new Net.CanceledContract()
            {
                user_phone_number = ControlApp.UserInfo.phone_number,
                preparer = item.Preparer,
                contract_number = item.ContractNnumber,
                company_contractor_name = item.CompanyName,
                date_of_contract = item.ContractDate,
                contract_price = item.ContractPrice,
                payment_percent = "85%",
                comment = ""

            };

            Model.SetTransitionType(TransitionType.SlideFromBottom);
            await Navigation.PushModalAsync(new PageCurrentCancelContract(canceledContract));
        }

        private PageTableViewModel PModel
        {
            get
            {
                return Model as PageTableViewModel;
            }
        }
    }
}