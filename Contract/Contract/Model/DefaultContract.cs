using Contract.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Model
{
    public class DefaultContract : BaseModel
    {
        public int id { get; set; }
        public bool Selected { get => GetValue<bool>(); set => SetValue(value); }
        public string Name { get => GetValue<string>(); set => SetValue(value); }
        public string Url { get => GetValue<string>(); set => SetValue(value); }

        public DefaultContract(DefaultContract other)
        {
            Copy(other);
        }

        public DefaultContract()
        {
            Selected = false;
        }

        public void Copy(DefaultContract other)
        {
            id = other.id;
            Selected = other.Selected;
            Name = other.Name;
            Url = other.Url;
        }
    }
}
