using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.HttpModels
{
    public class ContractNumber
    {
        public string user_phone_number { get; set; }
        public string contract_date { get; set; }
        public string conatrct_time { get; set; }
        public string contract_option { get; set; }
        public int contract_format { get; set; }

        public ContractNumber()
        {
            
        }

        public ContractNumber(ContractNumber other)
        {
            Copy(other);
        }

        public void Copy(ContractNumber other)
        {
            this.user_phone_number = other.user_phone_number;
            this.contract_date = other.contract_date;
            this.conatrct_time = other.conatrct_time;
            this.contract_option = other.contract_option;
            this.contract_format = other.contract_format;
        }
    }
}
