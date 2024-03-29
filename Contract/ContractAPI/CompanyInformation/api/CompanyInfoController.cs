﻿using ContractAPI.CompanyInformation.service;
using ContractAPI.DataAccess;
using ContractAPI.Helper;
using LibContract.HttpModels;
using LibContract.HttpResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ContractAPI.Models;

namespace ContractAPI.CompanyInformation.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyInfoController : AppBaseController<ICompanyInfoService>
    {
        public CompanyInfoController(ICompanyInfoService service) : base(service)
        {
             
        }

        [HttpGet("getUserCompanyInfo/{phoneNumber}")]
        public async Task<IActionResult> getUserCompanyInfo(string phoneNumber)
        {
            ResponseUserCompanyInfo response = await Service.getUserCompanyInfo(phoneNumber);
            return MakeResponse(response, response.error_code);
        }

        [HttpGet("getClientCompanyInfo/{phoneNumber}")]
        public async Task<IActionResult> getClientCompanyInfo(string phoneNumber)
        {
            ResponseClientCompanyInfo response = await Service.getClientCompanyInfo(phoneNumber);
            return MakeResponse(response, response.error_code);
        }

        [HttpGet("getUserCompanyInfoToCreateContract/{stirNumber}")]
        public async Task<IActionResult> getUserCompanyInfoToCreateContract(string stirNumber)
        {
            ResponseUserCompanyInfo response = await Service.getUserCompanyInfoToCreateContract(stirNumber);
            return MakeResponse(response, response.error_code);
        }


        [HttpDelete("deleteUserCompanyInfo")]
        public async Task<IActionResult> deleteUserCompanyInfo([FromBody] DeleteCompanyInfo info)
        {
            ResponseUserCompanyInfo response = await Service.deleteUserCompanyInfo(info);
            return MakeResponse(response, response.error_code);
        }
        
        [HttpDelete("deleteClientCompanyInfo")]
        public async Task<IActionResult> deleteClientCompanyInfo([FromBody] DeleteCompanyInfo info)
        {
            ResponseClientCompanyInfo response = await Service.deleteClientCompanyInfo(info);
            return MakeResponse(response, response.error_code);
        }


        [HttpPost("setUserCompanyInfo")]
        public async Task<IActionResult> setUserCompanyInfo([FromBody] CompanyInfo info)
        {
            ResponseUserCompanyInfo response = await Service.setUserCompanyInfo(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpPost("setUserCompanyInfoWithFile")]
        public async Task<IActionResult> setUserCompanyInfoWithFile([FromForm] CompanyInfoWithFile info)
        {
            ResponseUserCompanyInfo response = await Service.setUserCompanyInfoWithFile(info);
            return MakeResponse(response, response.error_code);
        }


        [HttpPost("setClientCompanyInfoToCreateContract")]
        public async Task<IActionResult> setClientCompanyInfoToCreateContract([FromBody] CompanyInfo info)
        {
            ResponseClientCompanyInfo response = await Service.setClientCompanyInfoToCreateContract(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpPost("setClientCompanyInfo")]
        public async Task<IActionResult> setClientCompanyInfo([FromBody] CompanyInfo info)
        {
            ResponseClientCompanyInfo response = await Service.setClientCompanyInfo(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpPost("setClientCompanyInfoWithFile")]
        public async Task<IActionResult> setClientCompanyInfoWithFile([FromForm] CompanyInfoWithFile info)
        {
            ResponseClientCompanyInfo response = await Service.setClientCompanyInfoWithFile(info);
            return MakeResponse(response, response.error_code);
        }

         
        [HttpPut("updateUserCompanyInfo")]
        public async Task<IActionResult> updateUserCompanyInfo([FromBody] CompanyInfo info)
        {
            ResponseUserCompanyInfo response = await Service.updateUserCompanyInfo(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpPost("updateUserCompanyInfoWithFile")]
        public async Task<IActionResult> updateUserCompanyInfoWithFile([FromForm] CompanyInfoWithFile info)
        {
            //Console.WriteLine("================updateUserCompanyInfoWithFile");
            ResponseUserCompanyInfo response = await Service.updateUserCompanyInfoWithFile(info);
            return MakeResponse(response, response.error_code);
        }

 
        [HttpPut("updateClientCompanyInfo")]
        public async Task<IActionResult> updateClientCompanyInfo([FromBody] CompanyInfo info)
        {
            ResponseClientCompanyInfo response = await Service.updateClientCompanyInfo(info);
            return MakeResponse(response, response.error_code);
        }
        
        [HttpPut("updateClientCompanyInfoWithFile")]
        public async Task<IActionResult> updateClientCompanyInfoWithFile([FromForm] CompanyInfoWithFile info)
        {
            ResponseClientCompanyInfo response = await Service.updateClientCompanyInfoWithFile(info);
            return MakeResponse(response, response.error_code);
        } 
    }
}
