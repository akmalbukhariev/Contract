﻿using LibContract.HttpModels;
using LibContract.HttpResponse;
using ContractAPI.ContractNumberInfo.service;
using ContractAPI.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.ContractNumberInfo.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractNumberController : AppBaseController<IContractNumberService>
    {
        public ContractNumberController(IContractNumberService service) : base(service)
        {

        }

        [HttpGet("geContractNumber/{phoneNumber}")]
        public async Task<IActionResult> geContractNumber(string phoneNumber)
        {
            ResponseContractNumberTemplate response = await Service.getContractNumber(phoneNumber);
            return MakeResponse(response, response.error_code);
        }

        [HttpPost("setContractNumber")]
        public async Task<IActionResult> setContractNumber([FromBody] ContractNumberTemplate info)
        {
            ResponseContractNumberTemplate response = await Service.setContractNumber(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpPut("updateContractNumber")]
        public async Task<IActionResult> updateContractNumber([FromBody] ContractNumberTemplate info)
        {
            ResponseContractNumberTemplate response = await Service.updateContractNumber(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpDelete("deleteContractNumber")]
        public async Task<IActionResult> deleteContractNumber([FromBody] ContractNumberTemplate info)
        {
            ResponseContractNumberTemplate response = await Service.deleteContractTemplate(info);
            return MakeResponse(response, response.error_code);
        }
    }
}
