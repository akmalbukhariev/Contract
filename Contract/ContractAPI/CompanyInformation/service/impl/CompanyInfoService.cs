using ContractAPI.DataAccess;
using ContractAPI.Helper;
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
    public class CompanyInfoService : AppBaseService, ICompanyInfoService
    {
        public CompanyInfoService(ContractMakerContext db)
        {
            dataBase = db;
        }

        public async Task<ResponseClientCompanyInfo> getClientCompanyInfo(string phoneNumber)
        {
            ResponseClientCompanyInfo response = new ResponseClientCompanyInfo();
            List<ClientCompanyInfo> infoList = await dataBase.ClientCompanyInfo
                .Where(item => item.user_phone_number.Equals(phoneNumber))
                .AsNoTracking()
                .ToListAsync();

            if (infoList == null)
            {
                response.data = null;
                return response;
            }

            response.result = true;
            response.message = Constants.Success;

            foreach (ClientCompanyInfo info in infoList)
            {
                response.data.Add(new CompanyInfo(info));
            }
            return response;
        }

        public async Task<ResponseUserCompanyInfo> getUserCompanyInfo(string phoneNumber)
        {
            ResponseUserCompanyInfo response = new ResponseUserCompanyInfo();
            CompanyInfo info = await dataBase.UserCompanyInfo
                .Where(item => item.user_phone_number.Equals(phoneNumber))
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

        public async Task<ResponseClientCompanyInfo> setClientCompanyInfo(CompanyInfo info)
        {
            ResponseClientCompanyInfo response = new ResponseClientCompanyInfo();
            response.data = null;

            CompanyInfo found = await dataBase.ClientCompanyInfo
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

            var newInfo = new ClientCompanyInfo();
            newInfo.Copy(info);
            dataBase.ClientCompanyInfo.Add(newInfo);

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

        public async Task<ResponseUserCompanyInfo> setUserCompanyInfo(CompanyInfo info)
        {
            ResponseUserCompanyInfo response = new ResponseUserCompanyInfo();
            response.data = null;

            CompanyInfo found = await dataBase.UserCompanyInfo
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

            var newInfo = new UserCompanyInfo();
            newInfo.Copy(info);
            dataBase.UserCompanyInfo.Add(newInfo);

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

        public async Task<ResponseUserCompanyInfo> updateClientCompanyInfo(CompanyInfo info)
        {
            ResponseUserCompanyInfo response = new ResponseUserCompanyInfo();
            response.data = null;

            CompanyInfo found = await dataBase.ClientCompanyInfo
                .Where(item => item.user_phone_number.Equals(info.user_phone_number))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (found == null)
            {
                response.result = false;
                response.message = "Not found the user phone number!";

                return response;
            }

            var newInfo = new ClientCompanyInfo();
            newInfo.Copy(info);

            dataBase.ClientCompanyInfo.Update(newInfo);

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

        public async Task<ResponseUserCompanyInfo> updateUserCompanyInfo(CompanyInfo info)
        {
            ResponseUserCompanyInfo response = new ResponseUserCompanyInfo();
            response.data = null;

            CompanyInfo found = await dataBase.UserCompanyInfo
                .Where(item => item.user_phone_number.Equals(info.user_phone_number))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (found == null)
            {
                response.result = false;
                response.message = "Not found the user phone number!";

                return response;
            }

            var newInfo = new UserCompanyInfo();
            newInfo.Copy(info);
            
            dataBase.UserCompanyInfo.Update(newInfo);

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
