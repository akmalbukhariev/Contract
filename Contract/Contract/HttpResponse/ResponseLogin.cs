using Contract.HttpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contract.HttpResponse
{
    public class ResponseLogin : Response, IResponse
    {
        public User userInfo { get; set; } = new User();
    }
}
