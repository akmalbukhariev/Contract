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
    public partial class PageUnapprovedCancelContract : IPage
    {
        public PageUnapprovedCancelContract(Net.CanceledContract canceledContract)
        {
            InitializeComponent();

            SetModel(new PageUnapprovedCancelContractViewModel(canceledContract, Navigation));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private PageUnapprovedCancelContractViewModel PModel
        {
            get
            {
                return Model as PageUnapprovedCancelContractViewModel;
            }
        }
    }
}