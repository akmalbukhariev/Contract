using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.CreateContract
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCreateContract1 : IPage
    {
        public PageCreateContract1()
        {
            InitializeComponent();

            lbYesNo.Text = "Mijozning (kontragent) \n rekvizitlari ochiq qolsinmi?";
        }
    }
}