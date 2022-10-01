using ContractAPI.DataAccess;
using ContractAPI.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.AppInfo.service
{
    public interface IAppService
    { 
        Task<ResponseAboutApp> getAboutApp(string lan_code);
    }
}
