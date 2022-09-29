using ContractAPI.DataAccess;
using ContractAPI.Models;
using ContractAPI.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Controllers.UnapprovedContracts.service
{
   public interface IUnapprovedContractService
    {
        ContractMakerContext dataBase { get; set; }
        Task<ResponseUnapprovedContract> getUnapprovedContract(string phoneNumber);
        Task<ResponseUnapprovedContract> setUnapprovedContract(UnapprovedContract info);
        Task<ResponseUnapprovedContract> deleteUnapprovedContract(UnapprovedContract info);
        Task<ResponseCanceledContract> deleteUnapprovedContractAndSetCanceledContract(CanceledContract info);
    }
}
