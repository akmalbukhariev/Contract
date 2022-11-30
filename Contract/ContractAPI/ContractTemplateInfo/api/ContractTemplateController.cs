using Contract.HttpModels;
using Contract.HttpResponse;
using ContractAPI.ContractTemplateInfo.service;
using ContractAPI.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.ContractTemplateInfo.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractTemplateController : AppBaseController<IContractTemplateService>
    {
        public ContractTemplateController(IContractTemplateService service) : base(service)
        {

        }

        [HttpGet("getContractTemplate/{phoneNumber}")]
        public async Task<IActionResult> getContractTemplate(string phoneNumber)
        {
            ResponseContractTemplate response = await Service.getContractTemplate(phoneNumber);
            return MakeResponse(response, response.error_code);
        }

        [HttpPost("setContractTemplate")]
        public async Task<IActionResult> setContractTemplate([FromBody] ContractTemplate info)
        {
            ResponseContractTemplate response = await Service.setContractTemplate(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpPut("updateContractTemplate")]
        public async Task<IActionResult> updateContractTemplate([FromBody] ContractTemplate info)
        {
            ResponseContractTemplate response = await Service.updateContractTemplate(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpDelete("deleteContractTemplate")]
        public async Task<IActionResult> deleteContractTemplate([FromBody] ContractTemplate info)
        {
            ResponseContractTemplate response = await Service.deleteContractTemplate(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpGet("getAllReadyTemplate")]
        public async Task<IActionResult> getAllReadyTemplate()
        {
            ResponseReadyTemplate response = await Service.getAllReadyTemplate();
            return MakeResponse(response, response.error_code);
        }
    }
}
