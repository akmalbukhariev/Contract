using Contract.HttpModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Models
{
    public class CompanyInfoWithFile : BaseCompanyInfo
    {
        public IFormFile company_logo_url { get; set; }
    }
}
