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

        public async Task<ResponseCreateContract> createContract(CreateContractInfo info)
        {
            ResponseCreateContract response = new ResponseCreateContract();

            CreateContractInfo contractNumber = await dataBase.CreateContractInfo
                .Where(item => item.contract_number.Equals(info.contract_number))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (contractNumber != null)
            {
                response.message = "Contract number is exist.";
                response.error_code = (int)HttpStatusCode.BadRequest;
                return response;
            }
              
            CreateContractInfo newInfo = new CreateContractInfo(info);
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

            CreateContractInfo found = await dataBase.CreateContractInfo
                .Where(item => item.contract_number.Equals(contract_number))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (found == null)
            {
                response.message = "Contract number does not exist.";
                response.error_code = (int)HttpStatusCode.BadRequest;
                return response;
            }
             
            dataBase.CreateContractInfo.Remove(found);

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

        public async Task<ResponseCreateContract> cancelContract(CreateContractInfo info)
        {
            ResponseCreateContract response = new ResponseCreateContract();

            CreateContractInfo found = await dataBase.CreateContractInfo
                .Where(item => item.contract_number.Equals(info.contract_number))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (found == null)
            {
                response.message = "Contract number does not exist.";
                response.error_code = (int)HttpStatusCode.BadRequest;
                return response;
            }

            found.comment = info.comment;
            found.is_deleted = 1;
            found.deleted_date = DateTime.Now.ToString(Constants.TimeFormat);

            dataBase.CreateContractInfo.Update(found);

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

        public async Task<ResponseCanceledContract> getCanceledContract(CreateContractInfo info)
        {
            ResponseCanceledContract response = new ResponseCanceledContract();

            List<CreateContractInfo> found = await dataBase.CreateContractInfo
                .Where(item => (item.is_deleted == 1 && item.user_phone_number.Equals(info.user_phone_number) || item.client_stir.Equals(info.user_stir)))
                .AsNoTracking()
                .ToListAsync();

            foreach (CreateContractInfo item in found)
            {
                response.data.Add(new CreateContractInfo(item));
            }
            
            response.result = true;
            response.error_code = (int)HttpStatusCode.OK;
            response.message = Constants.Success;

            return response;
        }
    }
}
