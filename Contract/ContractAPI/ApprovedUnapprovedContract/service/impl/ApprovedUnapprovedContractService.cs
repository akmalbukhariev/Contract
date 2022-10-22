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

namespace ContractAPI.ApprovedUnapprovedContract.service.impl
{ 
    public class ApprovedUnapprovedContractService : AppBaseService, IApprovedUnapprovedContractService
    {  
        public ApprovedUnapprovedContractService(ContractMakerContext db)
        {
            dataBase = db;
        }
          
        public async Task<ResponseApprovedUnapprovedContract> setApprovedContract(string contract_number)
        {
            return await updateContract(contract_number, 1);
        }

        public async Task<ResponseApprovedUnapprovedContract> setUnapprovedContract(string contract_number)
        {
            return await updateContract(contract_number, 0);
        }

        /// <summary>
        /// Change if is_approved is 1 or 0
        /// </summary>
        /// <param name="contract_number"></param>
        /// <param name="is_approved">approved: 1; unapproved: 0</param>
        /// <returns></returns>
        async Task<ResponseApprovedUnapprovedContract> updateContract(string contract_number, int is_approved)
        {
            ResponseApprovedUnapprovedContract response = new ResponseApprovedUnapprovedContract();
            response.data = null;

            CreateContractInfo found = await dataBase.CreateContractInfo
                .Where(item => item.contract_number.Equals(contract_number))
                .AsNoTracking()
                .FirstOrDefaultAsync();
            found.is_approved = is_approved;

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
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;

            return response;
        }

        public async Task<ResponseApprovedUnapprovedContract> getApprovedOrUnapprovedContract(Contract.HttpModels.ApprovedUnapprovedContract info)
        {
            ResponseApprovedUnapprovedContract response = new ResponseApprovedUnapprovedContract(); 

            List<CreateContractInfo> found = await dataBase.CreateContractInfo
                .Where(item => item.is_approved == info.is_approved && (item.user_phone_number.Equals(info.user_phone_number) || item.client_stir.Equals(info.user_stir)))
                .AsNoTracking()
                .ToListAsync();

            foreach (CreateContractInfo item in found)
            {
                response.data.Add(new CreateContractInfo(item));
            }
             
            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;

            return response;
        } 
    }
}
