using ContractAPI.DataAccess;
using ContractAPI.Models;
using ContractAPI.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.CompanyInformation.service
{
    public interface ICompanyInfoService
    {
        Task<ResponseUserCompanyInfo> getUserCompanyInfo(string phoneNumber);
        Task<ResponseClientCompanyInfo> getClientCompanyInfo(string phoneNumber);
        Task<ResponseUserCompanyInfo> setUserCompanyInfo(CompanyInfo info);
        Task<ResponseClientCompanyInfo> setClientCompanyInfo(CompanyInfoWithFile info);
        Task<ResponseUserCompanyInfo> updateUserCompanyInfo(CompanyInfo info);
        Task<ResponseClientCompanyInfo> updateClientCompanyInfo(CompanyInfo info);
    }
}
