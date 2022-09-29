﻿using ContractAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Models
{
    public class UnapprovedContract : BaseContractTableInfo
    { 
        public UnapprovedContract()
        {

        }

        public UnapprovedContract(UnapprovedContract other)
        {
            this.Copy(other);
        }

        public void Copy(UnapprovedContract other)
        {
            base.Copy(other);       
        }
    }
}
