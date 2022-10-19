using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contract.HttpModels
{
    public class CreateContractInfo
    {
        #region Properties
        public string user_phone_number { get; set; } 
        public int open_client_info { get; set; }
        public int open_search_client { get; set; }
        public string client_stir { get; set; }
        public string client_company_name { get; set; }
        public string user_company_name { get; set; }
        public string service_type { get; set; }
        public int service_type_index { get; set; }
        public string contract_number { get; set; }
        public string contract_currency { get; set; }
        public int contract_currency_index { get; set; }
        public string amount_of_qqs { get; set; }
        public int amount_of_qqs_index { get; set; }
        public int is_execise_tax { get; set; }
        public string interest_text { get; set; }
        public string total_cost_text { get; set; }
        public int agree { get; set; }
        public string created_date { get; set; }
        public string comment { get; set; }
        public string deleted_date { get; set; }
        /// <summary>
        /// 1: yes, 0: no
        /// </summary>
        public int is_deleted { get; set; }
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
            this.client_company_name = other.client_company_name;
            this.user_company_name = other.user_company_name; 
            this.service_type = other.service_type;
            this.service_type_index = other.service_type_index;
            this.contract_number = other.contract_number;
            this.contract_currency = other.contract_currency;
            this.contract_currency_index = other.contract_currency_index;
            this.amount_of_qqs = other.amount_of_qqs;
            this.amount_of_qqs_index = other.amount_of_qqs_index;
            this.is_execise_tax = other.is_execise_tax;
            this.interest_text = other.interest_text;
            this.total_cost_text = other.total_cost_text;
            this.agree = other.agree;
            this.created_date = other.created_date;
            this.comment = other.comment;
            this.deleted_date = other.deleted_date;
            this.is_deleted = other.is_deleted;
        }
    }
}
