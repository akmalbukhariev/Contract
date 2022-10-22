using Contract.HttpModels;
using Contract.ViewModel.Pages.CreateContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.CanceledContracts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCancelContract : IPage
    { 
        public PageCancelContract(CreateContractInfo contractInfo)
        {
            InitializeComponent();

            SetModel(new PageCancelContractViewModel(contractInfo, Navigation));
        }

        private PageCancelContractViewModel PModel
        {
            get
            {
                return Model as PageCancelContractViewModel;
            }
        }
    }
}