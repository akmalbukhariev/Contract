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
        Task<ResponseUserCompanyInfo> getCompanyInfo(string phoneNumber);
        Task<ResponseUserCompanyInfo> setCompanyInfo(CompanyInfo info);
        Task<ResponseUserCompanyInfo> updateCompanyInfo(CompanyInfo info);
    }
}
