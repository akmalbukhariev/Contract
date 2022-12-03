using ContractAPI.DataAccess;
using LibContract.HttpModels;
using LibContract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.CanceledContractInfo.service
{
    public interface ICanceledContractService
    { 
        Task<ResponseCanceledContract> getCanceledContract(string phoneNumber);
        Task<ResponseCanceledContract> setCanceledContract(CanceledContract info);
        Task<ResponseCanceledContract> deleteCanceledContract(CanceledContract info); 
    }
}
