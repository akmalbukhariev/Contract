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
        {
            dataBase = db;
        }

        public async Task<ResponseLogin> login(Login user)
        {
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
                reg_date = DateTime.Now.ToString("yyyymmdd_hhmmss.fff")
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

            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;

            return response;
        }
    }
}
