using ContractAPI.DataAccess;
using ContractAPI.Models;
using ContractAPI.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ContractAPI.Users.service.impl
{
    public class UserInfoService : IUserInfoService
    {
        public ContractMakerContext dataBase { get; set; }
          
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

        public async Task<ResponseLogin> updateUserPassword(User user)
        {
            ResponseLogin response = new ResponseLogin();

            User found = await dataBase.Users.Where(item => item.phone_number == user.phone_number)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (found == null)
            {
                response.result = false;
                response.message = Constants.NotFound;
                response.error_code = (int)HttpStatusCode.NotFound;

                return response;
            }

            var newUser = new User();
            newUser.Copy(found);
            newUser.password = user.password;

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
