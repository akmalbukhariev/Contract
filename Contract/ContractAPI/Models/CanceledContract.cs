using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Models
{
    public class CanceledContract : ContractTableInfo
    {
        public string comment { get; set; }
        public string payment_percent { get; set; }
        public CanceledContract()
        {
            
        }

        public CanceledContract(CanceledContract other)
        {
            this.Copy(other);
        }

        public void Copy(CanceledContract other)
        {
            base.Copy(other);
            this.comment = other.comment;
            this.payment_percent = other.payment_percent;
        }
    }
}
