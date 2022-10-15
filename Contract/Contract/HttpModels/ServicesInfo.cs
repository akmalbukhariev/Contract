using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contract.HttpModels
{
    public class ServicesInfo
    {
        public string contract_number { get; set; }
        public string name_of_service { get; set; }
        public string unit_of_measure { get; set; }
        public int unit_of_measure_index { get; set; }
        public int amount_value { get; set; }
        public string amount_value_price { get; set; }
        public string currency { get; set; }
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
            this.unit_of_measure_index = other.unit_of_measure_index;
            this.amount_value = other.amount_value;
            this.amount_value_price = other.amount_value_price;
            this.currency = other.currency;
            this.created_date = other.created_date;
        }
    }
}
