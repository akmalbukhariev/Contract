using ContractAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Response
{
    public class ResponseContractInfo : Response
    {
        public List<ContractInfo> data { get; set; } = new List<ContractInfo>();
    }
}
