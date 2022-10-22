using Contract.HttpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contract.HttpResponse
{
    public class ResponseApprovedUnapprovedContract : Response, IResponse
    {
        public List<CreateContractInfo> data { get; set; } = new List<CreateContractInfo>();
    }
}
