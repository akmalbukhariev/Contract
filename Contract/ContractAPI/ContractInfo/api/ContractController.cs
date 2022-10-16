using ContractAPI.ContractInfo.service;
using ContractAPI.DataAccess;
using ContractAPI.Helper;
using Contract.HttpModels;
using Contract.HttpResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ContractAPI.ContractInfo.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : AppBaseController<IContractService>
    { 
        public ContractController(IContractService service) : base(service)
        {
            
        }

        [HttpGet("getPurposeOfContract/{phoneNumber}")]
        public async Task<IActionResult> getPurposeOfContract(string phoneNumber)
        {
            ResponsePurposeOfContract response = await Service.getPurposeOfContract(phoneNumber);
            return MakeResponse(response, response.error_code);
        }

        [HttpPost("setPurposeOfContract")]
        public async Task<IActionResult> setPurposeOfContract([FromBody] PurposeOfContract info)
        {
            ResponsePurposeOfContract response = await Service.setPurposeOfContract(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpPost("createContract")]
        public async Task<IActionResult> createContract([FromBody] CreateContract info)
        {
            ResponseCreateContract response = await Service.createContract(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpPost("deleteContract")]
        public async Task<IActionResult> deleteContract(string contract_number)
        {
            ResponseCreateContract response = await Service.deleteContract(contract_number);
            return MakeResponse(response, response.error_code);
        }
    }
}
