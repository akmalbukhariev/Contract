using Contract.Model;
using Contract.ViewModel.Pages.TemplateContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.TemplateContract
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageClausesChild : IPage
    {
        public PageClausesChild(EditTemplate selectedItem)
        {
            InitializeComponent();

            SetModel(new PageClausesChildViewModel(selectedItem));
        }

        private void EditDone_Clicked(object sender, EventArgs e)
        {
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
                        uint timeout = 50;
                        await view.RotateTo(2.5, timeout);
                        await view.RotateTo(0, timeout);
                        await view.RotateTo(2.5, timeout);
                        await view.RotateTo(0, timeout);
                        await view.RotateTo(2.5, timeout);
                        await view.RotateTo(0, timeout);
                        await view.RotateTo(2.5, timeout);
                        await view.RotateTo(0, timeout);
                    });
                });

                Thread.Sleep(300);
            }

            Parallel.ForEach(MyItems.Children, async view =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    uint timeout = 50;
                    await view.RotateTo(0, timeout);
                });
            });
        }

        private void Add_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);

            EditTemplate newItem = new EditTemplate();
            newItem.Title = $"{PModel.DataList[0].Title}.{PModel.DataList.Count}";
            PModel.DataList.Add(newItem);
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