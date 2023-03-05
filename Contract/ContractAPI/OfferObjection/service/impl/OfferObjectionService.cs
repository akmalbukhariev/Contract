using LibContract.HttpModels;
using LibContract.HttpResponse;
using ContractAPI.DataAccess;
using ContractAPI.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ContractAPI.OfferObjection.service.impl
{
    public class OfferObjectionService : AppBaseService, IOfferObjectionService
    {
        public OfferObjectionService(ContractMakerContext db)
            :base(db)
        {
        }

        public async Task<ResponseOfferObjection> Save(OfferAndObjection info)
        {
            ResponseOfferObjection response = new ResponseOfferObjection();
            OfferAndObjection newItem = new OfferAndObjection(info);
            newItem.created_date = DateTime.Now.ToString("yyyymmdd_hhmmss.fff");

            dataBase.OfferAndObjection.Add(newItem);

            try
            {
                await dataBase.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.result = false;
                response.message = ex.Message;
                response.error_code = (int)HttpStatusCode.BadRequest;
                return response;
            }

            response.result = true;
            response.error_code = (int)HttpStatusCode.OK;
            return response;
        }
    }
}
