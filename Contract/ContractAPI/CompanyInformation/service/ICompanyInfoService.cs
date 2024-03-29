﻿using ContractAPI.DataAccess;
using LibContract.HttpModels;
using LibContract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContractAPI.Models;

namespace ContractAPI.CompanyInformation.service
{
    public interface ICompanyInfoService
    {
        Task<ResponseUserCompanyInfo> getUserCompanyInfo(string phoneNumber);
        Task<ResponseClientCompanyInfo> getClientCompanyInfo(string phoneNumber);
        Task<ResponseUserCompanyInfo> getUserCompanyInfoToCreateContract(string stirNumber);
        Task<ResponseUserCompanyInfo> deleteUserCompanyInfo(DeleteCompanyInfo info);
        Task<ResponseClientCompanyInfo> deleteClientCompanyInfo(DeleteCompanyInfo info);

        Task<ResponseUserCompanyInfo> setUserCompanyInfo(CompanyInfo info);
        Task<ResponseUserCompanyInfo> setUserCompanyInfoWithFile(CompanyInfoWithFile info);

        Task<ResponseClientCompanyInfo> setClientCompanyInfoToCreateContract(CompanyInfo info);
        Task<ResponseClientCompanyInfo> setClientCompanyInfo(CompanyInfo info);
        Task<ResponseClientCompanyInfo> setClientCompanyInfoWithFile(CompanyInfoWithFile info);

        Task<ResponseUserCompanyInfo> updateUserCompanyInfo(CompanyInfo info);
        Task<ResponseUserCompanyInfo> updateUserCompanyInfoWithFile(CompanyInfoWithFile info);

        Task<ResponseClientCompanyInfo> updateClientCompanyInfo(CompanyInfo info);
        Task<ResponseClientCompanyInfo> updateClientCompanyInfoWithFile(CompanyInfoWithFile info);
    }
}
