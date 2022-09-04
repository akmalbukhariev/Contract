using ContractAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Response
{
    public class ResponseUserCompanyInfo : Response
    {
        public UserCompanyInfo data { get; set; } = new UserCompanyInfo();
    }
}
