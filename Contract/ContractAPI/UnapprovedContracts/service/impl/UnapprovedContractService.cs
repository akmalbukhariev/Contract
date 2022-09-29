using ContractAPI.Controllers.UnapprovedContracts.service;
using ContractAPI.DataAccess;
using ContractAPI.Models;
using ContractAPI.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ContractAPI.UnapprovedContracts.service.impl
{
    public class UnapprovedContractService : IUnapprovedContractService
    {
        public ContractMakerContext dataBase { get; set ; }

        public async Task<ResponseUnapprovedContract> getUnapprovedContract(string phoneNumber)
        {
            ResponseUnapprovedContract response = new ResponseUnapprovedContract();
            List<UnapprovedContract> infoList = await dataBase.UnapprovedContracts.AsNoTracking()
                .Where(item => item.user_phone_number.Equals(phoneNumber)).ToListAsync();

            if (infoList == null || infoList.Count == 0)
            {
                response.data = null;
                return response;
            }

            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;

            foreach (UnapprovedContract info in infoList)
            {
                response.data.Add(new UnapprovedContract(info));
            }

            return response;
        }

        public async Task<ResponseUnapprovedContract> setUnapprovedContract(UnapprovedContract info)
        {
            ResponseUnapprovedContract response = new ResponseUnapprovedContract();
            response.data = null;

            UnapprovedContract newInfo = new UnapprovedContract();
            newInfo.Copy(info);

            dataBase.UnapprovedContracts.Add(newInfo);

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

        public async Task<ResponseUnapprovedContract> deleteUnapprovedContract(UnapprovedContract info)
        {
            ResponseUnapprovedContract response = new ResponseUnapprovedContract();
            response.data = null;

            UnapprovedContract found = await dataBase.UnapprovedContracts.AsNoTracking()
                .Where(item => item.user_phone_number.Equals(info.user_phone_number) &&
                               item.contract_number.Equals(info.contract_number) &&
                               item.date_of_contract.Equals(info.date_of_contract)).FirstOrDefaultAsync();

            if (found == null)
            {
                response.data = null;
                return response;
            }
            
            dataBase.UnapprovedContracts.Remove(found);

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

        public async Task<ResponseCanceledContract> deleteUnapprovedContractAndSetCanceledContract(CanceledContract info)
        {
            ResponseCanceledContract response = new ResponseCanceledContract();

            UnapprovedContract found = await dataBase.UnapprovedContracts.AsNoTracking()
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

            UnapprovedContract newUnapprovedContract = new UnapprovedContract()
            {
                user_phone_number = info.user_phone_number,
                preparer = info.preparer,
                contract_number = info.contract_number,
                company_contractor_name = info.company_contractor_name,
                date_of_contract = info.date_of_contract,
                contract_price = info.contract_price
            };
            dataBase.UnapprovedContracts.Remove(found);

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
