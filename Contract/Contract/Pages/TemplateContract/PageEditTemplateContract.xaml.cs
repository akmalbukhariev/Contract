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
    public partial class PageEditTemplateContract : IPage
    {
        private PageEditTemplateContractViewModel model;
        public PageEditTemplateContract()
        {
            InitializeComponent();

            model = new PageEditTemplateContractViewModel();
            BindingContext = model; 
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            EditTemplate item1 = new EditTemplate()
            {
                Title = "1",
                Description = "Misol uchun, misol uchun, \n misol uchun, misol uchun, \n misol uchun, misol uchun, \n misol uchun"
            };
            EditTemplate item2 = new EditTemplate()
            {
                Title = "1.1",
                Description = "Misol uchun, misol uchun, \n misol uchun, misol uchun, \n misol uchun, misol uchun, \n misol uchun"
            };
            EditTemplate item3 = new EditTemplate()
            {
                ButtonText = RSC.Info4,
                ButtonDeleteText = RSC.Info6,
                IsVisibleItemClause = false,
                IsVisibleButton = true,
                IsVisibleAddButton = true,
                IsVisibleAddContractInfoButton = true
            };
            EditTemplate item4 = new EditTemplate()
            {
                ButtonText = RSC.AddClause,
                IsThisAddClauseButton = true,
                IsVisibleItemClause = false,
                IsVisibleButton = true,
                IsVisibleAddButton = true,
                IsVisibleAddClauseButton = true
            };
            EditTemplate item5 = new EditTemplate()
            {
                Title = "2",
                Description = "Misol uchun, misol uchun, \n misol uchun, misol uchun, \n misol uchun, misol uchun, \n misol uchun"
            };
            EditTemplate item6 = new EditTemplate()
            {
                ButtonText = RSC.Info5,
                ButtonDeleteText = RSC.Info7,
                IsVisibleItemClause = false,
                IsVisibleButton = true,
                IsVisibleAddButton = true,
                IsVisibleAddDetailOfNegotiatorButton = true
            };

            model.DataList.Add(item1);
            model.DataList.Add(item2);
            model.DataList.Add(item3);
            model.DataList.Add(item4);
            model.DataList.Add(item5);
            model.DataList.Add(item6);
        }

        private void DragGestureRecognizer_DragStarting_Collection(System.Object sender, Xamarin.Forms.DragStartingEventArgs e)
        {

        }

        private void DropGestureRecognizer_Drop_Collection(System.Object sender, Xamarin.Forms.DropEventArgs e)
        {
            // We handle reordering login in our view model
            e.Handled = true;
        }

        private void MyItems_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MyItems.SelectedItem = null;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {   
            Views.ViewEditContractButton vButton = (Views.ViewEditContractButton)sender;
            EditTemplate item = (EditTemplate)vButton.BindingContext;

            await vButton.ScaleTo(0.8, 200);
            if (item.IsVisibleAddButton)
            {
                if (!item.IsThisAddClauseButton)
                {
                    item.IsVisibleDeleteButton = true;
                    item.IsVisibleAddButton = false;
                }
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

            model.ShowClauseBox = false;
        } 
    }
}