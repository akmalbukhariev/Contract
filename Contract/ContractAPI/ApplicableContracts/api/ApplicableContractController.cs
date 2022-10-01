using ContractAPI.ApplicableContracts.service;
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

namespace ContractAPI.ApplicableContracts.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicableContractController : AppBaseController<IApplicableContractService>
    {
        public ApplicableContractController(IApplicableContractService service) : base(service)
        {
        }

        [HttpGet("getApplicableContract/{phoneNumber}")]
        public async Task<IActionResult> getApplicableContract(string phoneNumber)
        {
            ResponseApplicableContract response = await Service.getApplicableContract(phoneNumber);
            return MakeResponse(response, response.error_code);
        }

        [HttpPost("setApplicableContract")]
        public async Task<IActionResult> setApplicableContract([FromBody] ApplicableContract info)
        {
            ResponseApplicableContract response = await Service.setApplicableContract(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpDelete("deleteApplicableContract")]
        public async Task<IActionResult> deleteApplicableContract([FromBody] ApplicableContract info)
        {
            ResponseApplicableContract response = await Service.deleteApplicableContract(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpDelete("deleteApplicableContractAndSetCanceledContract")]
        public async Task<IActionResult> deleteApplicableContractAndSetCanceledContract([FromBody] CanceledContract info)
        {
            ResponseCanceledContract response = await Service.deleteApplicableContractAndSetCanceledContract(info);
            return MakeResponse(response, response.error_code);
        } 
    }
}
