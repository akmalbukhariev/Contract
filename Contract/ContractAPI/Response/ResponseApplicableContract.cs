using ContractAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Response
{
    public class ResponseApplicableContract : Response
    {
        public ApplicableContract data { get; set; } = new ApplicableContract();
    }
}
