using ContractAPI.DataAccess;
using LibContract.HttpModels;
using LibContract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.ContractInfo.service
{
    public interface IContractService
    {
        Task<ResponseCreateContract> getNewContractNumber(string phoneNumber);
        Task<ResponsePurposeOfContract> getPurposeOfContract(string phoneNumber);
        Task<ResponsePurposeOfContract> setPurposeOfContract(PurposeOfContract info);
        Task<ResponseCreateContract> createContract(CreateContractInfo info);
        Task<ResponseCreateContract> deleteContract(string contract_number);
        Task<ResponseCreateContract> cancelContract(CreateContractInfo info);
        Task<ResponseCanceledContract> getCanceledContract(CreateContractInfo info);
    }
}
