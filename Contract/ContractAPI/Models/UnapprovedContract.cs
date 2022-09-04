using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Models
{
    public class UnapprovedContract : ContractTableInfo
    { 
        public void Copy(UnapprovedContract other)
        {
            base.Copy(other);       
        }
    }
}
