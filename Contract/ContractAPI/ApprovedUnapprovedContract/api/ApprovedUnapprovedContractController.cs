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
         
        [HttpPost("setApprovedContract")]
        public async Task<IActionResult> setApprovedContract([FromBody] LibContract.HttpModels.ApprovedUnapprovedContract info)
        {
            ResponseApprovedUnapprovedContract response = await Service.setApprovedContract(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpPost("setUnapprovedContract")]
        public async Task<IActionResult> setUnapprovedContract([FromBody] LibContract.HttpModels.ApprovedUnapprovedContract info)
        {
            ResponseApprovedUnapprovedContract response = await Service.setUnapprovedContract(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpPost("getUnapprovedContract")]
        public async Task<IActionResult> getUnapprovedContract([FromBody] LibContract.HttpModels.ApprovedUnapprovedContract info)
        {
            ResponseApprovedUnapprovedContract response = await Service.getUnapprovedContract(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpPost("getApprovedContract")]
        public async Task<IActionResult> getApprovedContract([FromBody] LibContract.HttpModels.ApprovedUnapprovedContract info)
        {
            ResponseApprovedUnapprovedContract response = await Service.getApprovedContract(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpPost("getApprovedAndUnapprovedContract")]
        public async Task<IActionResult> getApprovedAndUnapprovedContract([FromBody] LibContract.HttpModels.ApprovedUnapprovedContract info)
        {
            ResponseApprovedUnapprovedContract response = await Service.getApprovedAndUnapprovedContract(info);
            return MakeResponse(response, response.error_code);
        } 
    }
}
