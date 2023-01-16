using LibContract.HttpModels;
using LibContract.HttpResponse;
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

        public async Task<ResponseContractNumberTemplate> getContractNumber(string userPhoneNumber)
        {
            ResponseContractNumberTemplate response = new ResponseContractNumberTemplate();

            List<ContractNumberTemplate> found = await dataBase.ContractNumberTemplate
                 .Where(item => item.user_phone_number.Equals(userPhoneNumber) && item.is_deleted == 0).ToListAsync(); 

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

            foreach (ContractNumberTemplate item in found)
            {
                response.data.Add(new ContractNumberTemplate(item));
            }

            return response;
        }

        public async Task<ResponseContractNumberTemplate> setContractNumber(ContractNumberTemplate info)
        {
            ResponseContractNumberTemplate response = new ResponseContractNumberTemplate();

            ContractNumberTemplate found = await dataBase.ContractNumberTemplate
                 .Where(item => item.user_phone_number.Equals(info.user_phone_number) && 
                        item.option.Equals(info.option) && 
                        item.format == info.format)
                 .AsNoTracking()
                 .FirstOrDefaultAsync();
            
            if (found != null)
            {
                response.data = null;
                response.message = "Exist";
                response.error_code = (int)HttpStatusCode.Found;
                return response;
            }

            ContractNumberTemplate newItem = new ContractNumberTemplate(info);
            newItem.created_date = DateTime.Now.ToString(Constants.TimeFormat);
            dataBase.ContractNumberTemplate.Add(newItem);

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

        public async Task<ResponseContractNumberTemplate> updateContractNumber(ContractNumberTemplate info)
        {
            ResponseContractNumberTemplate response = new ResponseContractNumberTemplate();

            ContractNumberTemplate newItem = new ContractNumberTemplate(info);
            dataBase.ContractNumberTemplate.Update(newItem);

            List<ContractTemplate> templateList = await dataBase.ContractTemplate
                                                        .Where(item => item.contract_number_format_id == info.id)
                                                        .AsNoTracking()
                                                        .ToListAsync();

            foreach (ContractTemplate i in templateList)
            {
                ContractTemplate newTemplate = new ContractTemplate(i);
                newTemplate.contract_number_option = info.option;

                dataBase.ContractTemplate.Update(newTemplate);
            }

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
