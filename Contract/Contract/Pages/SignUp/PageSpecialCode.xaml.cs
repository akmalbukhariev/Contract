using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.SignUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSpecialCode : IPage
    {
        public PageSpecialCode()
        {
            InitializeComponent();
        }

        private void SendCodeaAgain_Tapped(object sender, EventArgs e)
        {
            ChangeClickBackColor(lbSendCodeAgain,  Color.White, (Color)App.Current.Resources["AppColor"]);
        }
    }
}