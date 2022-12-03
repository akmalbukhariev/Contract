using ContractAPI.CanceledContractInfo.service;
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

namespace ContractAPI.CanceledContractInfo.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CanceledContractController : AppBaseController<ICanceledContractService>
    { 
        public CanceledContractController(ICanceledContractService service) : base(service)
        {
            
        }

        [HttpGet("getCanceledContract/{phoneNumber}")]
        public async Task<IActionResult> getCanceledContract(string phoneNumber)
        {
            ResponseCanceledContract response = await Service.getCanceledContract(phoneNumber);
            return MakeResponse(response, response.error_code);
        }

        [HttpPost("setCanceledContract")]
        public async Task<IActionResult> setCanceledContract([FromBody] CanceledContract info)
        {
            ResponseCanceledContract response = await Service.setCanceledContract(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpDelete("deleteCanceledContract")]
        public async Task<IActionResult> deleteCanceledContract([FromBody] CanceledContract info)
        {
            ResponseCanceledContract response = await Service.deleteCanceledContract(info);
            return MakeResponse(response, response.error_code);
        } 
    }
}
