using ContractAPI.DataAccess;
using ContractAPI.Helper;
using ContractAPI.Models;
using ContractAPI.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ContractAPI.AppInfo.service.impl
{
    public class AppService : AppBaseService, IAppService
    {
        public AppService(ContractMakerContext db)
        {
            dataBase = db;
        }

        public async Task<ResponseAboutApp> getAboutApp(string lan_code)
        {
            ResponseAboutApp response = new ResponseAboutApp();
            response.data = null;
            AboutApp found = await dataBase.AboutApp.Where(item => item.lan_code.Equals(lan_code))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (found == null)
            {
                response.result = false;
                response.message = Constants.NotFound;
                response.error_code = (int)HttpStatusCode.NotFound;

                return response;
            }

            response.data = new AboutApp();
            response.data.Copy(found);

            response.result = true;
            response.message = Constants.Success;
            response.error_code = (int)HttpStatusCode.OK;

            return response;
        }
    }
}
