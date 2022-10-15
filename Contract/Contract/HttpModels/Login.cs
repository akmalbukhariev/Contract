using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contract.HttpModels
{
    public class Login
    {
        public string phone_number { get; set; }
        public string password { get; set; }
    }
}
