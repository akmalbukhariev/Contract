using LibContract.HttpResponse;
using Contract.Interfaces;
using Contract.Model;
using Contract.ViewModel.Pages.TemplateContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.TemplateContract
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageContractTemplateTable : IPage
    { 
        public PageContractTemplateTable()
        {
            InitializeComponent();

            SetModel(new PageContractTemplateTableViewModel());
              
            for (int i = 0; i < grHeader.ColumnDefinitions.Count; i++)
            {
                listView.WidthRequest += grHeader.ColumnDefinitions[i].Width.Value;
            }
            listView.WidthRequest += 70;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
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

        private async void Edit_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
            ControlApp.Vibrate();

            ContractTemplate item = (ContractTemplate)((Image)sender).BindingContext;
            if (item == null) return;

            await Navigation.PushAsync(new PageEditTemplateContract(item.TemplateInfo));
        }

        private async void Delete_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
            ControlApp.Vibrate();

            ContractTemplate item = (ContractTemplate)((Image)sender).BindingContext;
            if (item == null) return;

            if (!await DisplayAlert(RSC.ContractTemplates, RSC.DeleteMessage, RSC.Yes, RSC.No)) return;

            ControlApp.ShowLoadingView(RSC.Please);
            ResponseContractTemplate response = await Net.HttpService.DeleteContractTemplate(item.TemplateInfo);
            ControlApp.CloseLoadingView();

            string strMessage = response.result ? RSC.SuccessfullyCompleted : RSC.Failed;
            await DisplayAlert(RSC.Templates, strMessage, RSC.Ok);

            if (response.result)
                PModel.RequestInfo();
        }

        PageContractTemplateTableViewModel PModel
        {
            get
            {
                return Model as PageContractTemplateTableViewModel;
            }
        }
    }
}