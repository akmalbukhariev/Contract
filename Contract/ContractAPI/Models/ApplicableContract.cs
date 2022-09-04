using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Models
{
    public class ApplicableContract : ContractTableInfo
    {
        public string payment_percent { get; set; }

        public void Copy(ApplicableContract other)
        {
            base.Copy(other);
            this.payment_percent = other.payment_percent;
        }
    }
}
