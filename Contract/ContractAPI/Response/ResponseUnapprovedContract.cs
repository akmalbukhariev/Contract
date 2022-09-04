using ContractAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Response
{
    public class ResponseUnapprovedContract : Response
    {
        public UnapprovedContract data { get; set; } = new UnapprovedContract();
    }
}
