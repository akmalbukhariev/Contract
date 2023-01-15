using ContractAPI.DataAccess;
using ContractAPI.Helper;
using LibContract.HttpModels;
using LibContract.HttpResponse;
using ContractAPI.Users.service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ContractAPI.Users.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : AppBaseController<IUserInfoService>
    { 
        public UserInfoController(IUserInfoService service) : base(service)
        { 
        }

        [HttpGet("getUser/{phoneNumber}")]
        public async Task<IActionResult> getUser(string phoneNumber)
        {
            ResponseUser response = await Service.getUser(phoneNumber);
            return MakeResponse(response, response.error_code);
        }

        [HttpPut("updateUserPassword")]
        public async Task<IActionResult> updateUserPassword([FromBody] ChnagePassword user)
        {
            ResponseLogin response = await Service.updateUserPassword(user);
            return MakeResponse(response, response.error_code);
        }

        [HttpPut("updateDefaultTemplate")]
        public async Task<IActionResult> updateDefaultTemplate([FromBody] User user)
        {
            ResponseLogin response = await Service.updateDefaultTemplate(user);
            return MakeResponse(response, response.error_code);
        }

        [HttpPut("setNotificationToken")]
        public async Task<IActionResult> setNotificationToken([FromBody] User user)
        {
            ResponseLogin response = await Service.setNotificationToken(user);
            return MakeResponse(response, response.error_code);
        }
    }
}
