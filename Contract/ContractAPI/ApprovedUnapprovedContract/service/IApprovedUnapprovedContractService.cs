using ContractAPI.DataAccess;
using LibContract.HttpModels;
using LibContract.HttpResponse;
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
        Task<ResponseApprovedUnapprovedContract> getApprovedOrUnapprovedContract(LibContract.HttpModels.ApprovedUnapprovedContract info); 
    }
}
