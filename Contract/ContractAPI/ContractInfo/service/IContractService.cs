using ContractAPI.DataAccess;
using Contract.HttpModels;
using Contract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.ContractInfo.service
{
    public interface IContractService
    {
        Task<ResponsePurposeOfContract> getPurposeOfContract(string phoneNumber);
        Task<ResponsePurposeOfContract> setPurposeOfContract(PurposeOfContract info);
        Task<ResponseCreateContract> createContract(CreateContract info);
        Task<ResponseCreateContract> deleteContract(string contract_number);
    }
}
