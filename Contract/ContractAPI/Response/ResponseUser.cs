using ContractAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Response
{
    public class ResponseUser : Response
    {
        public User data { get; set; } = new User();
    }
}
