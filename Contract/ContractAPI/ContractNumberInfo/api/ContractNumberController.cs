using Contract.HttpModels;
using Contract.HttpResponse;
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
            ResponseContractNumber response = await Service.getContractNumber(phoneNumber);
            return MakeResponse(response, response.error_code);
        }

        [HttpPost("setContractNumber")]
        public async Task<IActionResult> setContractNumber([FromBody] ContractNumber info)
        {
            ResponseContractNumber response = await Service.setContractNumber(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpPut("updateContractNumber")]
        public async Task<IActionResult> updateContractNumber([FromBody] ContractNumber info)
        {
            ResponseContractNumber response = await Service.updateContractNumber(info);
            return MakeResponse(response, response.error_code);
        }
    }
}
