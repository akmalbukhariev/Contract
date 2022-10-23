using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contract.HttpModels
{
    public class ApprovedUnapprovedContract  
    {
        /// <summary>
        /// yes: 1; no: 0;
        /// </summary>
        public int is_approved { get; set; }
        /// <summary>
        /// yes: 1; no: 0;
        /// </summary>
        public int use_is_approved { get; set; } = 1;
        public string user_phone_number { get; set; }
        public string user_stir { get; set; }

        public ApprovedUnapprovedContract()
        {
            
        }

        public ApprovedUnapprovedContract(ApprovedUnapprovedContract other)
        {
            this.Copy(other);
        }

        public void Copy(ApprovedUnapprovedContract other)
        {
            this.is_approved = other.is_approved;
            this.use_is_approved = other.use_is_approved;
            this.user_phone_number = other.user_phone_number;
            this.user_stir = other.user_stir; 
        }
    }
}
