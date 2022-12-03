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
        public LibContract.HttpModels.ContractTemplate TemplateInfo { get; set; }

        public ContractTemplate()
        {
            
        }

        public ContractTemplate(LibContract.HttpModels.ContractTemplate other)
        {
            ContractTempName = other.template_name;
            TemplateInfo = new LibContract.HttpModels.ContractTemplate(other);
        }

        public ContractTemplate(ContractTemplate other)
        {
            Copy(other);
        }

        public void Copy(ContractTemplate other)
        {
            No = other.No;
            ContractTempName = other.ContractTempName;
            ContractPurpose = other.ContractPurpose;
            ItemColor = other.ItemColor;
            TemplateInfo = new LibContract.HttpModels.ContractTemplate(other.TemplateInfo);
        }
    }
}
