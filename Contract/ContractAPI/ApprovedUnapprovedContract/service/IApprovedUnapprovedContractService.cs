using ContractAPI.DataAccess;
using Contract.HttpModels;
using Contract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.ApprovedUnapprovedContract.service
{
    public interface IApprovedUnapprovedContractService
    {  
        Task<ResponseApprovedUnapprovedContract> setApprovedContract(string contract_number);
        Task<ResponseApprovedUnapprovedContract> setUnapprovedContract(string contract_number);
        Task<ResponseApprovedUnapprovedContract> getApprovedOrUnapprovedContract(Contract.HttpModels.ApprovedUnapprovedContract info); 
    }
}
