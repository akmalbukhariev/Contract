using ContractAPI.DataAccess;
using ContractAPI.Helper;
using ContractAPI.LoginSignUp.service;
using LibContract.HttpModels;
using LibContract.HttpResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ContractAPI.LoginSignUp.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginSignUpController : AppBaseController<ILoginSignUpService>
    {
        public LoginSignUpController(ILoginSignUpService service) : base(service)
        {
        }

        [HttpPost]
        [Route("signUp")]
        public async Task<IActionResult> signUp([FromBody] User user)
        {
            ResponseSignUp response = await Service.signUp(user);
            return MakeResponse(response, response.error_code);
        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] Login user)
        {
            ResponseLogin response = await Service.login(user);
            return MakeResponse(response, response.error_code);
        } 
    }
}
