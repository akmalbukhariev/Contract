using ContractAPI.DataAccess;
using ContractAPI.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.MyCompanyInfo.service.impl
{
    public class UserCompanyInfoService : AppBaseService, IUserCompanyInfoService
    {
        public UserCompanyInfoService(ContractMakerContext db)
        {
            dataBase = db;
        }



    }
}
