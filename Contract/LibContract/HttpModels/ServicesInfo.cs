using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibContract.HttpModels
{
    public class ServicesInfo
    {
        public int No { get; set; }
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
            No = other.No;
            contract_number = other.contract_number;
            name_of_service = other.name_of_service;
            unit_of_measure = other.unit_of_measure;
            unit_of_measure_index = other.unit_of_measure_index;
            amount_value = other.amount_value;
            amount_value_price = other.amount_value_price;
            currency = other.currency;
            created_date = other.created_date;
        }
    }
}
