using Contract.ViewModel.Pages.UnapprovedContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.UnapprovedContracts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageTable : IPage
    {
        private PageTableViewModel model;
        public PageTable()
        {
            InitializeComponent();

            model = new PageTableViewModel();
            BindingContext = model;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            model.Init();
        }
    }
}