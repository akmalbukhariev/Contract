﻿using ContractAPI.DataAccess;
using ContractAPI.Models;
using ContractAPI.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.ApplicableContracts.service
{
    public interface IApplicableContractService
    {
        ContractMakerContext dataBase { get; set; }
        Task<ResponseApplicableContract> getApplicableContract(string phoneNumber);
        Task<ResponseApplicableContract> setApplicableContract(ApplicableContract info);
        Task<ResponseApplicableContract> deleteApplicableContract(ApplicableContract info);
        Task<ResponseCanceledContract> deleteApplicableContractAndSetCanceledContract(CanceledContract info);
    }
}