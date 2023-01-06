using ContractAPI.Helper;
using ContractAPI.Models;
using ContractAPI.Signature.service;
using LibContract.HttpModels;
using LibContract.HttpResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Signature.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignatureController : AppBaseController<ISignatureService>
    {
        public SignatureController(ISignatureService service) : base(service)
        {

        }

        [HttpPost("setSignature")]
        public async Task<IActionResult> setSignature([FromForm] SignatureWithFile info)
        {
            ResponseSignatureInfo response = await Service.setSignature(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpPost("checkSignature/{phone_number}")]
        public async Task<IActionResult> checkSignature(string phone_number)
        {
            ResponseSignatureInfo response = await Service.checkSignature(phone_number);
            return MakeResponse(response, response.error_code);
        }
    }
}
