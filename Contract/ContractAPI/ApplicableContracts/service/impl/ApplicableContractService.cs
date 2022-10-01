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

namespace ContractAPI.ApplicableContracts.service.impl
{
    public class ApplicableContractService : AppBaseService, IApplicableContractService
    {  
        public ApplicableContractService(ContractMakerContext db)
        {
            dataBase = db;
        }

        public async Task<ResponseApplicableContract> getApplicableContract(string phoneNumber)
        {
            ResponseApplicableContract response = new ResponseApplicableContract();
            List<ApplicableContract> infoList = await dataBase.ApplicableContracts.AsNoTracking()
                .Where(item => item.user_phone_number.Equals(phoneNumber)).ToListAsync();

            if (infoList == null || infoList.Count == 0)
            {
                response.data = null;
                return response;
            }

            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;

            foreach (ApplicableContract info in infoList)
            {
                response.data.Add(new ApplicableContract(info));
            }

            return response;
        }

        public async Task<ResponseApplicableContract> setApplicableContract(ApplicableContract info)
        {
            ResponseApplicableContract response = new ResponseApplicableContract();
            response.data = null;

            ApplicableContract newInfo = new ApplicableContract();
            newInfo.Copy(info);

            dataBase.ApplicableContracts.Add(newInfo);

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

        public async Task<ResponseApplicableContract> deleteApplicableContract(ApplicableContract info)
        {
            ResponseApplicableContract response = new ResponseApplicableContract();
            response.data = null;

            ApplicableContract found = await dataBase.ApplicableContracts.AsNoTracking()
                .Where(item => item.user_phone_number.Equals(info.user_phone_number) &&
                               item.contract_number.Equals(info.contract_number) &&
                               item.date_of_contract.Equals(info.date_of_contract)).FirstOrDefaultAsync();

            if (found == null)
            {
                response.data = null;
                response.error_code = (int)HttpStatusCode.NotFound;
                return response;
            }

            dataBase.ApplicableContracts.Remove(found);

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

        public async Task<ResponseCanceledContract> deleteApplicableContractAndSetCanceledContract(CanceledContract info)
        {
            ResponseCanceledContract response = new ResponseCanceledContract();

            ApplicableContract found = await dataBase.ApplicableContracts.AsNoTracking()
                .Where(item => item.user_phone_number.Equals(info.user_phone_number) &&
                               item.contract_number.Equals(info.contract_number) &&
                               item.date_of_contract.Equals(info.date_of_contract)).FirstOrDefaultAsync();
            if (found == null)
            {
                response.data = null;
                response.error_code = (int)HttpStatusCode.NotFound;
                return response;
            }

            CanceledContract newCanceledContract = new CanceledContract();
            newCanceledContract.Copy(info);
            dataBase.CanceledContracts.Add(newCanceledContract);

            ApplicableContract newUnapprovedContract = new ApplicableContract()
            {
                user_phone_number = info.user_phone_number,
                preparer = info.preparer,
                contract_number = info.contract_number,
                company_contractor_name = info.company_contractor_name,
                date_of_contract = info.date_of_contract,
                contract_price = info.contract_price
            };
            dataBase.ApplicableContracts.Remove(found);

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
