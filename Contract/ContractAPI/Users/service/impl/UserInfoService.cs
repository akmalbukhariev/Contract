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

namespace ContractAPI.Users.service.impl
{
    public class UserInfoService : AppBaseService, IUserInfoService
    {
        public UserInfoService(ContractMakerContext db)
        {
            dataBase = db;
        }

        public async Task<ResponseUser> getUser(string phoneNumber)
        {
            ResponseUser response = new ResponseUser();
            User user = await dataBase.Users.Where(item => item.phone_number == phoneNumber)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (user == null)
            {
                response.data = null;
                return response;
            }

            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.Found;
            response.data.Copy(user);

            return response;
        }

        public async Task<ResponseLogin> updateUserPassword(ChnagePassword user)
        {
            ResponseLogin response = new ResponseLogin();
            User foundUser = await dataBase.Users
                .Where(item => item.phone_number.Equals(user.phone_number))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (foundUser == null)
            {
                response.userInfo = null;
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

            var newUser = new User();
            newUser.Copy(foundUser);
            newUser.password = AesOperation.EncryptString(user.new_password);

            dataBase.Users.Update(newUser);

            try
            {
                await dataBase.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.result = false;
                response.userInfo = null;
                response.message = ex.Message;
                response.error_code = (int)HttpStatusCode.BadRequest;

                return response;
            }

            response.result = true;
            response.userInfo = null;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;

            return response;
        }

        public async Task<IList<User>> getAllUsers()
        {
            return await dataBase.Users.ToListAsync();
        }
    }
}
