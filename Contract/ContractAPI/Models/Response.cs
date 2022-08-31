using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Models
{
    public class Response
    {
        public bool result { get; set; }
        public String message { get; set; }
        public String error_code { get; set; }

        public Response()
        {
            result = true;
            message = "";
            error_code = "200";
        }
    }
}
