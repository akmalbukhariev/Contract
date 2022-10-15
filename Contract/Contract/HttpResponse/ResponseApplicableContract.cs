using Contract.HttpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contract.HttpResponse
{
    public class ResponseApplicableContract : Response, IResponse
    {
        public List<ApplicableContract> data { get; set; } = new List<ApplicableContract>();
    }
}
