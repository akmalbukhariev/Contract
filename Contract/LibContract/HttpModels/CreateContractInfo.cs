﻿using System;
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
        /// <summary>
        /// 1: yes, 0: no
        /// </summary>
        public int is_approved_contragent { get; set; }
        public string contragent_phone_number { get; set; }
        public string comment { get; set; }
        public string deleted_date { get; set; }
        /// <summary>
        /// 1: yes, 0: no
        /// </summary>
        public int is_canceled { get; set; }
        public string pdf_url { get; set; }
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
            is_approved_contragent = other.is_approved_contragent;
            contragent_phone_number = other.contragent_phone_number;
            comment = other.comment;
            deleted_date = other.deleted_date;
            is_canceled = other.is_canceled;
            pdf_url = other.pdf_url;
        }

        public void CheckNull()
        {
            user_phone_number = user_phone_number.CheckNull();
            user_stir = user_stir.CheckNull();
            client_stir = client_stir.CheckNull();
            client_company_name = client_company_name.CheckNull();
            user_company_name = user_company_name.CheckNull();
            contract_sequence_number = contract_sequence_number.CheckNull();
            contract_number = contract_number.CheckNull();
            contract_currency = contract_currency.CheckNull();
            amount_of_qqs = amount_of_qqs.CheckNull();
            interest_text = interest_text.CheckNull();
            total_cost_text = total_cost_text.CheckNull();
            created_date = created_date.CheckNull();
            contragent_phone_number = contragent_phone_number.CheckNull();
            comment = comment.CheckNull();
            deleted_date = deleted_date.CheckNull();
            pdf_url = pdf_url.CheckNull();
        }
    }
}
