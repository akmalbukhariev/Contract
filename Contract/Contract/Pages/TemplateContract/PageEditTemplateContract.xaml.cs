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
            model.IsBeingDraggedOver = false;

            TestDragAndDrop item1 = new TestDragAndDrop()
            {
                image = "Add",
                text = "add image"
            };
            TestDragAndDrop item2 = new TestDragAndDrop()
            {
                image = "cancel",
                text = "cancel image"
            };
            TestDragAndDrop item3 = new TestDragAndDrop()
            {
                image = "check",
                text = "check image"
            };
            TestDragAndDrop item4 = new TestDragAndDrop()
            {
                image = "code",
                text = "code image"
            };
            TestDragAndDrop item5 = new TestDragAndDrop()
            {
                image = "edit",
                text = "edit image"
            };

            model.DataList.Add(item1);
            model.DataList.Add(item2);
            model.DataList.Add(item3);
            model.DataList.Add(item4);
            model.DataList.Add(item5);
        }

        private void DragGestureRecognizer_DragStarting_Collection(System.Object sender, Xamarin.Forms.DragStartingEventArgs e)
        {

        }

        private void DropGestureRecognizer_Drop_Collection(System.Object sender, Xamarin.Forms.DropEventArgs e)
        {
            // We handle reordering login in our view model
            e.Handled = true;
        }
    }
}