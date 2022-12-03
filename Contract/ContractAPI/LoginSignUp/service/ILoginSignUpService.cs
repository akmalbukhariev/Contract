using ContractAPI.DataAccess;
using LibContract.HttpModels;
using LibContract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.LoginSignUp.service
{
    public interface ILoginSignUpService
    {
        Task<ResponseSignUp> signUp(User user);

        Task<ResponseLogin> login(Login user);
    }
}
