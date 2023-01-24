using Contract.Model;
using Contract.ViewModel.Pages.TemplateContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.TemplateContract
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageEditTemplateContract : IPage
    {
        bool view = false;
        public PageEditTemplateContract(LibContract.HttpModels.ContractTemplate templateInfo = null, bool clickView = false)
        {
            InitializeComponent();
            view = clickView;

            grMain.IsEnabled = !clickView;
            btnEdit.IsVisible = !clickView;
            btnAdd.IsVisible = !clickView;
            btnSaveUpdate.IsVisible = !clickView;

            SetModel(new PageEditTemplateContractViewModel(templateInfo, Navigation));
            btnSaveUpdate.Text = templateInfo == null ? RSC.Save : RSC.Update;

            if (templateInfo == null && !clickView)
                backNavigation.Title = RSC.CreatingContractTemplate;
            else if(clickView)
                backNavigation.Title = RSC.View;

            PModel.RequestInfo();
            backNavigation.UseBackNavigation = true;
            backNavigation.EventClickBackButton += EventClickBackButton;
        }

        protected override void OnAppearing()
        {   
            base.OnAppearing();

            if (ControlApp.SelectedEditTemplate != null)
            {
                PModel.Update(ControlApp.SelectedEditTemplate);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            EventClickBackButton();
            return true;
        } 

        private async void EventClickBackButton()
        {
            if (PModel.OldModel != null && !PModel.OldModel.Equals(PModel))
            {
                if (await DisplayAlert(RSC.ContractTemplates, RSC.WouldYouLikeToSave, RSC.Yes, RSC.No))
                {
                    PModel.SaveUpdate();
                }
                else
                {
                    await Navigation.PopAsync();
                }
            }
            else
            {
                await Navigation.PopAsync();
            }
        }

        private async void Button_Tapped(object sender, EventArgs e)
        { 
            Views.ViewEditContractButton vButton = (Views.ViewEditContractButton)sender;
            EditTemplate item = (EditTemplate)vButton.BindingContext;

            await vButton.ScaleTo(0.8, 200);

            if (item.IsVisibleAddButton)
            {
                item.IsVisibleDeleteButton = true;
                item.IsVisibleAddButton = false;
            }
            else
            {
                item.IsVisibleDeleteButton = false;
                item.IsVisibleAddButton = true; 
            }
              
            await vButton.ScaleTo(1, 200, Easing.SpringOut);
        }

        private async void EditBox_Tapped(object sender, EventArgs e)
        {
            BoxView box = (BoxView)sender;

            box.BackgroundColor = Color.Gray;
            await Task.Delay(100);

            box.BackgroundColor = Color.Transparent;
            await Task.Delay(100);

            //PModel.ShowClauseBox = false;
        }

        private async void SelectFormat_Tapped(object sender, EventArgs e)
        {
            Frame frame = sender as Frame;
            await frame.ScaleTo(0.8, 200);
            await frame.ScaleTo(1, 200, Easing.SpringOut);

            //ContractNumber.PageEditContractNumber page = new ContractNumber.PageEditContractNumber(true);
            //await Navigation.PushModalAsync(page); 
        }
         
        private void EditDone_Clicked(object sender, EventArgs e)
        {
            PModel.ContractClausesList.ForEach(item => item.IsVisibleItemClauseDelete = PModel.Editable);

            Thread thread = new Thread(new ThreadStart(ShakeItems));
            thread.IsBackground = true;
            thread.Start();
        }

        void ShakeItems()
        {
            while (PModel.Editable)
            {
                Parallel.ForEach(MyItems.Children, view =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        uint timeout = 120;
                        await view.RotateTo(2.5, timeout);
                        await view.RotateTo(-3, timeout);
                        await view.RotateTo(2.5, timeout);
                        await view.RotateTo(-3, timeout);
                        await view.RotateTo(2.5, timeout);
                        await view.RotateTo(-3, timeout);
                        await view.RotateTo(2.5, timeout);
                        await view.RotateTo(-3, timeout);
                    });
                });

                Thread.Sleep(200);
            }

            Parallel.ForEach(MyItems.Children, view =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    uint timeout = 50;
                    await view.RotateTo(0, timeout);
                });
            });
        }

        PageEditTemplateContractViewModel PModel
        {
            get
            {
                return Model as PageEditTemplateContractViewModel;
            }
        } 
    }
}