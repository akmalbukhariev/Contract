using Contract.HttpModels;
using Contract.ViewModel.Pages.CurrentContracts;
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
        public PageCurrentCancelContract(CanceledContract canceledContract)
        {
            InitializeComponent();

            SetModel(new PageCurrentCancelContractViewModel(canceledContract, Navigation));
        }

        private PageCurrentCancelContractViewModel PModel
        {
            get
            {
                return Model as PageCurrentCancelContractViewModel;
            }
        }
    }
}