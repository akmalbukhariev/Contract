using Contract.HttpModels;
using Contract.HttpResponse;
using ContractAPI.AppInfo.service.impl;
using ContractAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ContractAPI.ContractNumberInfo.service.impl
{
    public class ContractNumberService : AppService, IContractNumberService
    {
        public ContractNumberService(ContractMakerContext db) : base(db)
        {

        }

        public async Task<ResponseContractNumber> getContractNumber(string userPhoneNumber)
        {
            ResponseContractNumber response = new ResponseContractNumber();

            ContractNumber found = await dataBase.ContractNumber
                 .Where(item => item.user_phone_number.Equals(userPhoneNumber))
                 .AsNoTracking()
                 .FirstOrDefaultAsync();

            if (found == null)
            {
                response.data = null;
                response.message = Constants.NotFound;
                response.error_code = (int)HttpStatusCode.NotFound;
                return response;
            }

            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;
            response.data.Copy(found);

            return response;
        }

        public async Task<ResponseContractNumber> setContractNumber(ContractNumber info)
        {
            ResponseContractNumber response = new ResponseContractNumber();

            ContractNumber newItem = new ContractNumber(info);
            dataBase.ContractNumber.Add(newItem);

            try
            {
                await dataBase.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.data = null;
                response.message = ex.Message;
                response.error_code = (int)HttpStatusCode.NotFound;
                return response;
            }

            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;

            return response;
        }

        public async Task<ResponseContractNumber> updateContractNumber(ContractNumber info)
        {
            ResponseContractNumber response = new ResponseContractNumber();

            ContractNumber newItem = new ContractNumber(info);
            dataBase.ContractNumber.Update(newItem);

            try
            {
                await dataBase.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.data = null;
                response.message = ex.Message;
                response.error_code = (int)HttpStatusCode.NotFound;
                return response;
            }

            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;

            return response;
        }
    }
}
