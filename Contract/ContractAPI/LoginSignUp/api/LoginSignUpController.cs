using ContractAPI.DataAccess;
using ContractAPI.LoginSignUp.service;
using ContractAPI.Models;
using ContractAPI.Response;
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
    public class LoginSignUpController : ControllerBase
    {
        private ILoginSignUpService _service;
        public LoginSignUpController(ContractMakerContext db, ILoginSignUpService service)
        {
            _service = service;
            _service.dataBase = db;
        }

        [HttpPost]
        [Route("signUp")]
        public async Task<IActionResult> signUp([FromBody] User user)
        {
            ResponseSignUp response = await _service.signUp(user);
            return MakeResponse(response, response.error_code);
        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] Login user)
        {
            ResponseLogin response = await _service.login(user);
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
