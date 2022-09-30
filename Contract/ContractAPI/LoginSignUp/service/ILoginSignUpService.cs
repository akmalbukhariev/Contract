using ContractAPI.DataAccess;
using ContractAPI.Models;
using ContractAPI.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.LoginSignUp.service
{
    public interface ILoginSignUpService
    {
        ContractMakerContext dataBase { get; set; }
        Task<ResponseSignUp> signUp(User user);

        Task<ResponseLogin> login(Login user);
    }
}
