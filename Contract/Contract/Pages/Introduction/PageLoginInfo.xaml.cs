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

            lbText1.Text = RSC.LoginInfo1_1;
            lbText2.Text = RSC.LoginInfo1_2;
        }

        private void Enter_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView<Grid>((Grid)sender);
        }
    }
}