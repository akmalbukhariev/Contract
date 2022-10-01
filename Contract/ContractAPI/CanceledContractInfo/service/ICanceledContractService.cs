using ContractAPI.DataAccess;
using ContractAPI.Models;
using ContractAPI.Response;
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
