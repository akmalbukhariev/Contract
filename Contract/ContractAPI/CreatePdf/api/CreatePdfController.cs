using ContractAPI.CreatePdf.service;
using ContractAPI.Helper;
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
    }
}
