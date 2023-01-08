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
        Task<ResponseApprovedUnapprovedContract> setApprovedContract(LibContract.HttpModels.ApprovedUnapprovedContract info);
        Task<ResponseApprovedUnapprovedContract> setUnapprovedContract(LibContract.HttpModels.ApprovedUnapprovedContract info);
        Task<ResponseApprovedUnapprovedContract> getUnapprovedContract(LibContract.HttpModels.ApprovedUnapprovedContract info);
        Task<ResponseApprovedUnapprovedContract> getApprovedContract(LibContract.HttpModels.ApprovedUnapprovedContract info);
        Task<ResponseApprovedUnapprovedContract> getApprovedAndUnapprovedContract(LibContract.HttpModels.ApprovedUnapprovedContract info);
    }
}
