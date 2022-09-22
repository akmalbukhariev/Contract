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

        [HttpPost("signUp")]
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
                    reg_date = DateTime.Now.ToString("yyyymmdd_hhmmss.SSS")
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

        [HttpGet("getUserCompanyInfo/{phoneNumber}")]
        public async Task<IActionResult> getUserCompanyInfo(string phoneNumber)
        {
            ResponseUserCompanyInfo response = new ResponseUserCompanyInfo();
            CompanyInfo info = await _db.UserCompanyInfo.Where(item => item.user_phone_number.Equals(phoneNumber)).FirstOrDefaultAsync();
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
        public async Task<IActionResult> setUserCompanyInfo([FromBody] CompanyInfo info)
        {
            ResponseUserCompanyInfo response = new ResponseUserCompanyInfo();
            response.data = null;

            CompanyInfo found = await _db.UserCompanyInfo
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

            var newInfo = new CompanyInfo();
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
        public async Task<IActionResult> updateUserCompanyInfo([FromBody] CompanyInfo info)
        {
            ResponseUserCompanyInfo response = new ResponseUserCompanyInfo();
            response.data = null;

            CompanyInfo found = await _db.UserCompanyInfo
                .Where(item => item.user_phone_number.Equals(info.user_phone_number))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (found == null)
            {
                response.result = false;
                response.message = "Not found the user phone number!";

                return BadRequest(response);
            }

            var newInfo = new CompanyInfo();
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
         
        #region UnapprovedContract
        [HttpGet("getUnapprovedContract/{phoneNumber}")]
        public async Task<IActionResult> getUnapprovedContract(string phoneNumber)
        {
            ResponseUnapprovedContract response = new ResponseUnapprovedContract();
            List<UnapprovedContract> infoList = await _db.UnapprovedContracts
                .Where(item => item.user_phone_number.Equals(phoneNumber)).ToListAsync();

            if (infoList == null || infoList.Count == 0)
            {
                response.data = null;
                return NotFound(response);
            }

            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;

            foreach (UnapprovedContract info in infoList)
            {
                response.data.Add(new UnapprovedContract(info));
            }

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

        [HttpDelete("deleteUnapprovedContract")]
        public async Task<IActionResult> deleteUnapprovedContract([FromBody] UnapprovedContract info)
        {
            ResponseUnapprovedContract response = new ResponseUnapprovedContract();
            response.data = null;

            UnapprovedContract found = await _db.UnapprovedContracts
                .Where(item => item.user_phone_number.Equals(info.user_phone_number) &&
                               item.contract_number.Equals(info.contract_number) && 
                               item.date_of_contract.Equals(info.date_of_contract)).FirstOrDefaultAsync();

            if (found == null)
            {
                response.data = null;
                response.error_code = (int)HttpStatusCode.NotFound;
                return NotFound(response);
            }

            try
            {
                _db.UnapprovedContracts.Remove(found);
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

        [HttpDelete("deleteUnapprovedContractAndSetCanceledContract")]
        public async Task<IActionResult> deleteUnapprovedContractAndSetCanceledContract([FromBody] CanceledContract info)
        {
            ResponseCanceledContract response = new ResponseCanceledContract();

            UnapprovedContract found = await _db.UnapprovedContracts
                .Where(item => item.user_phone_number.Equals(info.user_phone_number) &&
                               item.contract_number.Equals(info.contract_number) &&
                               item.date_of_contract.Equals(info.date_of_contract)).FirstOrDefaultAsync();
            if (found == null)
            {
                response.data = null;
                response.error_code = (int)HttpStatusCode.NotFound;
                return NotFound(response);
            }

            try
            {
                CanceledContract newCanceledContract = new CanceledContract();
                newCanceledContract.Copy(info);
                _db.CanceledContracts.Add(newCanceledContract);

                UnapprovedContract newUnapprovedContract = new UnapprovedContract()
                {
                    user_phone_number = info.user_phone_number,
                    preparer = info.preparer,
                    contract_number = info.contract_number,
                    company_contractor_name = info.company_contractor_name,
                    date_of_contract = info.date_of_contract,
                    contract_price = info.contract_price
                };
                _db.UnapprovedContracts.Remove(found);
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
        #endregion
         

        #region ApplicableContract
        [HttpGet("getApplicableContract/{phoneNumber}")]
        public async Task<IActionResult> getApplicableContract(string phoneNumber)
        {
            ResponseApplicableContract response = new ResponseApplicableContract();
            List<ApplicableContract> infoList = await _db.ApplicableContracts
                .Where(item => item.user_phone_number.Equals(phoneNumber)).ToListAsync();

            if (infoList == null || infoList.Count == 0)
            {
                response.data = null;
                return NotFound(response);
            }

            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;

            foreach (ApplicableContract info in infoList)
            {
                response.data.Add(new ApplicableContract(info));
            }

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

        [HttpDelete("deleteApplicableContract")]
        public async Task<IActionResult> deleteApplicableContract([FromBody] ApplicableContract info)
        {
            ResponseApplicableContract response = new ResponseApplicableContract();
            response.data = null;

            ApplicableContract found = await _db.ApplicableContracts
                .Where(item => item.user_phone_number.Equals(info.user_phone_number) &&
                               item.contract_number.Equals(info.contract_number) &&
                               item.date_of_contract.Equals(info.date_of_contract)).FirstOrDefaultAsync();

            if (found == null)
            {
                response.data = null;
                response.error_code = (int)HttpStatusCode.NotFound;
                return NotFound(response);
            }

            try
            {
                _db.ApplicableContracts.Remove(found);
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

        [HttpDelete("deleteApplicableContractAndSetCanceledContract")]
        public async Task<IActionResult> deleteApplicableContractAndSetCanceledContract([FromBody] CanceledContract info)
        {
            ResponseCanceledContract response = new ResponseCanceledContract();

            ApplicableContract found = await _db.ApplicableContracts
                .Where(item => item.user_phone_number.Equals(info.user_phone_number) &&
                               item.contract_number.Equals(info.contract_number) &&
                               item.date_of_contract.Equals(info.date_of_contract)).FirstOrDefaultAsync();
            if (found == null)
            {
                response.data = null;
                response.error_code = (int)HttpStatusCode.NotFound;
                return NotFound(response);
            }

            try
            {
                CanceledContract newCanceledContract = new CanceledContract();
                newCanceledContract.Copy(info);
                _db.CanceledContracts.Add(newCanceledContract);

                ApplicableContract newUnapprovedContract = new ApplicableContract()
                {
                    user_phone_number = info.user_phone_number,
                    preparer = info.preparer,
                    contract_number = info.contract_number,
                    company_contractor_name = info.company_contractor_name,
                    date_of_contract = info.date_of_contract,
                    contract_price = info.contract_price
                };
                _db.ApplicableContracts.Remove(found);
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
        #endregion


        [HttpPost("createContract")]
        public async Task<IActionResult> createContract([FromBody] CreateContract info)
        {
            ResponseContractInfo response = new ResponseContractInfo();
            response.data = null;

            var newInfo = new CreateContract();
            newInfo.Copy(info);
            newInfo.created_date = DateTime.Now.ToString("yyyymmdd_HHmmss_fff");

            try
            {
                _db.CreateContract.Add(newInfo);
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

        [HttpGet("getContractInfo/{phoneNumber}")]
        public async Task<IActionResult> getContractInfo(string phoneNumber)
        {
            ResponseContractInfo response = new ResponseContractInfo();
            List<CreateContract> infoList = await _db.CreateContract.Where(item => item.phone_number.Equals(phoneNumber)).ToListAsync();

            if (infoList == null)
            {
                response.data = null;
                return NotFound(response);
            }

            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;

            foreach (CreateContract info in infoList)
            {
                response.data.Add(new CreateContract(info));
            }

            return Ok(response);
        }

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
