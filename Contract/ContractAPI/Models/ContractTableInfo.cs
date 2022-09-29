using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Models
{
    public class BaseContractTableInfo
    {
        public string user_phone_number { get; set; }
        public string preparer { get; set; }
        public string contract_number { get; set; }
        public string company_contractor_name { get; set; }
        public string date_of_contract { get; set; }
        public string contract_price { get; set; }

        public void Copy(BaseContractTableInfo other)
        {
            this.user_phone_number = other.user_phone_number;
            this.preparer = other.preparer;
            this.contract_number = other.contract_number;
            this.company_contractor_name = other.company_contractor_name;
            this.date_of_contract = other.date_of_contract;
            this.contract_price = other.contract_price;
        }
    }
}
