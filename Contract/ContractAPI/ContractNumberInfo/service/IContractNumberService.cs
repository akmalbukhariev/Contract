﻿using Contract.HttpModels;
using Contract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.ContractNumberInfo.service
{
    public interface IContractNumberService
    {
        Task<ResponseContractNumberTemplate> getContractNumber(string userPhoneNumber);
        Task<ResponseContractNumberTemplate> setContractNumber(ContractNumberTemplate info);
        Task<ResponseContractNumberTemplate> updateContractNumber(ContractNumberTemplate info); 
    }
}
