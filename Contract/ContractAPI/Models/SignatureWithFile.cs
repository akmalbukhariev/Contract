using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Models
{
    public class SignatureWithFile
    {
        public string phone_number { get; set; }
        public IFormFile dataStream { get; set; }
    }
}
