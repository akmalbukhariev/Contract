using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Models
{
    public class ServicesInfo
    {
        public string contract_number { get; set; }
        public string name_of_service { get; set; }
        public string unit_of_measure { get; set; }
        public int amount_value { get; set; }
        public string currency_text { get; set; }
        public string created_date { get; set; }

        public ServicesInfo()
        {
            
        }

        public ServicesInfo(ServicesInfo other)
        {
            this.Copy(other);
        }

        public void Copy(ServicesInfo other)
        {
            this.contract_number = other.contract_number;
            this.name_of_service = other.name_of_service;
            this.unit_of_measure = other.unit_of_measure;
            this.amount_value = other.amount_value;
            this.currency_text = other.currency_text;
            this.created_date = other.created_date;
        }
    }
}
