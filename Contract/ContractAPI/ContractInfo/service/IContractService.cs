using ContractAPI.DataAccess;
using ContractAPI.Models;
using ContractAPI.Response;
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
    }
}
