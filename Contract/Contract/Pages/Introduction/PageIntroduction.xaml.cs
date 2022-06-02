using Contract.ViewModel.SignUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.Introduction
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageIntroduction : IPage
    {
        private PageIntroductionViewModel model;
        public PageIntroduction()
        {
            InitializeComponent();
            model = new PageIntroductionViewModel(Navigation);

            BindingContext = model;
        }

        private async void LabelSkip_Tapped(object sender, EventArgs e)
        {
            ChangeClickBackColor((Label)sender);
            await Navigation.PushAsync(new PageLoginInfo());
        }
    }
}