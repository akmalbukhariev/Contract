﻿using LibContract.HttpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibContract.Response
{
    public class ResponseUser : Response
    {
        public User data { get; set; } = new User();
    }
}