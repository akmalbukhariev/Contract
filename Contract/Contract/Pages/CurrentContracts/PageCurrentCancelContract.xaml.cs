using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.CurrentContracts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCurrentCancelContract : IPage
    {
        public PageCurrentCancelContract()
        {
            InitializeComponent();
        }
    }
}