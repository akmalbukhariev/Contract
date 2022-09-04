using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Models
{
    public class CanceledContract : ContractTableInfo
    {
        public string comment { get; set; }
        public void Copy(CanceledContract other)
        {
            base.Copy(other);
            this.comment = other.comment;
        }
    }
}
