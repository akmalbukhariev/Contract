using ContractAPI.DataAccess;
using ContractAPI.Helper;
using LibContract.HttpModels;
using LibContract.HttpResponse;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ContractAPI.CanceledContractInfo.service.impl
{
    public class CanceledContractService : AppBaseService, ICanceledContractService
    {
        public CanceledContractService(ContractMakerContext db)
            :base(db)
        {
        }

        public async Task<ResponseCanceledContract> getCanceledContract(string phoneNumber)
        {
            ResponseCanceledContract response = new ResponseCanceledContract();
            List<CanceledContract> infoList = await dataBase.CanceledContracts
                .Where(item => item.user_phone_number.Equals(phoneNumber))
                .AsNoTracking()
                .ToListAsync();

            if (infoList == null || infoList.Count == 0)
            {
                response.data = null;
                return response;
            }

            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;

            //foreach (CanceledContract info in infoList)
            //{
            //    response.data.Add(new CanceledContract(info));
            //}

            return response;
        }

        public async Task<ResponseCanceledContract> setCanceledContract(CanceledContract info)
        {
            ResponseCanceledContract response = new ResponseCanceledContract();
            response.data = null;

            CanceledContract newInfo = new CanceledContract();
            newInfo.Copy(info);

            dataBase.CanceledContracts.Add(newInfo);

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

        public async Task<ResponseCanceledContract> deleteCanceledContract(CanceledContract info)
        {
            ResponseCanceledContract response = new ResponseCanceledContract();
            response.data = null;

            CanceledContract found = await dataBase.CanceledContracts
                .Where(item => item.user_phone_number.Equals(info.user_phone_number) && item.created_date.Equals(info.created_date))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (found == null)
            {
                response.data = null;
                response.error_code = (int)HttpStatusCode.NotFound;
                return response;
            }

            dataBase.CanceledContracts.Remove(found);

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
