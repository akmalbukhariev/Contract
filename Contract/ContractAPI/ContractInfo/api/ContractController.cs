using ContractAPI.ContractInfo.service;
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
    }
}
