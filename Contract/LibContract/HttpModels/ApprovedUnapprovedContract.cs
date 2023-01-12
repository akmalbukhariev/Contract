using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibContract.HttpModels
{
    public class ApprovedUnapprovedContract
    {
        public string contract_number { get; set; }
        /// <summary>
        /// yes: 1; no: 0;
        /// </summary>
        public int is_approved { get; set; }
        /// <summary>
        /// yes: 1; no: 0;
        /// </summary>
        public int use_is_approved { get; set; } = 1;
        public string user_phone_number { get; set; }
        public string contragent_phone_number { get; set; }
        public string user_stir { get; set; }
        public string client_stir { get; set; }

        public ApprovedUnapprovedContract()
        {
            
        }

        public ApprovedUnapprovedContract(ApprovedUnapprovedContract other)
        {
            this.Copy(other);
        }

        public void Copy(ApprovedUnapprovedContract other)
        {
            contract_number = other.contract_number;
            is_approved = other.is_approved;
            use_is_approved = other.use_is_approved;
            user_phone_number = other.user_phone_number;
            contragent_phone_number = other.contragent_phone_number;
            user_stir = other.user_stir;
            client_stir = other.client_stir;
        }
    }
}
