using LibContract.HttpModels;
using LibContract.HttpResponse;
using ContractAPI.DataAccess;
using ContractAPI.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ContractAPI.ContractServices.service.impl
{
    public class ContractServiceInfoService : AppBaseService, IContractServiceInfoService
    {
        public ContractServiceInfoService(ContractMakerContext db) 
            : base(db)
        {
        }

        public async Task<ResponseServiceInfo> setContractServiceInfo(List<ServicesInfo> infoList)
        {
            ResponseServiceInfo response = new ResponseServiceInfo();

            foreach (ServicesInfo item in infoList)
            {
                ServicesInfo newItem = new ServicesInfo(item);
                //newItem.created_date = DateTime.Now.ToString(Constants.TimeFormat);

                dataBase.ServicesInfo.Add(newItem);

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
            }

            response.result = true;
            response.error_code = (int)HttpStatusCode.OK;
            response.message = Constants.Success;

            return response;
        }

        public async Task<ResponseServiceInfo> deleteContractServiceInfo(string contract_number)
        {
            ResponseServiceInfo response = new ResponseServiceInfo();

            List<ServicesInfo> foundList = await dataBase.ServicesInfo
                 .Where(item => item.contract_number.Equals(contract_number))
                 .AsNoTracking()
                 .ToListAsync();

            if (foundList.Count > 0)
            {
                ServicesInfo deleteItem = new ServicesInfo();
                deleteItem.contract_number = contract_number;
                dataBase.Remove(deleteItem);

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
            }

            response.result = true;
            response.error_code = (int)HttpStatusCode.OK;
            response.message = Constants.Success;

            return response;
        } 
    }
}
