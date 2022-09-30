using ContractAPI.CompanyInformation.service;
using ContractAPI.DataAccess;
using ContractAPI.Models;
using ContractAPI.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ContractAPI.CompanyInformation.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyInfoController : ControllerBase
    {
        private ICompanyInfoService _service;
        public CompanyInfoController(ContractMakerContext db, ICompanyInfoService service)
        {
            _service = service;
            _service.dataBase = db;
        }

        [HttpGet("getCompanyInfo/{phoneNumber}")]
        public async Task<IActionResult> getCompanyInfo(string phoneNumber)
        {
            ResponseUserCompanyInfo response = await _service.getCompanyInfo(phoneNumber);
            return MakeResponse(response, response.error_code);
        }

        [HttpPost("setCompanyInfo")]
        public async Task<IActionResult> setCompanyInfo([FromBody] CompanyInfo info)
        {
            ResponseUserCompanyInfo response = await _service.setCompanyInfo(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpPut("updateCompanyInfo")]
        public async Task<IActionResult> updateCompanyInfo([FromBody] CompanyInfo info)
        {
            ResponseUserCompanyInfo response = await _service.updateCompanyInfo(info);
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
