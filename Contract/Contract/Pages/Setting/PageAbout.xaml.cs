using LibContract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.Setting
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageAbout : IPage
    {
        public PageAbout()
        {
            InitializeComponent();
            lbVersion.Text = ControlApp.AppVersion;
        } 

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!ControlApp.InternetOk()) return;

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseAboutApp response = await Net.HttpService.GetAboutApp(AppSettings.GetLanguage());
            ControlApp.CloseLoadingView();

            if (response.result)
            {
                var source = new HtmlWebViewSource();
                var text = "<html>" +
                            "<body  style=\"text-align: justify; line-height: 1.6;\">" +
                            string.Format("<p style=\"text-indent: 1em;\">{0}</p>", response.data.text) +
                            "</body>" +
                            "</html>";
                source.Html = text;
                webInfo.Source = source;
            }
        }
    }
}