using ContractAPI.Controllers.UnapprovedContracts.service;
using ContractAPI.DataAccess;
using ContractAPI.Helper;
using Contract.HttpModels;
using Contract.HttpResponse;
using ContractAPI.UnapprovedContracts.service.impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ContractAPI.UnapprovedContracts.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnapprovedContractController : AppBaseController<IUnapprovedContractService>
    {
        public UnapprovedContractController(IUnapprovedContractService service) : base(service)
        {
        }

        [HttpGet("getUnapprovedContract/{phoneNumber}")]
        public async Task<IActionResult> getUnapprovedContract(string phoneNumber)
        {
            ResponseUnapprovedContract response = await Service.getUnapprovedContract(phoneNumber);
            return MakeResponse(response, response.error_code);
        }

        [HttpPost("setUnapprovedContract")]
        public async Task<IActionResult> setUnapprovedContract([FromBody] UnapprovedContract info)
        {
            ResponseUnapprovedContract response = await Service.setUnapprovedContract(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpDelete("deleteUnapprovedContract")]
        public async Task<IActionResult> deleteUnapprovedContract([FromBody] UnapprovedContract info)
        {
            ResponseUnapprovedContract response = await Service.deleteUnapprovedContract(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpDelete("deleteUnapprovedContractAndSetCanceledContract")]
        public async Task<IActionResult> deleteUnapprovedContractAndSetCanceledContract([FromBody] CanceledContract info)
        {
            ResponseCanceledContract response = await Service.deleteUnapprovedContractAndSetCanceledContract(info);
            return MakeResponse(response, response.error_code);
        } 
    }
}
