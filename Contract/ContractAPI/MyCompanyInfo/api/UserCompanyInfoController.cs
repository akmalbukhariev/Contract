using ContractAPI.Helper;
using ContractAPI.MyCompanyInfo.service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.MyCompanyInfo.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCompanyInfoController : AppBaseController<IUserCompanyInfoService>
    {
        public UserCompanyInfoController(IUserCompanyInfoService service) : base(service)
        {

        }
    }
}
