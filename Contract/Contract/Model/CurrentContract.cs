using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Contract.Model
{
    public class ApprovedContract : BaseContract
    {
        public string ContractPayment { get => GetValue<string>(); set => SetValue(value); }
        public Color ContractPaymentColor { get => GetValue<Color>(); set => SetValue(value); }
    }
}
