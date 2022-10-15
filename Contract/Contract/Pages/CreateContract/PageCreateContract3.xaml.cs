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
    public partial class PageCreateContract3 : IPage
    {
        public PageCreateContract3(string contractNumber, string price)
        {
            InitializeComponent();

            lbContractNumber.Text = contractNumber;
            lbContractPrice.Text = price;
        }

        private void View_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView(boxView);
            ClickAnimationView(stackView);
        }

        private void Send_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView(boxSend);
            ClickAnimationView(stackSend);
        }

        private void Cancel_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView(boxCancel);
            ClickAnimationView(stackCancel);
        }
    }
}