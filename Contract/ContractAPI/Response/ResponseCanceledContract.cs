﻿using ContractAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Response
{
    public class ResponseCanceledContract : Response
    {
        public List<CanceledContract> data { get; set; } = new List<CanceledContract>();
    }
}
