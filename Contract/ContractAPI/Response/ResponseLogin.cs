﻿using ContractAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Response
{
    public class ResponseLogin : Response
    {
        public User userInfo { get; set; } = new User();
    }
}