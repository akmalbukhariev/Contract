using ContractAPI.DataAccess;
using LibContract.HttpModels;
using LibContract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Users.service
{
    public interface IUserInfoService
    {
        Task<ResponseUser> getUser(string phoneNumber);
        Task<ResponseLogin> updateUserPassword(ChnagePassword user);
        Task<ResponseLogin> updateDefaultTemplate(User user);
        Task<ResponseLogin> setNotificationToken(User user);
        Task<IList<User>> getAllUsers();
    }
}
