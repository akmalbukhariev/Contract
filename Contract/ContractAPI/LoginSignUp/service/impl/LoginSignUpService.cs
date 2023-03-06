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

namespace ContractAPI.LoginSignUp.service.impl
{
    public class LoginSignUpService : AppBaseService, ILoginSignUpService
    {
        public LoginSignUpService(ContractMakerContext db)
            :base(db)
        {
        }

        public async Task<ResponseLogin> login(Login user)
        {
            /*MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=65.20.68.60;uid=contract;pwd=Aa!0532531989;database=contractmaker";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                 
            }*/

            ResponseLogin response = new ResponseLogin();
            //List<User> tList = await dataBase.Users.ToListAsync();

            User foundUser = await dataBase.Users
                .Where(item => item.phone_number.Equals(user.phone_number))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (foundUser == null)
            {
                response.data = null;
                response.message = Constants.DoNotExist;
                return response;
            }

            string decryptPassword = AesOperation.DecryptString(foundUser.password);
            if (!user.password.Equals(decryptPassword))
            {
                response.result = false;
                response.message = "Wrong password.";
                response.error_code = (int)HttpStatusCode.BadRequest;
                return response;
            }
                             
            UserCompanyInfo companyInfo = await dataBase.UserCompanyInfo
                            .Where(item => item.user_phone_number.Equals(user.phone_number))
                            .AsNoTracking()
                            .FirstOrDefaultAsync();
            
            if (companyInfo != null)
            {
                response.companyInfo = new UserCompanyInfo(companyInfo);
            }
             
            response.data.Copy(foundUser);
            response.result = true;
            response.message = Constants.Exist;
            response.error_code = (int)HttpStatusCode.OK;

            return response;
        }

        public async Task<ResponseSignUp> signUp(User user)
        {
            ResponseSignUp response = new ResponseSignUp();
            User foundUser = dataBase.Users.Where(item => item.phone_number.Equals(user.phone_number))
                .AsNoTracking()
                .FirstOrDefault();

            if (foundUser != null)
            {
                response.error_code = (int)HttpStatusCode.Found;
                response.message = Constants.Exist;
                return response;
            }

            var newUser = new User()
            {
                password = AesOperation.EncryptString(user.password),
                phone_number = user.phone_number,
                lan_id = user.lan_id,
                on_notification = 1,
                reg_date = user.reg_date
            };

            dataBase.Users.Add(newUser);

            try
            {
                await dataBase.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.result = false;
                response.message = ex.Message;
                return response;
            }

            ContractNumberTemplate numberTemplate = new ContractNumberTemplate()
            {
                user_phone_number = newUser.phone_number,
                option = "",
                format = 1,
                created_date = newUser.reg_date
            };

            var numberService = new ContractNumberInfo.service.impl.ContractNumberService(dataBase);
            var responseNumberService = await numberService.setContractNumber(numberTemplate);

            if (responseNumberService.result)
            {
                var responseNumberList = await numberService.getContractNumber(newUser.phone_number);
                int formatId = 1;
                
                if (responseNumberList.result)
                {
                    formatId = responseNumberList.data[0].id;
                }
                var contractService = new ContractTemplateInfo.service.impl.ContractTemplateService(dataBase);

                var clausesList = await contractService.getAllReadyTemplate();
                string clauseNormal = "";
                string clausePopular = "";
                string clauseSimple = "";
                if (clausesList.result)
                {
                    clauseNormal = clausesList.data.Where(item => item.template_name_en.Equals("Normal")).FirstOrDefault().clauses;
                    clausePopular = clausesList.data.Where(item => item.template_name_en.Equals("Popular")).FirstOrDefault().clauses;
                    clauseSimple = clausesList.data.Where(item => item.template_name_en.Equals("Simple")).FirstOrDefault().clauses;
                }
                ContractTemplate contractTemplateNormal = new ContractTemplate()
                {
                    user_phone_number = newUser.phone_number,
                    contract_number_format_id = formatId,
                    contract_number_option = "",
                    contract_address = "",
                    template_name = "Ўртача",
                    clauses = clauseNormal,
                    created_date = newUser.reg_date
                };
                ContractTemplate contractTemplatePopular = new ContractTemplate()
                {
                    user_phone_number = newUser.phone_number,
                    contract_number_format_id = formatId,
                    contract_number_option = "",
                    contract_address = "",
                    template_name = "Оммабоп",
                    clauses = clausePopular,
                    created_date = newUser.reg_date
                };
                ContractTemplate contractTemplateSimple = new ContractTemplate()
                {
                    user_phone_number = newUser.phone_number,
                    contract_number_format_id = formatId,
                    contract_number_option = "",
                    contract_address = "",
                    template_name = "Оддий",
                    clauses = clauseSimple,
                    created_date = newUser.reg_date
                };

                await contractService.setContractTemplate(contractTemplateNormal);
                await contractService.setContractTemplate(contractTemplatePopular);
                await contractService.setContractTemplate(contractTemplateSimple);
            }

            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;

            return response;
        }
    }
}
