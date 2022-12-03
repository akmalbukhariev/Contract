using LibContract.HttpModels;
using LibContract.HttpResponse;
using ContractAPI.Helper;
using ContractAPI.OfferObjection.service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.OfferObjection.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferObjectionController : AppBaseController<IOfferObjectionService>
    {
        public OfferObjectionController(IOfferObjectionService service) : base(service)
        {

        }

        [HttpPost("Save")]
        public async Task<IActionResult> Save([FromBody] OfferAndObjection info)
        {
            ResponseOfferObjection response = await Service.Save(info);
            return MakeResponse(response, response.error_code);
        }
    }
}
