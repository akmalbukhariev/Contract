using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Models
{
    public class ContractInfo
    {
        #region Properties
        public string phone_number { get; set; }

        //Contrat page 1
        public int open_client_info { get; set; }
        public int open_search_client { get; set; }
        public int customer_index { get; set; }
        public string company_name { get; set; }
        public string address_of_company { get; set; }
        public string account_number { get; set; }
        public string tin_enterprise { get; set; }
        public string name_of_bank { get; set; }
        public string bank_code { get; set; }
        public int are_you_tax_payer { get; set; }
        public string vat_code { get; set; }
        public string phone_number_of_company { get; set; }
        public int position_of_signatory { get; set; }
        public string full_name_of_signatory { get; set; }
        public int is_account_provided { get; set; }
        public string accountant_name { get; set; }
        public int is_counsel_provided { get; set; }
        public string counsel_name { get; set; }

        //Contrat page 2
        public int service_type_index { get; set; }
        public string contract_number { get; set; }
        public int contract_currency_index { get; set; }
        public int amount_of_vat_index { get; set; }
        public int is_execise_tax { get; set; }
        public string interest_text { get; set; }
        public string total_cost_text { get; set; }
        public int agree { get; set; }
        
        public string created_date { get; set; }
        #endregion

        public ContractInfo()
        {
            
        }

        public ContractInfo(ContractInfo other)
        {
            this.Copy(other);
        }

        public void Copy(ContractInfo other)
        {
            this.phone_number = other.phone_number;

            //Contrat page 1
            this.open_client_info = other.open_client_info;
            this.open_search_client = other.open_search_client;
            this.customer_index = other.customer_index;
            this.company_name = other.company_name;
            this.address_of_company = other.address_of_company;
            this.account_number = other.account_number;
            this.tin_enterprise = other.tin_enterprise;
            this.name_of_bank = other.name_of_bank;
            this.bank_code = other.bank_code;
            this.are_you_tax_payer = other.are_you_tax_payer;
            this.vat_code = other.vat_code;
            this.phone_number_of_company = other.phone_number_of_company;
            this.position_of_signatory = other.position_of_signatory;
            this.full_name_of_signatory = other.full_name_of_signatory;
            this.is_account_provided = other.is_account_provided;
            this.accountant_name = other.accountant_name;
            this.is_counsel_provided = other.is_counsel_provided;
            this.counsel_name = other.counsel_name;

            //Contrat page 2
            this.service_type_index = other.service_type_index;
            this.contract_number = other.contract_number;
            this.contract_currency_index = other.contract_currency_index;
            this.amount_of_vat_index = other.amount_of_vat_index;
            this.is_execise_tax = other.is_execise_tax;
            this.interest_text = other.interest_text;
            this.total_cost_text = other.total_cost_text;
            this.agree = other.agree;

            this.created_date = other.created_date;
        }
    }
}
