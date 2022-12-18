using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibContract.HttpModels
{
    public class PurposeOfContract
    {
        public string user_phone_number { get; set; }
		public string specify_service_type { get; set; }
		public string contract_number { get; set; }
		public string contract_currency { get; set; }
		public string amount_of_qqs { get; set; }
		public int is_there_excise_tax { get; set; }
		public string name_of_service_type { get; set; }
		public string unit_of_measure { get; set; }
		public int amount { get; set; }
		public int price { get; set; }
		public string total_cost_of_service { get; set; }
		public int public_offer { get; set; }

		public void Copy(PurposeOfContract other)
		{
			this.user_phone_number = other.user_phone_number;
			this.specify_service_type = other.specify_service_type;
			this.contract_number = other.contract_number;
			this.contract_currency = other.contract_currency;
			this.amount_of_qqs = other.amount_of_qqs;
			this.is_there_excise_tax = other.is_there_excise_tax;
			this.name_of_service_type = other.name_of_service_type;
			this.unit_of_measure = other.unit_of_measure;
			this.amount = other.amount;
			this.price = other.price;
			this.total_cost_of_service = other.total_cost_of_service;
			this.public_offer = other.public_offer;
		}
	}
}
