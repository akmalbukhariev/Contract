using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Contract.Model
{
    public class CurrentContract : BaseContract
    {
        public string PricePercent { get => GetValue<string>(); set => SetValue(value); }
        public Color PricePercentColor { get => GetValue<Color>(); set => SetValue(value); }
    }
}
