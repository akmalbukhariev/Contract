﻿using LibContract.HttpResponse;
using ContractAPI.DataAccess; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.AppInfo.service
{
    public interface IAppService
    { 
        Task<ResponseAboutApp> getAboutApp(string lan_code);
        Task<ResponseAboutApp> createPdf();
    }
}
