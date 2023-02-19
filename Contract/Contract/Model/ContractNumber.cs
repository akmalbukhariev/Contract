using Contract.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Contract.Model
{
    public class ContractNumber : BaseModel
    {
        public int Id { get; set; }
        public string No { get => GetValue<string>(); set => SetValue(value); }
        public string ContractNumberText { get => GetValue<string>(); set => SetValue(value); } 
        public Color ItemColor { get => GetValue<Color>(); set => SetValue(value); }
        public int Format { get; set; } = 1;
        public string CreatedDate { get; set; }
        public string CancelImage { get; set; } = "cancel.png";
        public bool IsEnabled { get; set; } = true;

        public ContractNumber()
        {
            
        }

        public ContractNumber(ContractNumber other)
        {
            Copy(other);
        }

        public void Copy(ContractNumber other)
        {
            Id = other.Id;
            No = other.No;
            ContractNumberText = other.ContractNumberText;
            ItemColor = other.ItemColor;
            Format = other.Format;
            CreatedDate = other.CreatedDate;
            CancelImage = other.CancelImage;
            IsEnabled = other.IsEnabled;
        }
    }
}
