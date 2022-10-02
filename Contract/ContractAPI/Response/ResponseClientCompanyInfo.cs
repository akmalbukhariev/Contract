using ContractAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Response
{
    public class ResponseClientCompanyInfo : Response
    {
        public List<CompanyInfo> data { get; set; } = new List<CompanyInfo>();
    }
}
