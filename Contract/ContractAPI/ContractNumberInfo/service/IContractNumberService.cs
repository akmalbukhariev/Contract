using Contract.HttpModels;
using Contract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.ContractNumberInfo.service
{
    public interface IContractNumberService
    {
        Task<ResponseContractNumber> getContractNumber(string userPhoneNumber);
        Task<ResponseContractNumber> setContractNumber(ContractNumber info);
        Task<ResponseContractNumber> updateContractNumber(ContractNumber info);
    }
}
