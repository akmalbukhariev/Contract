﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibContract.HttpModels
{
    public class ApplicableContract : BaseContractTableInfo
    {
        public string payment_percent { get; set; }

        public ApplicableContract()
        {
            
        }

        public ApplicableContract(ApplicableContract other)
        {
            this.Copy(other);
        }

        public void Copy(ApplicableContract other)
        {
            base.Copy(other);
            this.payment_percent = other.payment_percent;
        }
    }
}
