using System;
using System.Collections.Generic;
using System.Text;

namespace LibContract.HttpModels
{
    public class ContractTemplate
    {
        public int id { get; set; }
        public string user_phone_number { get; set; }
        public int contract_number_format_id { get; set; }
        public string contract_number_option { get; set; }
        public string company_address { get; set; }
        public string template_name { get; set; }
        public string clauses { get; set; }
        public string created_date { get; set; }

        public ContractTemplate()
        {
            
        }

        public ContractTemplate(ContractTemplate other)
        {
            Copy(other);
        }

        public void Copy(ContractTemplate other)
        {
            id = other.id;
            user_phone_number = other.user_phone_number;
            contract_number_format_id = other.contract_number_format_id;
            contract_number_option = other.contract_number_option;
            company_address = other.company_address;
            template_name = other.template_name;
            clauses = other.clauses;
            created_date = other.created_date;
        }
    }
}
