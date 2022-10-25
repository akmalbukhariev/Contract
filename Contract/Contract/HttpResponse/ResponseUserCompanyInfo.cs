﻿using Contract.HttpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contract.HttpResponse
{
    public class ResponseUserCompanyInfo : Response, IResponse
    {
        public CompanyInfo data { get; set; } = new CompanyInfo();
    }
}