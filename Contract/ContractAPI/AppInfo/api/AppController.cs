using ContractAPI.AppInfo.service;
using ContractAPI.DataAccess;
using ContractAPI.Helper;
using LibContract.HttpResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ContractAPI.AppInfo.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : AppBaseController<IAppService>
    {
        public AppController(IAppService service) : base(service)
        {
             
        }

        [HttpGet("getAboutApp/{lan_code}")]
        public async Task<IActionResult> getAboutApp(string lan_code)
        {
            ResponseAboutApp response = await Service.getAboutApp(lan_code);
            return MakeResponse(response, response.error_code);
        }

        [HttpGet("createPdf")]
        public async Task<IActionResult> createPdf()
        {
            ResponseAboutApp response = await Service.createPdf();
            return MakeResponse(response, response.error_code);
        }
    }
}
