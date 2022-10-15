using Contract.HttpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contract.HttpResponse
{
    public class ResponseAboutApp : Response, IResponse
    {
        public AboutApp data { get; set; } = new AboutApp();
    }
}
