using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contract.HttpModels
{
    public class UserCompanyInfo : CompanyInfo
    {
        public UserCompanyInfo()
        {
            
        }

        public UserCompanyInfo(CompanyInfo other)
        {
            Copy(other);
        }
    }
}
