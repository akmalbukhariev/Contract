using Contract.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.Introduction
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageLoginInfo : IPage
    {
        public PageLoginInfo()
        {
            InitializeComponent();

            SetModel(new ViewModel.BaseModel(Navigation));

            lbText1.Text = RSC.LoginInfo1_1;
            lbText2.Text = RSC.LoginInfo1_2;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Model.Parent = Parent;
        }

        private void Enter_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Grid)sender);
            
            Application.Current.MainPage = new TransitionNavigationPage(new Login.PageLogin());
            //await this.ScaleTo(0.5, 1000);
            //await this.FadeTo(1, 2000);
        }
    }
}