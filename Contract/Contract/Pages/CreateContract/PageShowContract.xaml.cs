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
    public partial class PageShowContract : IPage
    {
        public PageShowContract(string contractUrl)
        {
            InitializeComponent();

            webView.Source = contractUrl;
             
            //webView.Source = $"https://drive.google.com/viewerng/viewer?embedded=true&url={contractUrl}";
            //http://192.168.219.102:5000//Upload//images/2.jpg

            //webView.Uri = "https://icseindia.org/document/sample.pdf";
            //webView.Source = "https://drive.google.com/viewerng/viewer?embedded=true&url=https://icseindia.org/document/sample.pdf";
            //webView.Source = "https://drive.google.com/viewerng/viewer?embedded=true&url=https://192.168.219.102:5000//Upload//contracts_pdf//12_00002_Kukmin.pdf";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
             
        }
    }
}