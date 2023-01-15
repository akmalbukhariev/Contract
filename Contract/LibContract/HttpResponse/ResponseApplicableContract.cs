﻿using LibContract.HttpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibContract.HttpResponse
{
    public class ResponseApprovedUnapprovedContract : Response<List<CreateContractInfo>>, IResponse
    {
        public string contragent_phone_number { get; set; }
    }
}
