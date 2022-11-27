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
    public partial class PageClausesChild : IPage
    {
        private EditTemplate OldItem;
        public PageClausesChild(EditTemplate selectedItem)
        {
            InitializeComponent();

            SetModel(new PageClausesChildViewModel(selectedItem, Navigation));
            OldItem = new EditTemplate(selectedItem);

            backNavigation.UseBackNavigation = true;
            backNavigation.EventClickBackButton += EventClickBackButton;
        }
         
        protected override void OnAppearing()
        {
            base.OnAppearing();

            ControlApp.SelectedEditTemplate = null;
        }
         
        protected override bool OnBackButtonPressed()
        { 
            EventClickBackButton();
            return true;
        }

        private async void EventClickBackButton()
        {
            EditTemplate editedItem = PModel.GetSaveChanges();
            if (!OldItem.Equals(editedItem))
            {
                editedItem.Title = OldItem.Title;
                if (await DisplayAlert(RSC.ContractTemplates, RSC.WouldYouLikeToSave, RSC.Yes, RSC.No))
                {
                    ControlApp.SelectedEditTemplate = editedItem;//new EditTemplate(editedItem);
                }
            }

            await Navigation.PopAsync();
        }

        private void EditDone_Clicked(object sender, EventArgs e)
        {
            PModel.DataList.ForEach(item => item.IsVisibleItemClauseDelete = PModel.Editable);

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
          
        PageClausesChildViewModel PModel
        {
            get
            {
                return Model as PageClausesChildViewModel;
            }
        }
    }
}