using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ContractAPI.Models
{
    public class Response
    {
        public bool result { get; set; }
        public String message { get; set; }
        public int error_code { get; set; }

        public Response()
        {
            result = false;
            message = Constants.NotFound;
            error_code = (int)HttpStatusCode.NotFound;
        }
    }
}
