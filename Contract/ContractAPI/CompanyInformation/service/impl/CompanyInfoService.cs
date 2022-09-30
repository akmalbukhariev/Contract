using ContractAPI.DataAccess;
using ContractAPI.Models;
using ContractAPI.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ContractAPI.CompanyInformation.service.impl
{
    public class CompanyInfoService : ICompanyInfoService
    {
        public ContractMakerContext dataBase { get; set; }

        public async Task<ResponseUserCompanyInfo> getCompanyInfo(string phoneNumber)
        {
            ResponseUserCompanyInfo response = new ResponseUserCompanyInfo();
            CompanyInfo info = await dataBase.CompanyInfo.Where(item => item.user_phone_number.Equals(phoneNumber))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (info == null)
            {
                response.data = null;
                return response;
            }

            response.result = true;
            response.message = Constants.Success;
            response.data.Copy(info);

            return response;
        }

        public async Task<ResponseUserCompanyInfo> setCompanyInfo(CompanyInfo info)
        {
            ResponseUserCompanyInfo response = new ResponseUserCompanyInfo();
            response.data = null;

            CompanyInfo found = await dataBase.CompanyInfo
                .Where(item => item.user_phone_number.Equals(info.user_phone_number))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (found != null)
            {
                response.result = false;
                response.message = "User phone number already exist!";
                response.error_code = (int)HttpStatusCode.BadRequest;
                return response;
            }

            var newInfo = new CompanyInfo();
            newInfo.Copy(info);
            dataBase.CompanyInfo.Add(newInfo);

            try
            {
                await dataBase.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.error_code = (int)HttpStatusCode.BadRequest;
                return response;
            }

            response.result = true;
            response.error_code = (int)HttpStatusCode.OK;
            response.message = Constants.Success;

            return response;
        }

        public async Task<ResponseUserCompanyInfo> updateCompanyInfo(CompanyInfo info)
        {
            ResponseUserCompanyInfo response = new ResponseUserCompanyInfo();
            response.data = null;

            CompanyInfo found = await dataBase.CompanyInfo
                .Where(item => item.user_phone_number.Equals(info.user_phone_number))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (found == null)
            {
                response.result = false;
                response.message = "Not found the user phone number!";

                return response;
            }

            var newInfo = new CompanyInfo();
            newInfo.Copy(info);
            
            dataBase.Update(newInfo);

            try
            {
                await dataBase.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.error_code = (int)HttpStatusCode.BadRequest;

                return response;
            }

            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;

            return response;
        }
    }
}
