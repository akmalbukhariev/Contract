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
        private bool yes1 = true;
        
        public PageCreateContract2(HttpModels.CompanyInfo companyInfo)
        {
            InitializeComponent();

            SetModel(new PageCreateContract2ViewModel(Navigation, companyInfo));
            YesNo1_Tapped(null, null);

            ControlApp.EventCurrencyCostChanged += UpdateTotalCost;
            entContractNumber.Entry.IsEnabled = false;
        }
         
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Model.Parent = Parent;

            PModel.RequestContractNumber();

            lbStep.Text = RSC.Step + " #2";
            PModel.CurrencyList = GetCurrentList;
            PModel.QQSList = GetQQSList;

            if (PModel.CurrencyList.Count > 0)
                PModel.SelectedCurrency = PModel.CurrencyList[0];

            if (PModel.QQSList.Count > 0)
                PModel.SelectedQQS = PModel.QQSList[0];
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
            UpdateTotalCost();
        }

        private void Plus_Clicked(object sender, EventArgs e)
        { 
            ServicesInfo item = (ServicesInfo)((Button)sender).BindingContext;
            if (item == null) return;

            item.AmountText = (int.Parse(item.AmountText) + 1).ToString();
            UpdateTotalCost();
        }
         
        private void AddCopy_Stack_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((StackLayout)sender);

            ServicesInfo item = (ServicesInfo)((StackLayout)sender).BindingContext;
            if (item == null) return;

            ServicesInfo service = new ServicesInfo(item);
            PModel.AddService(service);
            UpdateTotalCost();
        }
         
        private void AddEmpty_Stack_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((StackLayout)sender);

            ServicesInfo item = (ServicesInfo)((StackLayout)sender).BindingContext;
            if (item == null) return;

            ServicesInfo service = new ServicesInfo();
            service.Index = item.Index + 1;
            PModel.AddService(service);
            UpdateTotalCost();
        }
          
        private void Delete_Stack_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((StackLayout)sender);

            if (PModel.ServicesList.Count == 1) return;

            ServicesInfo item = (ServicesInfo)((StackLayout)sender).BindingContext;
            if (item == null) return;

            PModel.RemoveService(item);
            UpdateTotalCost();
        }

        private void UpdateTotalCost()
        {
            int cost = 0;
            foreach (ServicesInfo item in PModel.ServicesList)
            {
                cost += item.CalcTotalCost();
            }

            PModel.TotalCostText = $"{cost.ToString()} {PModel.SelectedCurrency}";
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
            PModel.UpdateServiceList();
            UpdateTotalCost();
        }

        private void Template_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}