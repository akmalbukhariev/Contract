using Contract.HttpModels;
using Contract.HttpResponse;
using ContractAPI.ContractServices.service;
using ContractAPI.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.ContractServices.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceInfoController : AppBaseController<IContractServiceInfoService>
    {
        public ServiceInfoController(IContractServiceInfoService service) : base(service)
        {

        }

        [HttpPost("setContractServiceInfo")]
        public async Task<IActionResult> setContractServiceInfo(List<ServicesInfo> infoList)
        {
            ResponseServiceInfo response = await Service.setContractServiceInfo(infoList);
            return MakeResponse(response, response.error_code);
        }

        [HttpDelete("deleteContractServiceInfo")]
        public async Task<IActionResult> deleteContractServiceInfo(string contract_number)
        {
            ResponseServiceInfo response = await Service.deleteContractServiceInfo(contract_number);
            return MakeResponse(response, response.error_code);
        }
    }
}
