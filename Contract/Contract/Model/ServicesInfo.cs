using Contract.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Model
{
    public class ServicesInfo : BaseModel
    {
        public string NameOfService { get => GetValue<string>(); set => SetValue(value); }
        public int UnitOfMeasureIndex { get => GetValue<int>(); set => SetValue(value); }
        public string AmountText { get => GetValue<string>(); set => SetValue(value); }
        public string PriceText { get => GetValue<string>(); set => SetValue(value); }

        public ServicesInfo()
        {
            
        }

        public ServicesInfo(ServicesInfo other)
        {
            Copy(other);
        }

        public void Copy(ServicesInfo other)
        {
            this.NameOfService = other.NameOfService;
            this.UnitOfMeasureIndex = other.UnitOfMeasureIndex;
            this.AmountText = other.AmountText;
            this.PriceText = other.PriceText;
        }
    }
}
