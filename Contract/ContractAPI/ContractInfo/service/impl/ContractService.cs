using ContractAPI.DataAccess;
using ContractAPI.Helper;
using Contract.HttpModels;
using Contract.HttpResponse;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ContractAPI.ContractInfo.service.impl
{
    public class ContractService : AppBaseService, IContractService
    {
        public ContractService(ContractMakerContext db)
        {
            dataBase = db;
        }

        public async Task<ResponsePurposeOfContract> getPurposeOfContract(string phoneNumber)
        {
            ResponsePurposeOfContract response = new ResponsePurposeOfContract();
            PurposeOfContract info = await dataBase.PurposeOfContracts
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
            response.error_code = (int)HttpStatusCode.OK;
            response.data.Copy(info);

            return response;
        }

        public async Task<ResponsePurposeOfContract> setPurposeOfContract(PurposeOfContract info)
        {
            ResponsePurposeOfContract response = new ResponsePurposeOfContract();
            response.data = null;

            PurposeOfContract found = await dataBase.PurposeOfContracts
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

            var newInfo = new PurposeOfContract();
            newInfo.Copy(info);

            dataBase.PurposeOfContracts.Add(newInfo);

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

        public async Task<ResponseCreateContract> createContract(CreateContract info)
        {
            ResponseCreateContract response = new ResponseCreateContract();

            CreateContractInfo contractNumber = await dataBase.CreateContractInfo
                .Where(item => item.contract_number.Equals(info.contract_info.contract_number))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (contractNumber != null)
            {
                response.message = "Contract number is exist.";
                response.error_code = (int)HttpStatusCode.BadRequest;
                return response;
            }
              
            CreateContractInfo newInfo = new CreateContractInfo(info.contract_info);
            newInfo.created_date = DateTime.Now.ToString(Constants.TimeFormat);
             
            dataBase.CreateContractInfo.Add(newInfo);

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

        public async Task<ResponseCreateContract> deleteContract(string contract_number)
        {
            ResponseCreateContract response = new ResponseCreateContract();

            CreateContractInfo contractNumber = await dataBase.CreateContractInfo
                .Where(item => item.contract_number.Equals(contract_number))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (contractNumber == null)
            {
                response.message = "Contract number does not exist.";
                response.error_code = (int)HttpStatusCode.BadRequest;
                return response;
            }

            dataBase.CreateContractInfo.Remove(contractNumber);

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

        //public async Task<ResponseCreateContract> createContract(CreateContract info)
        //{
        //    ResponseCreateContract response = new ResponseCreateContract();

        //    CreateContractInfo contractNumber = await dataBase.CreateContractInfo
        //        .Where(item => item.contract_number.Equals(info.contract_info.contract_number))
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync();

        //    if (contractNumber != null)
        //    {
        //        response.message = "Contract number is exist.";
        //        response.error_code = (int)HttpStatusCode.BadRequest;
        //        return response;
        //    }

        //    ClientCompanyInfo stirNumber = await dataBase.ClientCompanyInfo
        //        .Where(item => item.stir_of_company.Equals(info.client_company_info.stir_of_company))
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync();

        //    if (stirNumber == null)
        //    {
        //        ContractMakerContext contect1 = dataBase.CreateNew();

        //        ClientCompanyInfo newInfo1 = new ClientCompanyInfo(info.client_company_info);
        //        newInfo1.created_date = DateTime.Now.ToString(Constants.TimeFormat);
        //        contect1.ClientCompanyInfo.Add(newInfo1);
        //        try
        //        {
        //            await contect1.SaveChangesAsync();
        //        }
        //        catch(Exception ex)
        //        {
        //            response.message = ex.Message;
        //            response.error_code = (int)HttpStatusCode.BadRequest;
        //            return response;
        //        }
        //    }

        //    foreach (ServicesInfo item in info.service_list)
        //    {
        //        ServicesInfo newInfo3 = new ServicesInfo(item);
        //        newInfo3.created_date = DateTime.Now.ToString(Constants.TimeFormat);

        //        dataBase.ServicesInfo.Add(newInfo3);

        //        try
        //        {
        //            await dataBase.SaveChangesAsync();
        //        }
        //        catch (Exception ex)
        //        {
        //            response.message = ex.Message;
        //            response.error_code = (int)HttpStatusCode.BadRequest;
        //            return response;
        //        }
        //    }

        //    CreateContractInfo newInfo2 = new CreateContractInfo(info.contract_info);
        //    newInfo2.created_date = DateTime.Now.ToString(Constants.TimeFormat);
        //    ContractMakerContext contect2 = dataBase.CreateNew();
        //    contect2.CreateContractInfo.Add(newInfo2);

        //    try
        //    {
        //        await contect2.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        foreach (ServicesInfo item in dataBase.ServicesInfo)
        //        {
        //            dataBase.ServicesInfo.Remove(item);

        //            try
        //            {
        //                await dataBase.SaveChangesAsync();
        //            }
        //            catch (Exception ex1)
        //            {
        //                response.message = ex1.Message;
        //                response.error_code = (int)HttpStatusCode.BadRequest;
        //                return response;
        //            }
        //        }
        //        response.message = ex.Message;
        //        response.error_code = (int)HttpStatusCode.BadRequest;
        //        return response;
        //    }

        //    response.result = true;
        //    response.error_code = (int)HttpStatusCode.OK;
        //    response.message = Constants.Success;

        //    return response;
        //}
    }
}
