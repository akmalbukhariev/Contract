using ContractAPI.DataAccess;
using ContractAPI.Models;
using ContractAPI.Response;
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
    public class UserInfoServiceController : ControllerBase
    {
        private IUserInfoService _service;
        public UserInfoServiceController(ContractMakerContext db, IUserInfoService service)
        {
            _service = service;
            _service.dataBase = db;
        }

        [HttpGet("getUser/{phoneNumber}")]
        public async Task<IActionResult> getUser(string phoneNumber)
        {
            ResponseUser response = await _service.getUser(phoneNumber);
            return MakeResponse(response, response.error_code);
        }

        [HttpPut("updateUserPassword")]
        public async Task<IActionResult> updateUserPassword([FromBody] User user)
        {
            ResponseLogin response = await _service.updateUserPassword(user);
            return MakeResponse(response, response.error_code);
        }

        private IActionResult MakeResponse(object obj, int error_code)
        {
            switch (error_code)
            {
                case (int)HttpStatusCode.NotFound: return NotFound(obj);
                case (int)HttpStatusCode.BadRequest: return BadRequest(obj);
            }

            return Ok(obj);
        }
    }
}
