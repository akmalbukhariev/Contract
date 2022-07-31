using Contract.Model;
using Contract.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMain : IPage
    {  
        public PageMain()
        {
            InitializeComponent();

            SetModel(new PageMainViewModel());
            (Model as PageMainViewModel).Init();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Model.Parent = Parent;

            lbMyCompany.Text = RSC.MyCompany;
            lbIdNumber.Text = RSC.IDNumber;
            btnCreateContract.Text = RSC.CreateContract;
            lbAllContracts.Text = RSC.AllContracts;

            (Model as PageMainViewModel).Clean();
            (Model as PageMainViewModel).Init();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = sender as ListView;
            ChildMenuItem item = listView.SelectedItem as ChildMenuItem;
             
            switch (item.ID)
            {
                case Constant.Menu1:
                    OnNavigatePage(new UnapprovedContracts.PageTable()); 
                    break;
                case Constant.Menu2:
                    OnNavigatePage(new CurrentContracts.PageCurrentContractTable()); 
                    break;
                case Constant.Menu3:
                    OnNavigatePage(new CanceledContracts.PageCanceledTable()); 
                    break;
            }

            listView.SelectedItem = null;
        }

        private void ShowMenu_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
            OnShowMenu(true);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            OnNavigatePage(new CreateContract.PageCreateContract1()); 
        }
    }
}