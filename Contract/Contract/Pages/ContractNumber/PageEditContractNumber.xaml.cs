using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.ContractNumber
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageEditContractNumber : IPage
    {
        public PageEditContractNumber()
        {
            InitializeComponent();
        }

        void OnAnimalRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton button = sender as RadioButton; 
        }
    }
}