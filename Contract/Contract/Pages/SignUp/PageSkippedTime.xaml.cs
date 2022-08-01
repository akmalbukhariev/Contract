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
    public partial class PageSkippedTime : IPage
    {
        public PageSkippedTime()
        {
            InitializeComponent();
            navigationBar.Title = RSC.SignUp;
            btnNext.Text = RSC.ButtonNext;
        }
    }
}