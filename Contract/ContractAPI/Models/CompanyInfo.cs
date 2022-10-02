using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Models
{
    public class CompanyInfo
    {
        #region Properties
        public string user_phone_number { get; set; }
        /// <summary>
        /// 1 = yes, 0 = no
        /// </summary>
        public string company_name { get; set; }
        public string address_of_company { get; set; }
        public string account_number { get; set; }
        public string ctr_of_company { get; set; }
        public string name_of_bank { get; set; }
        public string bank_code { get; set; }
        /// <summary>
        /// 1 = yes, 0 = no
        /// </summary>
        public int are_you_qqs_payer { get; set; } 
        public string qqs_number { get; set; }
        public string company_phone_number { get; set; }
        public string position_of_signer { get; set; }
        public string name_of_signer { get; set; }
        /// <summary>
        /// 1 = yes, 0 = no
        /// </summary>
        public int is_accountant_provided { get; set; } 
        public string accountant_name { get; set; }
        /// <summary>
        /// 1 = yes, 0 = no
        /// </summary>
        public int is_legal_counsel_provided { get; set; }
        public string counsel_name { get; set; }
        public string company_logo_url { get; set; }
        public string created_date { get; set; }
        #endregion

        public CompanyInfo()
        {
            
        }

        public CompanyInfo(CompanyInfo other)
        {
            this.Copy(other);
        }

        public void Copy(CompanyInfo other)
        {
            this.user_phone_number = other.user_phone_number;
            this.company_name = other.company_name;
            this.address_of_company = other.address_of_company;
            this.account_number = other.account_number;
            this.ctr_of_company = other.ctr_of_company;
            this.name_of_bank = other.name_of_bank;
            this.bank_code = other.bank_code;
            this.are_you_qqs_payer = other.are_you_qqs_payer;
            this.qqs_number = other.qqs_number;
            this.company_phone_number = other.company_phone_number;
            this.position_of_signer = other.position_of_signer;
            this.name_of_signer = other.name_of_signer;
            this.is_accountant_provided = other.is_accountant_provided;
            this.accountant_name = other.accountant_name;
            this.is_legal_counsel_provided = other.is_legal_counsel_provided;
            this.counsel_name = other.counsel_name;
            this.company_logo_url = other.company_logo_url;
            this.created_date = other.created_date;
        }
    }
}
