using ContractAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Response
{
    public class ResponseUnapprovedContract : Response
    {
        public List<UnapprovedContract> data { get; set; } = new List<UnapprovedContract>();
    }
}
