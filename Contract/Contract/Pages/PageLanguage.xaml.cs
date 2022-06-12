using Contract.Views;
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
    public partial class PageLanguage : IPage
    {
        public PageLanguage()
        {
            InitializeComponent();
        }

        private void Item_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((ViewLanguage)sender); 
        }
    }
}