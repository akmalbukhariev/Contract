﻿using LibContract.HttpModels;
using LibContract.HttpResponse;
using ContractAPI.AppInfo.service.impl;
using ContractAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ContractAPI.ContractTemplateInfo.service.impl
{
    public class ContractTemplateService : AppService, IContractTemplateService
    {
        public ContractTemplateService(ContractMakerContext db) 
            : base(db)
        {

        }

        public async Task<ResponseContractTemplate> getContractTemplate(string userPhoneNumber)
        {
            ResponseContractTemplate response = new ResponseContractTemplate();

            List<ContractTemplate> found = await dataBase.ContractTemplate
                 .Where(item => item.user_phone_number.Equals(userPhoneNumber)).ToListAsync();

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

            foreach (ContractTemplate item in found)
            {
                response.data.Add(new ContractTemplate(item));
            }

            return response;
        }

        public async Task<ResponseContractTemplate> setContractTemplate(ContractTemplate info)
        {
            ResponseContractTemplate response = new ResponseContractTemplate();

            ContractTemplate newItem = new ContractTemplate(info);
            //newItem.created_date = DateTime.Now.ToString(Constants.TimeFormat);
            dataBase.ContractTemplate.Add(newItem);

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

        public async Task<ResponseContractTemplate> updateContractTemplate(ContractTemplate info)
        {
            ResponseContractTemplate response = new ResponseContractTemplate();

            ContractTemplate newItem = new ContractTemplate(info); 
            dataBase.ContractTemplate.Update(newItem);

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

        public async Task<ResponseContractTemplate> deleteContractTemplate(ContractTemplate info)
        {
            ResponseContractTemplate response = new ResponseContractTemplate();

            ContractTemplate newItem = new ContractTemplate(info);
            dataBase.ContractTemplate.Remove(newItem);

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

        public async Task<ResponseReadyTemplate> getAllReadyTemplate()
        {
            ResponseReadyTemplate response = new ResponseReadyTemplate();
            response.result = true;
            response.error_code = (int)HttpStatusCode.OK;
            response.message = "";
            response.data = await dataBase.ReadyTemplates.ToListAsync();

            return response;
        }

        public async Task<ResponseReadyTemplate> getAllReadyTemplateUrl()
        {
            ResponseReadyTemplate response = new ResponseReadyTemplate();
            response.result = true;
            response.error_code = (int)HttpStatusCode.OK;
            response.message = "";
            
            List<ReadyTemplate> list = await dataBase.ReadyTemplates.ToListAsync();
            foreach (ReadyTemplate item in list)
            {
                ReadyTemplate newItem = new ReadyTemplate()
                {
                    id = item.id,
                    file_url = item.file_url,
                    template_name_en = item.template_name_en,
                    template_name_ru = item.template_name_ru,
                    template_name_uz = item.template_name_uz,
                    template_name_uz_cyrl = item.template_name_uz_cyrl
                };

                response.data.Add(new ReadyTemplate(newItem));    
            }

            return response;
        }
    }
}
