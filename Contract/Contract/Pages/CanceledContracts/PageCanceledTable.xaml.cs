using Contract.Interfaces;
using Contract.Model;
using Contract.ViewModel.Pages.CanceledContracts;
using LibContract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.CanceledContracts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCanceledTable : IPage
    { 
        public PageCanceledTable()
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

        private async void Eye_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
            ControlApp.Vibrate();

            CanceledContract item = (CanceledContract)((Image)sender).BindingContext;
            if (item == null) return;

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseCreatePdf response = await Net.HttpService.CreateContractPdf(item.ContractNnumberReal);
            if (response.result)
            {
                await DisplayAlert(RSC.CreateContract, RSC.SuccessfullyCompleted, RSC.Ok);
            }
            else
            {
                await DisplayAlert(RSC.CreateContract, response.message, RSC.Ok);
            }
            ControlApp.CloseLoadingView();
        }

        private void Send_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
            ControlApp.Vibrate();
        }

        private void Commit_Tapped(object sender, EventArgs e)
        { 
            ClickAnimationView((Image)sender);
            ControlApp.Vibrate();

            CanceledContract item = (CanceledContract)((Image)sender).BindingContext;
            if (item == null) return;

            PModel.ExplanationText = item.CommentText;
            PModel.ShowExplanationBox = true;
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