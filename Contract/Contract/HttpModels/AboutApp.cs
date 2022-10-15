using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contract.HttpModels
{
    public class AboutApp
    {
        public string lan_code { get; set; }
        public string text { get; set; }

        public void Copy(AboutApp other)
        {
            this.lan_code = other.lan_code;
            this.text = other.text;
        }
    }
}
