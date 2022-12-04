using ContractAPI.Helper;
using ContractAPI.service.Convert2Document;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Convert2Document.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvertDocumentController : AppBaseController<IConvertDocumentService>
    {
        public ConvertDocumentController(IConvertDocumentService service) : base(service)
        {

        }


    }
}
