using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.HttpModels
{
    public class ContractNumberTemplate
    {
        public int id { get; set; }
        public string user_phone_number { get; set; } 
        public string option { get; set; }
        public int format { get; set; }
        public string created_date { get; set; }
        public int is_deleted { get; set; }

        public ContractNumberTemplate()
        {
            
        }

        public ContractNumberTemplate(ContractNumberTemplate other)
        {
            Copy(other);
        }

        public void Copy(ContractNumberTemplate other)
        {
            this.id = other.id;
            this.user_phone_number = other.user_phone_number; 
            this.option = other.option;
            this.format = other.format;
            this.created_date = other.created_date;
            this.is_deleted = other.is_deleted;
        } 
    }
}
