﻿using ContractAPI.DataAccess;
using ContractAPI.Helper;
using LibContract.HttpModels;
using LibContract.HttpResponse;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ContractAPI.Models;
using Microsoft.AspNetCore.Http;

namespace ContractAPI.CompanyInformation.service.impl
{
    public class CompanyInfoService : AppBaseService, ICompanyInfoService
    {
        public static IWebHostEnvironment _environment;
        public CompanyInfoService(ContractMakerContext db, IWebHostEnvironment environment)
            :base(db)
        {
            _environment = environment;
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

        public async Task<ResponseUserCompanyInfo> getUserCompanyInfoToCreateContract(string stirNumber)
        {
            ResponseUserCompanyInfo response = new ResponseUserCompanyInfo();
            CompanyInfo info = await dataBase.UserCompanyInfo
                .Where(item => item.stir_of_company.Equals(stirNumber))
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


        public async Task<ResponseUserCompanyInfo> deleteUserCompanyInfo(DeleteCompanyInfo info)
        {
            ResponseUserCompanyInfo response = new ResponseUserCompanyInfo();
            response.data = null;

            UserCompanyInfo found = await dataBase.UserCompanyInfo
                .Where(item => item.user_phone_number.Equals(info.user_phone_number) && item.stir_of_company.Equals(info.stir_of_company))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (found == null)
            {
                response.result = false;
                response.message = Constants.NotFound;
                response.error_code = (int)HttpStatusCode.BadRequest;
                return response;
            }
             
            dataBase.UserCompanyInfo.Remove(found);

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
        public async Task<ResponseClientCompanyInfo> deleteClientCompanyInfo(DeleteCompanyInfo info)
        {
            ResponseClientCompanyInfo response = new ResponseClientCompanyInfo();
            response.data = null;

            ClientCompanyInfo found = await dataBase.ClientCompanyInfo
                .Where(item => item.user_phone_number.Equals(info.user_phone_number) && item.stir_of_company.Equals(info.stir_of_company))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (found == null)
            {
                response.result = false;
                response.message = Constants.NotFound;
                response.error_code = (int)HttpStatusCode.BadRequest;
                return response;
            }
             
            dataBase.ClientCompanyInfo.Remove(found);

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
                .Where(item => item.stir_of_company.Equals(info.stir_of_company))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (found != null)
            {
                response.result = false;
                response.message = Constants.Exist;
                response.error_code = (int)HttpStatusCode.BadRequest;
                return response;
            }

            var newInfo = new UserCompanyInfo();
            newInfo.Copy(info);
            //newInfo.created_date = DateTime.Now.ToString(Constants.TimeFormat);

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
        public async Task<ResponseUserCompanyInfo> setUserCompanyInfoWithFile(CompanyInfoWithFile info)
        {
            ResponseUserCompanyInfo response = new ResponseUserCompanyInfo();
            response.data = null;

            CompanyInfo found = await dataBase.UserCompanyInfo
                .Where(item => item.stir_of_company.Equals(info.stir_of_company))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (found != null)
            {
                response.result = false;
                response.message = Constants.Exist;
                response.error_code = (int)HttpStatusCode.BadRequest;
                return response;
            }

            await CreateFile($"{_environment.WebRootPath}{Constants.SaveCompanyImagePath}", info.company_logo_url);

            var newInfo = new UserCompanyInfo();
            newInfo.user_phone_number = info.user_phone_number;
            newInfo.company_name = info.company_name;
            newInfo.document = info.document;
            newInfo.document_index = info.document_index;
            newInfo.address_of_company = info.address_of_company;
            newInfo.account_number = info.account_number;
            newInfo.stir_of_company = info.stir_of_company;
            newInfo.name_of_bank = info.name_of_bank;
            newInfo.bank_code = info.bank_code;
            newInfo.are_you_qqs_payer = 0;
            newInfo.qqs_number = info.qqs_number;
            newInfo.company_phone_number = info.company_phone_number;
            newInfo.position_of_signer = info.position_of_signer;
            newInfo.name_of_signer = info.name_of_signer;
            newInfo.is_accountant_provided = 0;
            newInfo.accountant_name = info.accountant_name;
            newInfo.is_legal_counsel_provided = 0;
            newInfo.counsel_name = info.counsel_name;
            newInfo.company_logo_url = Constants.SaveCompanyImagePath.Replace("\\", "/") + info.company_logo_url.FileName;
            //newInfo.created_date = "";

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


        public async Task<ResponseClientCompanyInfo> setClientCompanyInfoToCreateContract(CompanyInfo info)
        {
            ResponseClientCompanyInfo response = new ResponseClientCompanyInfo();
            response.data = null;

            CompanyInfo found = await dataBase.ClientCompanyInfo
                .Where(item => item.stir_of_company.Equals(info.stir_of_company))
                .AsNoTracking()
                .FirstOrDefaultAsync();
             
            var newInfo = new ClientCompanyInfo();
            newInfo.Copy(info);
            //newInfo.created_date = DateTime.Now.ToString(Constants.TimeFormat);

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

        public async Task<ResponseClientCompanyInfo> setClientCompanyInfo(CompanyInfo info)
        {
            ResponseClientCompanyInfo response = new ResponseClientCompanyInfo();
            response.data = null;

            CompanyInfo found = await dataBase.ClientCompanyInfo
                .Where(item => item.stir_of_company.Equals(info.stir_of_company))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (found != null)
            {
                response.result = false;
                response.message = Constants.Exist;
                response.error_code = (int)HttpStatusCode.BadRequest;
                return response;
            }

            var newInfo = new ClientCompanyInfo();
            newInfo.Copy(info);
            //newInfo.created_date = DateTime.Now.ToString(Constants.TimeFormat);

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
        public async Task<ResponseClientCompanyInfo> setClientCompanyInfoWithFile(CompanyInfoWithFile info)
        {
            ResponseClientCompanyInfo response = new ResponseClientCompanyInfo();
            response.data = null;
             
            CompanyInfo found = await dataBase.ClientCompanyInfo
                .Where(item => item.stir_of_company.Equals(info.stir_of_company))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (found != null)
            {
                response.result = false;
                response.message = Constants.Exist;
                response.error_code = (int)HttpStatusCode.BadRequest;
                return response;
            }
             
            await CreateFile($"{_environment.WebRootPath}{Constants.SaveCompanyImagePath}", info.company_logo_url);

            var newInfo = new ClientCompanyInfo();
            newInfo.user_phone_number = info.user_phone_number;
            newInfo.company_name = info.company_name;
            newInfo.address_of_company = info.address_of_company;
            newInfo.account_number = info.account_number;
            newInfo.stir_of_company = info.stir_of_company;
            newInfo.name_of_bank = info.name_of_bank;
            newInfo.bank_code = info.bank_code;
            newInfo.are_you_qqs_payer = 0;
            newInfo.qqs_number = info.qqs_number;
            newInfo.company_phone_number = info.company_phone_number;
            newInfo.position_of_signer = info.position_of_signer;
            newInfo.name_of_signer = info.name_of_signer;
            newInfo.is_accountant_provided = 0;
            newInfo.accountant_name = info.accountant_name;
            newInfo.is_legal_counsel_provided = 0;
            newInfo.counsel_name = info.counsel_name;
            newInfo.company_logo_url = Constants.SaveCompanyImagePath.Replace("\\", "/") + info.company_logo_url.FileName;
            //newInfo.created_date = DateTime.Now.ToString(Constants.TimeFormat);
            
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

         
        public async Task<ResponseUserCompanyInfo> updateUserCompanyInfo(CompanyInfo info)
        {
            ResponseUserCompanyInfo response = new ResponseUserCompanyInfo();
            response.data = null;

            CompanyInfo found = await dataBase.UserCompanyInfo
                .Where(item => item.id.Equals(info.id))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (found == null)
            {
                response.result = false;
                response.message = Constants.NotFound;

                return response;
            }

            var newInfo = new UserCompanyInfo();
            newInfo.Copy(info);

            if (string.IsNullOrEmpty(info.company_logo_url))
                newInfo.company_logo_url = found.company_logo_url;

            if (string.IsNullOrEmpty(info.created_date))
                newInfo.created_date = found.created_date;

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
        public async Task<ResponseUserCompanyInfo> updateUserCompanyInfoWithFile(CompanyInfoWithFile info)
        {
            ResponseUserCompanyInfo response = new ResponseUserCompanyInfo();
            response.data = null;

            CompanyInfo found = await dataBase.UserCompanyInfo
                .Where(item => item.id.Equals(info.id))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (found == null)
            {
                response.result = false;
                response.message = Constants.NotFound;

                return response;
            }

            var newInfo = new UserCompanyInfo();
            newInfo.id = info.id;
            newInfo.user_phone_number = info.user_phone_number;
            newInfo.company_name = info.company_name;
            newInfo.document = info.document;
            newInfo.document_index = info.document_index;
            newInfo.address_of_company = info.address_of_company;
            newInfo.account_number = info.account_number;
            newInfo.stir_of_company = info.stir_of_company;
            newInfo.name_of_bank = info.name_of_bank;
            newInfo.bank_code = info.bank_code;
            newInfo.are_you_qqs_payer = 0;
            newInfo.qqs_number = info.qqs_number;
            newInfo.company_phone_number = info.company_phone_number;
            newInfo.position_of_signer = info.position_of_signer;
            newInfo.name_of_signer = info.name_of_signer;
            newInfo.is_accountant_provided = 0;
            newInfo.accountant_name = info.accountant_name;
            newInfo.is_legal_counsel_provided = 0;
            newInfo.counsel_name = info.counsel_name;

            DeleteFile($"{_environment.WebRootPath}{found.company_logo_url}");
            await CreateFile($"{_environment.WebRootPath}{Constants.SaveCompanyImagePath}", info.company_logo_url);
            newInfo.company_logo_url = Constants.SaveCompanyImagePath.Replace("\\", "/") + info.company_logo_url.FileName;
            
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


        public async Task<ResponseClientCompanyInfo> updateClientCompanyInfo(CompanyInfo info)
        {
            ResponseClientCompanyInfo response = new ResponseClientCompanyInfo();
            response.data = null;

            CompanyInfo found = await dataBase.ClientCompanyInfo
                .Where(item => item.id.Equals(info.id))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (found == null)
            {
                response.result = false;
                response.message = Constants.NotFound;

                return response;
            }

            var newInfo = new ClientCompanyInfo();
            newInfo.Copy(info);
            newInfo.company_logo_url = info.company_logo_url;
            newInfo.created_date = found.created_date;

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
        public async Task<ResponseClientCompanyInfo> updateClientCompanyInfoWithFile(CompanyInfoWithFile info)
        {
            ResponseClientCompanyInfo response = new ResponseClientCompanyInfo();
            response.data = null;

            CompanyInfo found = await dataBase.ClientCompanyInfo
                .Where(item => item.id.Equals(info.id))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (found == null)
            {
                response.result = false;
                response.message = Constants.NotFound;

                return response;
            }

            var newInfo = new ClientCompanyInfo();
            newInfo.id = info.id;
            newInfo.user_phone_number = info.user_phone_number;
            newInfo.company_name = info.company_name;
            newInfo.document = info.document;
            newInfo.document_index = info.document_index;
            newInfo.address_of_company = info.address_of_company;
            newInfo.account_number = info.account_number;
            newInfo.stir_of_company = info.stir_of_company;
            newInfo.name_of_bank = info.name_of_bank;
            newInfo.bank_code = info.bank_code;
            newInfo.are_you_qqs_payer = 0;
            newInfo.qqs_number = info.qqs_number;
            newInfo.company_phone_number = info.company_phone_number;
            newInfo.position_of_signer = info.position_of_signer;
            newInfo.name_of_signer = info.name_of_signer;
            newInfo.is_accountant_provided = 0;
            newInfo.accountant_name = info.accountant_name;
            newInfo.is_legal_counsel_provided = 0;
            newInfo.counsel_name = info.counsel_name;

            DeleteFile($"{_environment.WebRootPath}{found.company_logo_url}");
            await CreateFile($"{_environment.WebRootPath}{Constants.SaveCompanyImagePath}", info.company_logo_url);
            newInfo.company_logo_url = Constants.SaveCompanyImagePath.Replace("\\", "/") + info.company_logo_url.FileName;
             
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
    }
}
