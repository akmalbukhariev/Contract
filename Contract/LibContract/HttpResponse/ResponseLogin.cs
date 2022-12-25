using LibContract.HttpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibContract.HttpResponse
{
    public class ResponseLogin : Response<User>, IResponse
    {
        public UserCompanyInfo companyInfo { get; set; } = null;
    }
}
