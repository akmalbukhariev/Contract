﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibContract.HttpModels
{
    public class UserCompanyInfo : CompanyInfo
    {  
        public UserCompanyInfo()
        {
            
        }

        public UserCompanyInfo(UserCompanyInfo other)
        { 
            Copy(other);
        }
    }
}
