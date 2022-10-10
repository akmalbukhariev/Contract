using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Models
{
    public class CreateContractInfo
    {
        #region Properties
        public string user_phone_number { get; set; } 
        public int open_client_info { get; set; }
        public int open_search_client { get; set; }
        public string client_stir { get; set; }
        public string service_type { get; set; }
        public string contract_number { get; set; }
        public string contract_currency { get; set; }
        public string amount_of_qqs { get; set; }
        public int is_execise_tax { get; set; }
        public string interest_text { get; set; }
        public string total_cost_text { get; set; }
        public int agree { get; set; }
        public string created_date { get; set; }
        #endregion

        public CreateContractInfo()
        {
            
        }

        public CreateContractInfo(CreateContractInfo other)
        {
            this.Copy(other);
        }

        public void Copy(CreateContractInfo other)
        {
            this.user_phone_number = other.user_phone_number;
            this.open_client_info = other.open_client_info;
            this.open_search_client = other.open_search_client;
            this.client_stir = other.client_stir;
            this.service_type = other.service_type;
            this.contract_number = other.contract_number;
            this.contract_currency = other.contract_currency;
            this.amount_of_qqs = other.amount_of_qqs;
            this.is_execise_tax = other.is_execise_tax;
            this.interest_text = other.interest_text;
            this.total_cost_text = other.total_cost_text;
            this.agree = other.agree;
            this.created_date = other.created_date;
        }
    }
}
