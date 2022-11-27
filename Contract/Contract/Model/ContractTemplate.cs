using Contract.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Contract.Model
{
    public class ContractTemplate : BaseModel
    {
        public string No { get => GetValue<string>(); set => SetValue(value); }
        public string ContractTempName { get => GetValue<string>(); set => SetValue(value); }
        public string ContractPurpose { get => GetValue<string>(); set => SetValue(value); } 
        public Color ItemColor { get => GetValue<Color>(); set => SetValue(value); }
        public HttpModels.ContractTemplate TemplateInfo { get; set; }
    }
}
