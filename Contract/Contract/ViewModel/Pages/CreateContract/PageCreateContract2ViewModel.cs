using Contract.Model;
using Contract.Pages.CreateContract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contract.ViewModel.Pages.CreateContract
{
    public class PageCreateContract2ViewModel : BaseModel
    {
        public int ServiceTypeIndex { get => GetValue<int>(); set => SetValue(value); }
        public string ContractNumber { get => GetValue<string>(); set => SetValue(value); }
        public int ContractCurrencyIndex { get => GetValue<int>(); set => SetValue(value); }
        public int AmountOfVatIndex { get => GetValue<int>(); set => SetValue(value); }
        public bool IsExeciseTax { get => GetValue<bool>(); set => SetValue(value); }
        public string InterestText { get => GetValue<string>(); set => SetValue(value); }
        //public string NameOfService { get => GetValue<string>(); set => SetValue(value); }
        //public int UnitOfMeasureIndex { get => GetValue<int>(); set => SetValue(value); }
        //public string AmountText { get => GetValue<string>(); set => SetValue(value); }
        //public string PriceText { get => GetValue<string>(); set => SetValue(value); }
        public string TotalCostText { get => GetValue<string>(); set => SetValue(value); }
        public bool Agree { get => GetValue<bool>(); set => SetValue(value); }

        public ObservableCollection<ServicesInfo> ServicesList { get; set; }

        public PageCreateContract2ViewModel(INavigation navigation) : base(navigation)
        {
            this.ServicesList = new ObservableCollection<ServicesInfo>();
            InitServiceList();

            TotalCostText = "123,000 sum";
        }

        #region Command
        public ICommand CommandFinished => new Command(Finished);

        private async void Finished()
        {
            SetTransitionType();
            await Navigation.PushAsync(new PageCreateContract3());
        }
        #endregion

        public void InitServiceList()
        {
            ServicesInfo service = new ServicesInfo()
            {
                NameOfService = "",
                UnitOfMeasureIndex = 0,
                AmountText = "1",
                PriceText = ""
            };

            AddService(service);
        }

        public void AddService(ServicesInfo service)
        {
            this.ServicesList.Add(service);
        }
    }
}
