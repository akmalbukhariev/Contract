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
        public PageEditTemplateContract()
        {
            InitializeComponent();

            SetModel(new PageEditTemplateContractViewModel(Navigation)); 
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            PModel.DataList.Clear();

            EditTemplate item1 = new EditTemplate()
            {
                Title = "1",
                Description = "1  Misol uchun, misol uchun, \n misol uchun, misol uchun, \n misol uchun, misol uchun, \n misol uchun",
            };
            item1.Child.Add(new EditTemplate()
            {
                Title = "1.1",
                Description = "1.1  DDDDDD XXXXXXXXXXXXXXXXXXXXAAAAAAAAAAAAA SSSSSSSSSA \n ZZZZZZZZZZZZZZ"
            });
            item1.Child.Add(new EditTemplate()
            {
                Title = "1.2",
                Description = "1.2  DDDDDD XXXXXXXXXXXXXXXXXXXXAAAAAAAAAAAAA SSSSSSSSSA \n ZZZZZZZZZZZZZZ"
            });

            EditTemplate item2 = new EditTemplate()
            {
                Title = "3",
                Description = "3 Misol uchun, misol uchun, \n misol uchun, misol uchun, \n misol uchun, misol uchun, \n misol uchun"
            };
            item2.Child.Add(new EditTemplate()
            {
                Title = "3.1",
                Description = "3.1 DDDDDD XXXXXXXXXXXXXXXXXXXXAAAAAAAAAAAAA SSSSSSSSSA \n ZZZZZZZZZZZZZZ"
            });
            item2.Child.Add(new EditTemplate()
            {
                Title = "3.2",
                Description = "3.2 ASXSXSX XMMZNBHXBHVX SSSSSSSSSA \n ZZZZZZZZZZZZZZ"
            });
            item2.Child.Add(new EditTemplate()
            {
                Title = "3.3",
                Description = "3.3 DDDDDD WWWWWWW52225c5dc2 555 \n SSSSSSSSSA \n ZZZZZZZZZZZZZZ"
            });
            item2.Child.Add(new EditTemplate()
            {
                Title = "3.4",
                Description = "3.4 EEEEEEEEDD AAAAAXXXX 555 \n SSSSSSSSSA \n ZZZZZZZZZZZZZZ"
            });
            item2.Child.Add(new EditTemplate()
            {
                Title = "3.5",
                Description = "3.5 XXXX FFFFFFFFFFFFFFFF 555 \n PPPPP \n WWWWWW"
            });

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
                ButtonColor = Color.FromHex("#2DACC3"),
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

            PModel.DataList.Add(item1);
            PModel.DataList.Add(item3);
            //PModel.DataList.Add(item4);
            PModel.DataList.Add(item5);
            PModel.DataList.Add(item6);
            PModel.DataList.Add(item2);
        }

        private void MyItems_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //MyItems.SelectedItem = null;
        }

        private async void Button_Tapped(object sender, EventArgs e)
        { 
            Views.ViewEditContractButton vButton = (Views.ViewEditContractButton)sender;
            EditTemplate item = (EditTemplate)vButton.BindingContext;

            await vButton.ScaleTo(0.8, 200);
            if (item.IsVisibleAddButton)
            {
                if (item.IsThisAddClauseButton)
                {
                    PModel.ShowClauseBox = true;
                }
                else
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

            PModel.ShowClauseBox = false;
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
            PModel.DataList.ForEach(item => item.IsVisibleDelete = PModel.Editable);

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