using Contract.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Model
{
    public class Customer : BaseModel
    {
        public string UserImage { get => GetValue<string>(); set => SetValue(value); }
        public string UserTitle { get => GetValue<string>(); set => SetValue(value); }
        public string UserStir { get => GetValue<string>(); set => SetValue(value); }

        public Customer()
        {
            
        }
    }
}
