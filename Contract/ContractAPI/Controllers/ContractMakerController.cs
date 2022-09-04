using ContractAPI.DataAccess;
using ContractAPI.Models;
using ContractAPI.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            ResponseSignUp response = new ResponseSignUp();
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
            response.result = true;
            response.message = Constants.Success;

            return Ok(response);
        }

        [HttpPost("login")]
        public IActionResult login([FromBody] Login user)
        {
            ResponseLogin response = new ResponseLogin();
            User foundUser = _db.Users.FirstOrDefault(item => item.phone_number.Equals(user.phone_number) && item.password.Equals(user.password));
            if (foundUser == null)
            {
                response.userInfo = null;
                response.message = Constants.DoNotExist;
                return NotFound(response);
            }

            response.userInfo.Copy(foundUser);
            response.result = true;
            response.message = Constants.Exist;
            response.error_code = (int)HttpStatusCode.OK;

            return Ok(response);
        }

        [HttpGet("getUser/{phoneNumber}")]
        public IActionResult getUser(string phoneNumber)
        {
            ResponseUser response = new ResponseUser();
            User user = _db.Users.ToList().Find(item => item.phone_number == phoneNumber);
            if (user == null)
            {
                response.data = null;
                return NotFound(response);
            }

            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.Found;
            response.data.Copy(user);

            return Ok(response);
        }

        [HttpGet("getUserCompanyInfo/{phoneNumber}")]
        public IActionResult getUserCompanyInfo(string phoneNumber)
        {
            ResponseUserCompanyInfo response = new ResponseUserCompanyInfo();
            UserCompanyInfo info = _db.UserCompanyInfo.ToList().Find(item => item.user_phone_number.Equals(phoneNumber));
            if (info == null)
            {
                response.data = null;
                return NotFound(response);
            }

            response.result = true;
            response.message = Constants.Success;
            response.data.Copy(info);

            return Ok(response);
        }

        [HttpPost("setUserCompanyInfo")]
        public async Task<IActionResult> setUserCompanyInfo([FromBody] UserCompanyInfo info)
        {
            ResponseUserCompanyInfo response = new ResponseUserCompanyInfo();
            response.data = null;

            UserCompanyInfo found = _db.UserCompanyInfo.ToList().Find(item => item.user_phone_number.Equals(info.user_phone_number));

            if (found != null)
            {
                response.result = false;
                response.message = "User phone number already exist!";
                response.error_code = (int)HttpStatusCode.BadRequest;
                return BadRequest(response);    
            }

            var newInfo = new UserCompanyInfo();
            newInfo.Copy(info);

            try
            {
                _db.UserCompanyInfo.Add(newInfo);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.error_code = (int)HttpStatusCode.BadRequest;
                return BadRequest(response);
            }

            await _db.SaveChangesAsync();
            response.result = true;
            response.error_code = (int)HttpStatusCode.OK;
            response.message = Constants.Success;

            return Ok(response);
        }

        [HttpPut("updateUserCompanyInfo")]
        public async Task<IActionResult> updateUserCompanyInfo([FromBody] UserCompanyInfo info)
        {
            ResponseUserCompanyInfo response = new ResponseUserCompanyInfo();
            response.data = null;

            UserCompanyInfo found = _db.UserCompanyInfo.ToList().Find(item => item.user_phone_number.Equals(info.user_phone_number));
            if (found == null)
            {
                response.result = false;
                response.message = "Not found the user phone number!";

                return BadRequest(response);
            }

            var newInfo = new UserCompanyInfo();
            newInfo.Copy(info);

            try
            {
                _db.Update(newInfo);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.error_code = (int)HttpStatusCode.BadRequest;

                return BadRequest(response);
            }

            await _db.SaveChangesAsync();
            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;

            return Ok(response);
        }

        [HttpGet("setPurposeOfCompany/{phoneNumber}")]
        public IActionResult getPurposeOfCompany(string phoneNumber)
        {
            ResponsePurposeOfContract response = new ResponsePurposeOfContract();
            PurposeOfContract info = _db.PurposeOfContracts.ToList().Find(item => item.user_phone_number.Equals(phoneNumber));
            if (info == null)
            {
                response.data = null;
                return NotFound(response);
            }

            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;
            response.data.Copy(info);

            return Ok(response);
        }

        [HttpPost("setPurposeOfCompany")]
        public async Task<IActionResult> setPurposeOfCompany([FromBody] PurposeOfContract info)
        {
            ResponsePurposeOfContract response = new ResponsePurposeOfContract();
            response.data = null;

            PurposeOfContract found = _db.PurposeOfContracts.ToList().Find(item => item.user_phone_number.Equals(info.user_phone_number));

            if (found != null)
            {
                response.result = false;
                response.message = "User phone number already exist!";
                response.error_code = (int)HttpStatusCode.BadRequest;
                return BadRequest(response);
            }

            var newInfo = new PurposeOfContract();
            newInfo.Copy(info);

            try
            {
                _db.PurposeOfContracts.Add(newInfo);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.error_code = (int)HttpStatusCode.BadRequest;
                return BadRequest(response);
            }

            await _db.SaveChangesAsync();
            response.result = true;
            response.error_code = (int)HttpStatusCode.OK;
            response.message = Constants.Success;

            return Ok(response);
        }

        [HttpGet("getAllUsers")]
        public IList<User> getAllUsers()
        {
            return (_db.Users.ToList());
        }

        [HttpGet("getUnapprovedContract/{phoneNumber}")]
        public IActionResult getUnapprovedContract(string phoneNumber)
        {
            ResponseUnapprovedContract response = new ResponseUnapprovedContract();
            UnapprovedContract info = _db.UnapprovedContracts.ToList().Find(item => item.user_phone_number.Equals(phoneNumber));
            if (info == null)
            {
                response.data = null;
                return NotFound(response);
            }

            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;
            response.data.Copy(info);

            return Ok(response);
        }

        [HttpPost("setUnapprovedContract")]
        public async Task<IActionResult> setUnapprovedContract([FromBody] UnapprovedContract info)
        {
            ResponseUnapprovedContract response = new ResponseUnapprovedContract();
            response.data = null;

            try
            {
                UnapprovedContract newInfo = new UnapprovedContract();
                newInfo.Copy(info);

                _db.UnapprovedContracts.Add(newInfo);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.error_code = (int)HttpStatusCode.BadRequest;

                return BadRequest(response);
            }

            await _db.SaveChangesAsync();
            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;

            return Ok(response);
        }

        [HttpGet("getApplicableContract/{phoneNumber}")]
        public IActionResult getApplicableContract(string phoneNumber)
        {
            ResponseApplicableContract response = new ResponseApplicableContract();
            ApplicableContract info = _db.ApplicableContracts.ToList().Find(item => item.user_phone_number.Equals(phoneNumber));
            if (info == null)
            {
                response.data = null;
                return NotFound(response);
            }

            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;
            response.data.Copy(info);

            return Ok(response);
        }

        [HttpPost("setApplicableContract")]
        public async Task<IActionResult> setApplicableContract([FromBody] ApplicableContract info)
        {
            ResponseApplicableContract response = new ResponseApplicableContract();
            response.data = null;

            try
            {
                ApplicableContract newInfo = new ApplicableContract();
                newInfo.Copy(info);

                _db.ApplicableContracts.Add(newInfo);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.error_code = (int)HttpStatusCode.BadRequest;

                return BadRequest(response);
            }

            await _db.SaveChangesAsync();
            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;

            return Ok(response);
        }
         
        [HttpGet("getCanceledContract/{phoneNumber}")]
        public IActionResult getCanceledContract(string phoneNumber)
        {
            ResponseCanceledContract response = new ResponseCanceledContract();
            CanceledContract info = _db.CanceledContracts.ToList().Find(item => item.user_phone_number.Equals(phoneNumber));
            if (info == null)
            {
                response.data = null;
                return NotFound(response);
            }

            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;
            response.data.Copy(info);

            return Ok(response);
        }

        [HttpPost("setCanceledContract")]
        public async Task<IActionResult> setCanceledContract([FromBody] CanceledContract info)
        {
            ResponseCanceledContract response = new ResponseCanceledContract();
            response.data = null;

            try
            {
                CanceledContract newInfo = new CanceledContract();
                newInfo.Copy(info);

                _db.CanceledContracts.Add(newInfo);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.error_code = (int)HttpStatusCode.BadRequest;

                return BadRequest(response);
            }

            await _db.SaveChangesAsync();
            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;

            return Ok(response);
        }

    }
}
