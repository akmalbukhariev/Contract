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
    public partial class PageLoginInfo : IPage
    {
        public PageLoginInfo()
        {
            InitializeComponent();

            lbText1.Text = "FOYDA VA ZARARLARNI \n TAHLIL QILISH";
            lbText2.Text = "Barcha bajarilgan amalyotlar bo'yicha \n kirim-chimlarni foyda va zararlarga \n ajrating";
        }

        private void Enter_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView<Grid>((Grid)sender);
        }
    }
}