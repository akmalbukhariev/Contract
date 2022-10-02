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

            ClientCompanyInfo newInfo1 = new ClientCompanyInfo(info.client_company_info);
            newInfo1.created_date = DateTime.Now.ToString(Constants.TimeFormat);

            CreateContractInfo newInfo2 = new CreateContractInfo(info.contract_info);
            newInfo2.created_date = DateTime.Now.ToString(Constants.TimeFormat);

            ContractMakerContext contect1 = dataBase.CreateNew();
            ContractMakerContext contect2 = dataBase.CreateNew();

            contect1.ClientCompanyInfo.Add(newInfo1);
            contect2.CreateContractInfo.Add(newInfo2);

            foreach (ServicesInfo item in info.service_list)
            {
                ServicesInfo newInfo3 = new ServicesInfo(item);
                newInfo3.created_date = DateTime.Now.ToString(Constants.TimeFormat);

                dataBase.ServicesInfo.Add(newInfo3);
                await dataBase.SaveChangesAsync();
            }

            try
            {
                await contect1.SaveChangesAsync();
                await contect2.SaveChangesAsync();
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
    }
}
