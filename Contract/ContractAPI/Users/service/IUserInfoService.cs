using ContractAPI.DataAccess;
using ContractAPI.Models;
using ContractAPI.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Users.service
{
    public interface IUserInfoService
    {
        ContractMakerContext dataBase { get; set; }
        Task<ResponseUser> getUser(string phoneNumber);
        Task<ResponseLogin> updateUserPassword(User user);
        Task<IList<User>> getAllUsers();
    }
}
