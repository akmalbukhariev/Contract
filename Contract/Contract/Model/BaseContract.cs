using Contract.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Contract.Model
{
    public class BaseContract : BaseModel
    {
        public string No { get => GetValue<string>(); set => SetValue(value); }
        public string Preparer { get => GetValue<string>(); set => SetValue(value); }
        public string ContractNnumber { get => GetValue<string>(); set => SetValue(value); }
        public string ContractNnumberReal { get => GetValue<string>(); set => SetValue(value); }
        public string CompanyName { get => GetValue<string>(); set => SetValue(value); }
        public string ContractDate { get => GetValue<string>(); set => SetValue(value); }
        public string ContractPrice { get => GetValue<string>(); set => SetValue(value); }
        public Color ItemColor { get => GetValue<Color>(); set => SetValue(value); }
        public Color PreparerColor { get => GetValue<Color>(); set => SetValue(value); }
    }
}
