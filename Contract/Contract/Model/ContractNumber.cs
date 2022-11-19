﻿using Contract.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Contract.Model
{
    public class ContractNumber : BaseModel
    {
        public string No { get => GetValue<string>(); set => SetValue(value); }
        public string ContractNumberText { get => GetValue<string>(); set => SetValue(value); } 
        public Color ItemColor { get => GetValue<Color>(); set => SetValue(value); }
        public int Format { get; set; } = 1;
        public string CreatedDate { get; set; }
        public int IsDeleted { get; set; } = 0;
    }
}
