using ContractAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Response
{
    public class ResponseApplicableContract : Response
    {
        public List<ApplicableContract> data { get; set; } = new List<ApplicableContract>();
    }
}
