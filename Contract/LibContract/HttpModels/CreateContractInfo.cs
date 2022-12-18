using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibContract.HttpModels
{
    public class CreateContractInfo
    {
        #region Properties
        public string user_phone_number { get; set; } 
        public int open_client_info { get; set; }
        public int open_search_client { get; set; }
        public string user_stir { get; set; }
        public string client_stir { get; set; }
        public string client_company_name { get; set; }
        public string user_company_name { get; set; }
        //public string service_type { get; set; }
        public int template_id { get; set; }
        public string contract_sequence_number { get; set; }
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
        /// <summary>
        /// 1: yes, 0: no
        /// </summary>
        public int is_approved { get; set; }
        public string comment { get; set; }
        public string deleted_date { get; set; }
        /// <summary>
        /// 1: yes, 0: no
        /// </summary>
        public int is_canceled { get; set; }
        #endregion

        public CreateContractInfo()
        {
            
        }

        public CreateContractInfo(CreateContractInfo other)
        {
            Copy(other);
        }

        public void Copy(CreateContractInfo other)
        {
            user_phone_number = other.user_phone_number;
            open_client_info = other.open_client_info;
            open_search_client = other.open_search_client;
            user_stir = other.user_stir;
            client_stir = other.client_stir; 
            client_company_name = other.client_company_name;
            user_company_name = other.user_company_name; 
            //service_type = other.service_type;
            template_id = other.template_id;
            contract_sequence_number = other.contract_sequence_number;
            contract_number = other.contract_number;
            contract_currency = other.contract_currency;
            contract_currency_index = other.contract_currency_index;
            amount_of_qqs = other.amount_of_qqs;
            amount_of_qqs_index = other.amount_of_qqs_index;
            is_execise_tax = other.is_execise_tax;
            interest_text = other.interest_text;
            total_cost_text = other.total_cost_text;
            agree = other.agree;
            created_date = other.created_date;
            is_approved = other.is_approved;
            comment = other.comment;
            deleted_date = other.deleted_date;
            is_canceled = other.is_canceled;
        }
    }
}
