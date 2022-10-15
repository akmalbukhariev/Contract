using Contract.HttpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contract.HttpResponse
{
    public class ResponseClientCompanyInfo : Response, IResponse
    {
        public List<CompanyInfo> data { get; set; } = new List<CompanyInfo>();
    }
}
