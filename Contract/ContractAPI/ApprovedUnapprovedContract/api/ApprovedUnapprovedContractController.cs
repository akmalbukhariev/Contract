using ContractAPI.ApprovedUnapprovedContract.service;
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

namespace ContractAPI.ApprovedUnapprovedContract.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovedUnapprovedContractController : AppBaseController<IApprovedUnapprovedContractService>
    { 
        public ApprovedUnapprovedContractController(IApprovedUnapprovedContractService service) : base(service)
        {
        }
         
        [HttpPost("setApprovedContract/{contract_number}")]
        public async Task<IActionResult> setApprovedContract(string contract_number)
        {
            ResponseApprovedUnapprovedContract response = await Service.setApprovedContract(contract_number);
            return MakeResponse(response, response.error_code);
        }

        [HttpPost("setUnapprovedContract/{contract_number}")]
        public async Task<IActionResult> setUnapprovedContract(string contract_number)
        {
            ResponseApprovedUnapprovedContract response = await Service.setUnapprovedContract(contract_number);
            return MakeResponse(response, response.error_code);
        }

        [HttpPost("getApprovedOrUnapprovedContract")]
        public async Task<IActionResult> getApprovedOrUnapprovedContract([FromBody] LibContract.HttpModels.ApprovedUnapprovedContract info)
        {
            ResponseApprovedUnapprovedContract response = await Service.getApprovedOrUnapprovedContract(info);
            return MakeResponse(response, response.error_code);
        } 
    }
}
