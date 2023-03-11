using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageFindPassword : IPage
    {
        public PageFindPassword()
        {
            InitializeComponent();
            navigationBar.Title = RSC.FindPassword;
        }

        private async void Finish_Clicked(object sender, EventArgs e)
        {

        }
    }
}