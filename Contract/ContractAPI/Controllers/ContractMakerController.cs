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
