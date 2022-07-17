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
            Init();
        }

        private void Init()
        {
            lbVersion.Text = ControlApp.AppVersion;

            string info = "Ushbu dasturiy ta'minot ikki tomon \n kelishuvchilarini rasmiylashtirish uchun \n shartnomalar tuzish" +
                          "yo'li bilan xizmat qiladi. \n Platforma foydalanuvchilar uchun qulay \n  qilib, ularning takliflari asosida \n tuzilgan.";
            var source = new HtmlWebViewSource();
            var text = "<html>" +
                        "<body  style=\"text-align: justify; line-height: 1.6;\">" +
                        string.Format("<p style=\"text-indent: 1em;\">{0}</p>", info) +
                        "</body>" +
                        "</html>";
            source.Html = text;
            webInfo.Source = source;
        }
    }
}