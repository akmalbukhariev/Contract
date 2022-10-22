using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contract.HttpModels
{
    public class ApprovedUnapprovedContract  
    {
        public string user_phone_number { get; set; }
        //public string preparer { get; set; }
        public string user_stir { get; set; }
        //public string contract_number { get; set; }
        //public string company_contractor_name { get; set; }
        //public string date_of_contract { get; set; }
        //public string contract_price { get; set; }
        //public string payment_percent { get; set; }
        public int is_approved { get; set; }

        public ApprovedUnapprovedContract()
        {
            
        }

        public ApprovedUnapprovedContract(ApprovedUnapprovedContract other)
        {
            this.Copy(other);
        }

        public void Copy(ApprovedUnapprovedContract other)
        {
            this.user_phone_number = other.user_phone_number;
            this.user_stir = other.user_stir;
            //this.preparer = other.preparer;
            //this.contract_number = other.contract_number;
            //this.company_contractor_name = other.company_contractor_name;
            //this.date_of_contract = other.date_of_contract;
            //this.contract_price = other.contract_price;
            //this.payment_percent = other.payment_percent;
            this.is_approved = other.is_approved;
        }
    }
}
