using Contract.Model;
using Contract.ViewModel.Pages.CreateContract;
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
    public partial class PageCreateContract2 : IPage
    {
        private bool yes1 = false;
        
        public PageCreateContract2(Net.CompanyInfo companyInfo)
        {
            InitializeComponent();

            SetModel(new PageCreateContract2ViewModel(Navigation, companyInfo));
            YesNo1_Tapped(null, null); 
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Model.Parent = Parent;

            lbStep.Text = RSC.Step + " #2";
            PModel.CurrencyList = GetCurrentList;
            PModel.QQSList = GetQQSList;
        }

        private void YesNo1_Tapped(object sender, EventArgs e)
        {
            if (yes1)
            {
                imYesNo1.Source = GetYesNoIcon(false);
                yes1 = false;
                entTax.IsVisible = false;
            }
            else
            {
                imYesNo1.Source = GetYesNoIcon(true);
                yes1 = true;
                entTax.IsVisible = true;
            }

            if (sender != null)
                ControlApp.Vibrate();
        }
         
        private void Minus_Clicked(object sender, EventArgs e)
        {  
            ServicesInfo item = (ServicesInfo)((Button)sender).BindingContext;
            if (item == null) return;

            if (item.AmountText == "0") return;

            item.AmountText = (int.Parse(item.AmountText) - 1).ToString();
        }

        private void Plus_Clicked(object sender, EventArgs e)
        { 
            ServicesInfo item = (ServicesInfo)((Button)sender).BindingContext;
            if (item == null) return;

            item.AmountText = (int.Parse(item.AmountText) + 1).ToString();
        }
         
        private void AddCopy_Stack_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((StackLayout)sender);

            ServicesInfo item = (ServicesInfo)((StackLayout)sender).BindingContext;
            if (item == null) return;

            ServicesInfo service = new ServicesInfo(item);
            PModel.AddService(service);
        }
         
        private void AddEmpty_Stack_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((StackLayout)sender);

            ServicesInfo item = (ServicesInfo)((StackLayout)sender).BindingContext;
            if (item == null) return;

            ServicesInfo service = new ServicesInfo();
            service.Index = item.Index + 1;
            PModel.AddService(service);
        }
          
        private void Delete_Stack_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((StackLayout)sender);

            if (PModel.ServicesList.Count == 1) return;

            ServicesInfo item = (ServicesInfo)((StackLayout)sender).BindingContext;
            if (item == null) return;

            PModel.RemoveService(item);
        }

        async void ChangeBoxColor(BoxView boxView)
        {
            boxView.BackgroundColor = Color.White;
            await Task.Delay(100);

            boxView.BackgroundColor = Color.FromHex("#E6E6E6");
            await Task.Delay(200);
        } 

        private PageCreateContract2ViewModel PModel
        {
            get
            {
                return Model as PageCreateContract2ViewModel;
            }
        }

        private void BoxMinus_Tapped(object sender, EventArgs e)
        {
            ChangeBoxColor((BoxView)sender);
        }

        private void BoxPlus_Tapped(object sender, EventArgs e)
        {
            ChangeBoxColor((BoxView)sender);
        }

        private void Currency_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}