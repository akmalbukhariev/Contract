using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contract.HttpModels
{
    public class CanceledContract //: BaseContractTableInfo
    {
        public string user_phone_number { get; set; }
        public string preparer { get; set; }
        public string contract_number { get; set; }
        public string client_company_name { get; set; }
        public string user_company_name { get; set; }
        public string open_client_info { get; set; }
        public string open_search_client { get; set; }
        public string client_stir { get; set; }
        public string service_type { get; set; }
        public string contract_currency { get; set; }
        public string amount_of_qqs { get; set; }
        public string is_execise_tax { get; set; }
        public string interest_text { get; set; }
        public string total_cost_text { get; set; }
        public string agree { get; set; }
        public string comment { get; set; }
        public string created_date { get; set; }

        public CanceledContract()
        {
            
        }

        public CanceledContract(CanceledContract other)
        {
            this.Copy(other);
        }

        public void Copy(CanceledContract other)
        {
            user_phone_number = other.user_phone_number;
            preparer = other.preparer;
            contract_number = other.contract_number;
            client_company_name = other.client_company_name;
            user_company_name = other.user_company_name;
            open_client_info = other.open_client_info;
            open_search_client = other.open_search_client;
            client_stir = other.client_stir;
            service_type = other.service_type;
            contract_currency = other.contract_currency;
            amount_of_qqs = other.amount_of_qqs;
            is_execise_tax = other.is_execise_tax;
            interest_text = other.interest_text;
            total_cost_text = other.total_cost_text;
            agree = other.agree;
            comment = other.comment;
            created_date = other.created_date;
        }
    }
}
