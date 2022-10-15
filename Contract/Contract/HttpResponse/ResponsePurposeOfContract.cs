using Contract.HttpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contract.HttpResponse
{
    public class ResponsePurposeOfContract : Response, IResponse
    {
        public PurposeOfContract data { get; set; } = new PurposeOfContract();
    }
}
