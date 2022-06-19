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
    public partial class PageMessage : IPage
    {
        public PageMessage()
        {
            InitializeComponent();
        }
    }
}