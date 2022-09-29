using ContractAPI.DataAccess;
using ContractAPI.Models;
using ContractAPI.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpPost]
        [Route("signUp")]
        public async Task<IActionResult> signUp([FromBody] User user)
        {
            ResponseSignUp response = new ResponseSignUp();
            User foundUser = _db.Users.Where(item => item.phone_number.Equals(user.phone_number)).AsNoTracking().FirstOrDefault();
            if (foundUser != null)
            {
                response.error_code = (int)HttpStatusCode.Found;
                response.message = Constants.Exist;
                return BadRequest(response);
            }

            try
            {
                var newUser = new User()
                {
                    password = AesOperation.EncryptString(user.password),
                    phone_number = user.phone_number,
                    reg_date = DateTime.Now.ToString("yyyymmdd_hhmmss.fff")
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
            response.error_code = (int)HttpStatusCode.OK;

            return Ok(response);
        }

        [HttpPost("login")]
        public IActionResult login([FromBody] Login user)
        {
            ResponseLogin response = new ResponseLogin();
            User foundUser = _db.Users.FirstOrDefault(item => item.phone_number.Equals(user.phone_number));
            if (foundUser == null)
            {
                response.userInfo = null;
                response.message = Constants.DoNotExist;
                return NotFound(response);
            }

            string decryptPassword = AesOperation.DecryptString(foundUser.password);
            if (!user.password.Equals(decryptPassword))
            {
                response.result = false;
                response.message = "Wrong password.";
                response.error_code = (int)HttpStatusCode.BadRequest;
                return BadRequest(response);
            }

            response.userInfo.Copy(foundUser);
            response.result = true;
            response.message = Constants.Exist;
            response.error_code = (int)HttpStatusCode.OK;

            return Ok(response);
        }

        [HttpGet("getUser/{phoneNumber}")]
        public async Task<IActionResult> getUser(string phoneNumber)
        {
            ResponseUser response = new ResponseUser();
            User user = await _db.Users.Where(item => item.phone_number == phoneNumber).FirstOrDefaultAsync();
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

        //[HttpGet("getUserCompanyInfo/{phoneNumber}")]
        //public async Task<IActionResult> getUserCompanyInfo(string phoneNumber)
        //{
        //    ResponseUserCompanyInfo response = new ResponseUserCompanyInfo();
        //    CompanyInfo info = await _db.UserCompanyInfo.Where(item => item.user_phone_number.Equals(phoneNumber)).FirstOrDefaultAsync();
        //    if (info == null)
        //    {
        //        response.data = null;
        //        return NotFound(response);
        //    }

        //    response.result = true;
        //    response.message = Constants.Success;
        //    response.data.Copy(info);

        //    return Ok(response);
        //}

        //[HttpPost("setUserCompanyInfo")]
        //public async Task<IActionResult> setUserCompanyInfo([FromBody] CompanyInfo info)
        //{
        //    ResponseUserCompanyInfo response = new ResponseUserCompanyInfo();
        //    response.data = null;

        //    CompanyInfo found = await _db.UserCompanyInfo
        //        .Where(item => item.user_phone_number.Equals(info.user_phone_number))
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync();

        //    if (found != null)
        //    {
        //        response.result = false;
        //        response.message = "User phone number already exist!";
        //        response.error_code = (int)HttpStatusCode.BadRequest;
        //        return BadRequest(response);    
        //    }

        //    var newInfo = new CompanyInfo();
        //    newInfo.Copy(info);

        //    try
        //    {
        //        _db.UserCompanyInfo.Add(newInfo);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.message = ex.Message;
        //        response.error_code = (int)HttpStatusCode.BadRequest;
        //        return BadRequest(response);
        //    }

        //    await _db.SaveChangesAsync();
        //    response.result = true;
        //    response.error_code = (int)HttpStatusCode.OK;
        //    response.message = Constants.Success;

        //    return Ok(response);
        //}

        //[HttpPut("updateUserCompanyInfo")]
        //public async Task<IActionResult> updateUserCompanyInfo([FromBody] CompanyInfo info)
        //{
        //    ResponseUserCompanyInfo response = new ResponseUserCompanyInfo();
        //    response.data = null;

        //    CompanyInfo found = await _db.UserCompanyInfo
        //        .Where(item => item.user_phone_number.Equals(info.user_phone_number))
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync();

        //    if (found == null)
        //    {
        //        response.result = false;
        //        response.message = "Not found the user phone number!";

        //        return BadRequest(response);
        //    }

        //    var newInfo = new CompanyInfo();
        //    newInfo.Copy(info);

        //    try
        //    {
        //        _db.Update(newInfo);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.message = ex.Message;
        //        response.error_code = (int)HttpStatusCode.BadRequest;

        //        return BadRequest(response);
        //    }

        //    await _db.SaveChangesAsync();
        //    response.result = true;
        //    response.message = Constants.Success;
        //    response.error_code = (int)HttpStatusCode.OK;

        //    return Ok(response);
        //}

        [HttpPut("updateUserPassword")]
        public async Task<IActionResult> updateUserPassword([FromBody] User user)
        {
            ResponseLogin response = new ResponseLogin();

            User found = await _db.Users.Where(item => item.phone_number == user.phone_number)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (found == null)
            {
                response.result = false;
                response.message = Constants.NotFound;
                response.error_code = (int)HttpStatusCode.NotFound;

                return NotFound(response);
            }

            var newUser = new User();
            newUser.Copy(found);
            newUser.password = user.password;

            try
            {
                _db.Users.Update(newUser);
            }
            catch (Exception ex)
            {
                response.result = false;
                response.userInfo = null;
                response.message = ex.Message;
                response.error_code = (int)HttpStatusCode.BadRequest;

                return BadRequest(response);
            }

            await _db.SaveChangesAsync();
            response.result = true;
            response.userInfo = null;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;

            return Ok(response);
        }

        [HttpGet("getPurposeOfCompany/{phoneNumber}")]
        public async Task<IActionResult> getPurposeOfCompany(string phoneNumber)
        {
            ResponsePurposeOfContract response = new ResponsePurposeOfContract();
            PurposeOfContract info = await _db.PurposeOfContracts
                .Where(item => item.user_phone_number.Equals(phoneNumber))
                .FirstOrDefaultAsync();

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

            PurposeOfContract found = await _db.PurposeOfContracts
                .Where(item => item.user_phone_number.Equals(info.user_phone_number))
                .AsNoTracking()
                .FirstOrDefaultAsync();

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
         
        [HttpPost("createContract")]
        public async Task<IActionResult> createContract([FromBody] CreateContract info)
        {   
            ResponseCreateContract response = new ResponseCreateContract();

            CompanyInfo newInfo1 = new CompanyInfo(info.client_company_info);
            newInfo1.created_date = DateTime.Now.ToString("yyyymmdd_hhmmss.fff");
             
            CreateContractInfo newInfo2 = new CreateContractInfo(info.contract_info);
            newInfo2.created_date = DateTime.Now.ToString("yyyymmdd_hhmmss.fff");

            ContractMakerContext contect1 = _db.CreateNew();
            ContractMakerContext contect2 = _db.CreateNew();

            contect1.CompanyInfo.Add(newInfo1);
            contect2.CreateContractInfo.Add(newInfo2);

            foreach (ServicesInfo item in info.service_list)
            {
                ServicesInfo newInfo3 = new ServicesInfo(item);
                newInfo3.created_date = DateTime.Now.ToString("yyyymmdd_hhmmss.fff");

                _db.ServicesInfo.Add(newInfo3);
                await _db.SaveChangesAsync();
            }

            try
            {
                await contect1.SaveChangesAsync();
                await contect2.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.error_code = (int)HttpStatusCode.BadRequest;
                return BadRequest(response);
            }
             
            response.result = true;
            response.error_code = (int)HttpStatusCode.OK;
            response.message = Constants.Success;

            return Ok(response);
        }    

        //[HttpGet("getContractInfo/{phoneNumber}")]
        //public async Task<IActionResult> getContractInfo(string phoneNumber)
        //{
        //    ResponseContractInfo response = new ResponseContractInfo();
        //    List<CreateContract> infoList = await _db.CreateContractInfo.Where(item => item.phone_number.Equals(phoneNumber)).ToListAsync();

        //    if (infoList == null)
        //    {
        //        response.data = null;
        //        return NotFound(response);
        //    }

        //    response.result = true;
        //    response.message = Constants.Success;
        //    response.error_code = (int)HttpStatusCode.OK;

        //    foreach (CreateContract info in infoList)
        //    {
        //        response.data.Add(new CreateContract(info));
        //    }

        //    return Ok(response);
        //}

        [HttpGet("getCanceledContract/{phoneNumber}")]
        public async Task<IActionResult> getCanceledContract(string phoneNumber)
        {
            ResponseCanceledContract response = new ResponseCanceledContract();
            List<CanceledContract> infoList = await _db.CanceledContracts
                .Where(item => item.user_phone_number.Equals(phoneNumber)).ToListAsync();

            if (infoList == null || infoList.Count == 0)
            {
                response.data = null;
                return NotFound(response);
            }

            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;

            foreach(CanceledContract info in infoList)
            {
                response.data.Add(new CanceledContract(info));
            }

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

        [HttpDelete("deleteCanceledContract")]
        public async Task<IActionResult> deleteCanceledContract([FromBody] CanceledContract info)
        {
            ResponseCanceledContract response = new ResponseCanceledContract();
            response.data = null;

            CanceledContract found = await _db.CanceledContracts
                .Where(item => item.user_phone_number.Equals(info.user_phone_number) && item.date_of_contract.Equals(info.date_of_contract)).FirstOrDefaultAsync();

            if (found == null)
            {
                response.data = null;
                response.error_code = (int)HttpStatusCode.NotFound;
                return NotFound(response);
            }

            try
            {
                _db.CanceledContracts.Remove(found);
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

        [HttpGet("getAboutApp/{lan_code}")]
        public async Task<IActionResult> getAboutApp(string lan_code)
        {
            ResponseAboutApp response = new ResponseAboutApp();
            response.data = null;
            AboutApp found = await _db.AboutApp.Where(item => item.lan_code.Equals(lan_code)).FirstOrDefaultAsync();

            if (found == null)
            {
                response.result = false;
                response.message = Constants.NotFound;
                response.error_code = (int)HttpStatusCode.NotFound;

                return NotFound(response);
            }

            response.data = new AboutApp();
            response.data.Copy(found);

            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;

            return Ok(response);
        }
    }
}
