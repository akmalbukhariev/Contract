﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Models
{
    public class ClientCompanyInfo : CompanyInfo
    {
        public ClientCompanyInfo()
        {
            
        }

        public ClientCompanyInfo(CompanyInfo other)
        {
            Copy(other);
        }
    }
}