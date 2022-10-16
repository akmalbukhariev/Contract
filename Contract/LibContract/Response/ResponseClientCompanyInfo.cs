using LibContract.HttpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibContract.Response
{
    public class ResponseClientCompanyInfo : Response
    {
        public List<CompanyInfo> data { get; set; } = new List<CompanyInfo>();
    }
}
