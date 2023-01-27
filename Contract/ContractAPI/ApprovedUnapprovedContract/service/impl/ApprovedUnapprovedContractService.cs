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

namespace ContractAPI.ApprovedUnapprovedContract.service.impl
{ 
    public class ApprovedUnapprovedContractService : AppBaseService, IApprovedUnapprovedContractService
    {  
        public ApprovedUnapprovedContractService(ContractMakerContext db)
        {
            dataBase = db;
        }
          
        public async Task<ResponseApprovedUnapprovedContract> setApprovedContract(LibContract.HttpModels.ApprovedUnapprovedContract info)
        {
            ResponseApprovedUnapprovedContract response = new ResponseApprovedUnapprovedContract();
            response.data = null;

            CreateContractInfo found = await dataBase.CreateContractInfo
                .Where(item => item.contract_number.Equals(info.contract_number))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (found == null)
            {
                return response;
            }

            CompanyInfo clientInfo = await dataBase.UserCompanyInfo
                .Where(item => item.stir_of_company.Equals(info.client_stir))
                .AsNoTracking()
                .FirstOrDefaultAsync();
             
            if (found.user_phone_number.Equals(info.user_phone_number))
            {
                found.is_approved = 1;
                if (clientInfo == null)
                    found.is_approved_contragent = 1;
                found.contragent_phone_number = clientInfo == null ? "" : clientInfo.user_phone_number;
            }
            else if (found.contragent_phone_number.Equals(info.user_phone_number))
            {
                found.is_approved_contragent = 1;
            }

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

            response.contragent_phone_number = info.user_phone_number.Equals(found.contragent_phone_number) ? found.user_phone_number : found.contragent_phone_number;

            #region Send Notification
            if (!string.IsNullOrEmpty(response.contragent_phone_number))
            {
                var responseUser = await new Users.service.impl.UserInfoService(dataBase).getUser(response.contragent_phone_number);
                if (responseUser.result && responseUser.data != null && responseUser.data.on_notification == 1)
                {
                    string strLan = responseUser.result ? responseUser.data.lan_id : "";

                    NotificationInfo notData = new NotificationInfo()
                    {
                        title = LanguageConverter.GetNotificationTitle(strLan),
                        message = LanguageConverter.MessageContractApproved(strLan),
                        phone_number = response.contragent_phone_number
                    };
                    var responseNotification = await new Notification.service.impl.NotificationService(dataBase).sendNotification(notData);
                }
            }
            #endregion

            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;

            return response; 
        }

        public async Task<ResponseApprovedUnapprovedContract> setUnapprovedContract(LibContract.HttpModels.ApprovedUnapprovedContract info)
        {
            return await updateContract(info.contract_number, info.contragent_phone_number, 0);
        }

        /// <summary>
        /// Change if is_approved is 1 or 0
        /// </summary>
        /// <param name="contract_number"></param>
        /// <param name="is_approved">approved: 1; unapproved: 0</param>
        /// <returns></returns>
        async Task<ResponseApprovedUnapprovedContract> updateContract(string contract_number, string contragent_phone_number,  int is_approved)
        {
            ResponseApprovedUnapprovedContract response = new ResponseApprovedUnapprovedContract();
            response.data = null;

            CreateContractInfo found = await dataBase.CreateContractInfo
                .Where(item => item.contract_number.Equals(contract_number))
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (found == null)
            { 
                return response;
            }

            found.contragent_phone_number = contragent_phone_number;
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

        public async Task<ResponseApprovedUnapprovedContract> getUnapprovedContract(LibContract.HttpModels.ApprovedUnapprovedContract info)
        {
            ResponseApprovedUnapprovedContract response = new ResponseApprovedUnapprovedContract();

            List<CreateContractInfo> found = new List<CreateContractInfo>();
             
            found = await dataBase.CreateContractInfo
                .Where(item => ((item.is_approved == 0) || (item.is_approved_contragent == 0)) &&
                               (item.user_phone_number.Equals(info.user_phone_number) ||
                                item.contragent_phone_number.Equals(info.user_phone_number)) &&
                               (item.is_canceled == 0))
                .AsNoTracking()
                .ToListAsync();
             
            foreach (CreateContractInfo item in found)
            {
                response.data.Add(new CreateContractInfo(item));
            }

            //found.Clear();
            //found = await dataBase.CreateContractInfo
            //    .Where(item => ((item.is_approved == 0) || (item.is_approved_contragent == 0)) &&
            //                   item.contragent_phone_number.Equals(info.user_phone_number) &&
            //                   (item.is_canceled == 0))
            //    .AsNoTracking()
            //    .ToListAsync();
            //
            //foreach (CreateContractInfo item in found)
            //{
            //    response.data.Add(new CreateContractInfo(item));
            //}

            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;

            return response;
        }

        public async Task<ResponseApprovedUnapprovedContract> getApprovedContract(LibContract.HttpModels.ApprovedUnapprovedContract info)
        {
            ResponseApprovedUnapprovedContract response = new ResponseApprovedUnapprovedContract();

            List<CreateContractInfo> found = new List<CreateContractInfo>();

            found = await dataBase.CreateContractInfo
               .Where(item => (item.is_approved == 1) && (item.is_approved_contragent == 1) &&
                              (item.user_phone_number.Equals(info.user_phone_number) ||
                                item.contragent_phone_number.Equals(info.user_phone_number)) &&
                              (item.is_canceled == 0))
               .AsNoTracking()
               .ToListAsync();

            foreach (CreateContractInfo item in found)
            {
                response.data.Add(new CreateContractInfo(item));
            }

            //found.Clear();
            //found = await dataBase.CreateContractInfo
            //    .Where(item => (item.is_approved == 1) && (item.is_approved_contragent == 1) &&
            //                   item.contragent_phone_number.Equals(info.user_phone_number) &&
            //                   (item.is_canceled == 0))
            //    .AsNoTracking()
            //    .ToListAsync();
            //
            //foreach (CreateContractInfo item in found)
            //{
            //    response.data.Add(new CreateContractInfo(item));
            //}

            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;

            return response;
        }

        public async Task<ResponseApprovedUnapprovedContract> getApprovedAndUnapprovedContract(LibContract.HttpModels.ApprovedUnapprovedContract info)
        {
            ResponseApprovedUnapprovedContract response = new ResponseApprovedUnapprovedContract();

            List<CreateContractInfo> found = new List<CreateContractInfo>();
             
            found = await dataBase.CreateContractInfo
                .Where(item => (item.user_phone_number.Equals(info.user_phone_number) || item.contragent_phone_number.Equals(info.user_phone_number)) &&
                               (item.is_canceled == 0))
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
