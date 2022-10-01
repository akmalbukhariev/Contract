using ContractAPI.CompanyInformation.service;
using ContractAPI.DataAccess;
using ContractAPI.Helper;
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
    public class CompanyInfoController : AppBaseController<ICompanyInfoService>
    {
        public CompanyInfoController(ICompanyInfoService service) : base(service)
        {
             
        }

        [HttpGet("getCompanyInfo/{phoneNumber}")]
        public async Task<IActionResult> getCompanyInfo(string phoneNumber)
        {
            ResponseUserCompanyInfo response = await Service.getCompanyInfo(phoneNumber);
            return MakeResponse(response, response.error_code);
        }

        [HttpPost("setCompanyInfo")]
        public async Task<IActionResult> setCompanyInfo([FromBody] CompanyInfo info)
        {
            ResponseUserCompanyInfo response = await Service.setCompanyInfo(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpPut("updateCompanyInfo")]
        public async Task<IActionResult> updateCompanyInfo([FromBody] CompanyInfo info)
        {
            ResponseUserCompanyInfo response = await Service.updateCompanyInfo(info);
            return MakeResponse(response, response.error_code);
        } 
    }
}
