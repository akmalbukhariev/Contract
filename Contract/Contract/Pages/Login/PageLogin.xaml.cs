using Contract.ViewModel.Pages.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageLogin : IPage
    {
        public PageLogin()
        {
            InitializeComponent();
            SetModel(new PageLoginViewModel(Navigation));

            entId.Entry.ClearButtonVisibility = ClearButtonVisibility.WhileEditing;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Model.Parent = Parent;
        }
        private void AutoLogin_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

        }

        private void ClickFindId(object sender, EventArgs e)
        {
            ChangeClickBackColor(lbFindId, Color.White, Color.FromHex("#3F6C6C"));
        }

        private void ClickFindPassword(object sender, EventArgs e)
        {
            ChangeClickBackColor(lbFindPassword, Color.White, Color.FromHex("#3F6C6C"));
        }
    }   
}