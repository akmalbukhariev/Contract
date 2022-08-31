using ContractAPI.DataAccess;
using ContractAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractMakerController : ControllerBase
    {
        private ContractMakerContext _db;
        public ContractMakerController(ContractMakerContext db)
        {
            _db = db;
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> signUp([FromBody] User user)
        {
            Response response = new Response();
            try
            {
                var newUser = new User()
                {
                    id = user.id,
                    password = user.password,
                    phone_number = user.phone_number,
                    reg_date = DateTime.Now.ToString("yyyymmdd_hhmmss")
                };

                _db.Users.Add(newUser);
            }
            catch (Exception ex)
            {
                response.result = false;
                response.message = ex.Message;
                return BadRequest(response);
            }

            await _db.SaveChangesAsync();
            return Ok(response);
        }

        [HttpGet("getUser/{phoneNumber}")]
        public async Task<User> getUser(string phoneNumber)
        {
            User user = _db.Users.ToList().Find(item => item.phone_number == phoneNumber);
            if(user == null)
                return NotFound()
        }

        [HttpGet("getAllUsers")]
        public IList<User> getAllUsers()
        {
            return (_db.Users.ToList());
        }
    }
}
