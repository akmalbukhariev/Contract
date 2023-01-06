using ContractAPI.CreatePdf.service;
using ContractAPI.Helper;
using LibContract.HttpResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.CreatePdf.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreatePdfController : AppBaseController<ICreatePdfService>
    {
        public CreatePdfController(ICreatePdfService service) : base(service)
        {

        }

        [HttpPost("createPdf/{contractNumber}")]
        public async Task<IActionResult> createPdf(string contractNumber)
        {
            ResponseCreatePdf response = await Service.createPdf(contractNumber);
            return MakeResponse(response, response.error_code);
        } 
    }
}
