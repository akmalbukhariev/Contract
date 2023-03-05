using ContractAPI.ApprovedUnapprovedContract.service.impl;
using ContractAPI.DataAccess;
using ContractAPI.Helper;
using LibContract;
using LibContract.HttpModels;
using LibContract.HttpResponse;
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
            :base(db)
        {
        }

        public async Task<ResponseCreateContract> getNewContractNumber(string phoneNumber)
        {
            ResponseCreateContract response = new ResponseCreateContract();

            CreateContractInfo lastContractNumber = await dataBase.CreateContractInfo
                .Where(item => item.user_phone_number.Equals(phoneNumber))
                .OrderBy(item => item.contract_sequence_number)
                .LastOrDefaultAsync();
              
            response.result = true;
            response.message = Constants.Found;
            response.error_code = (int)HttpStatusCode.OK;

            response.new_contract_sequence_number = lastContractNumber == null ? 
                                           ContractNumberWorker.MakeSequenceNumber("00000") :
                                           ContractNumberWorker.MakeSequenceNumber(lastContractNumber.contract_sequence_number);

            return response;
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

        public async Task<ResponseCreateContract> createContract(CreateContractInfo info)
        {
            ResponseCreateContract response = new ResponseCreateContract();

            CreateContractInfo contractNumber = await dataBase.CreateContractInfo
                .Where(item => item.contract_number.Equals(info.contract_number))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (contractNumber != null)
            {
                response.message = "Contract number is exist.";
                response.error_code = (int)HttpStatusCode.BadRequest;
                return response;
            }

            CompanyInfo clientInfo = await dataBase.UserCompanyInfo
                .Where(item => item.stir_of_company.Equals(info.client_stir))
                .AsNoTracking()
                .FirstOrDefaultAsync();
             
            CreateContractInfo newInfo = new CreateContractInfo(info);
            newInfo.created_date = DateTime.Now.ToString(Constants.TimeFormat);

            if (newInfo.user_phone_number.Equals(info.user_phone_number))
            {
                newInfo.is_approved = 1;
                if (clientInfo == null)
                    newInfo.is_approved_contragent = 1;
                newInfo.contragent_phone_number = clientInfo == null ? "" : clientInfo.user_phone_number;
            }
            else if (newInfo.contragent_phone_number.Equals(info.user_phone_number))
            {
                newInfo.is_approved_contragent = 1;
            }

            dataBase.CreateContractInfo.Add(newInfo);

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

            #region Send Notification
            if (!string.IsNullOrEmpty(newInfo.contragent_phone_number))
            {
                var responseUser = await new Users.service.impl.UserInfoService(dataBase).getUser(newInfo.contragent_phone_number);
                if (responseUser.result && responseUser.data != null && responseUser.data.on_notification == 1)
                {
                    string strLan = responseUser.result ? responseUser.data.lan_id : "";

                    NotificationInfo notData = new NotificationInfo()
                    {
                        title = LanguageConverter.GetNotificationTitle(strLan),
                        message = LanguageConverter.MessageNewContract(strLan),
                        phone_number = newInfo.contragent_phone_number
                    };
                    var responseNotification = await new Notification.service.impl.NotificationService(dataBase).sendNotification(notData);
                }
            }
            #endregion

            response.result = true;
            response.error_code = (int)HttpStatusCode.OK;
            response.message = Constants.Success;
            
            return response;
        }

        public async Task<ResponseCreateContract> deleteContract(string contract_number)
        {
            ResponseCreateContract response = new ResponseCreateContract();

            CreateContractInfo found = await dataBase.CreateContractInfo
                .Where(item => item.contract_number.Equals(contract_number))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (found == null)
            {
                response.message = "Contract number does not exist.";
                response.error_code = (int)HttpStatusCode.BadRequest;
                return response;
            }
             
            dataBase.CreateContractInfo.Remove(found);

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

        public async Task<ResponseCreateContract> cancelContract(CreateContractInfo info)
        {
            ResponseCreateContract response = new ResponseCreateContract();

            CreateContractInfo found = await dataBase.CreateContractInfo
                .Where(item => item.contract_number.Equals(info.contract_number))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (found == null)
            {
                response.message = "Contract number does not exist.";
                response.error_code = (int)HttpStatusCode.BadRequest;
                return response;
            }

            found.comment = info.comment;
            found.is_canceled = 1;
            found.deleted_date = DateTime.Now.ToString(Constants.TimeFormat);

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

            string contrAgentPhoneNumber = "";
            if (found.user_phone_number.Equals(info.user_phone_number))
            {
                contrAgentPhoneNumber = found.contragent_phone_number;
            }
            else if (found.contragent_phone_number.Equals(info.user_phone_number))
            {
                contrAgentPhoneNumber = found.user_phone_number;
            }

            #region Send Notification
            if (!string.IsNullOrEmpty(contrAgentPhoneNumber))
            {
                var responseUser = await new Users.service.impl.UserInfoService(dataBase).getUser(contrAgentPhoneNumber);
                if (responseUser.result && responseUser.data != null && responseUser.data.on_notification == 1)
                {
                    string strLan = responseUser.result ? responseUser.data.lan_id : "";

                    NotificationInfo notData = new NotificationInfo()
                    {
                        title = LanguageConverter.GetNotificationTitle(strLan),
                        message = LanguageConverter.MessageContractCanceled(strLan),
                        phone_number = contrAgentPhoneNumber
                    };
                    var responseNotification = await new Notification.service.impl.NotificationService(dataBase).sendNotification(notData);
                }
            }
            #endregion

            response.result = true;
            response.error_code = (int)HttpStatusCode.OK;
            response.message = Constants.Success;

            return response;
        }

        public async Task<ResponseCanceledContract> getCanceledContract(CreateContractInfo info)
        {
            ResponseCanceledContract response = new ResponseCanceledContract();

            List<CreateContractInfo> found = await dataBase.CreateContractInfo
                .Where(item => (item.is_canceled == 1) && 
                               (item.user_phone_number.Equals(info.user_phone_number) || item.client_stir.Equals(info.user_stir)))
                .AsNoTracking()
                .ToListAsync();

            foreach (CreateContractInfo item in found)
            {
                response.data.Add(new CreateContractInfo(item));
            }
            
            response.result = true;
            response.error_code = (int)HttpStatusCode.OK;
            response.message = Constants.Success;

            return response;
        }
    }
}
