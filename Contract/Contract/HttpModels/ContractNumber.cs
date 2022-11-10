using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.HttpModels
{
    public class ContractNumber
    {
        public string user_phone_number { get; set; }
        public string sequence_number { get; set; }
        public string option { get; set; }
        public int format { get; set; } 

        public ContractNumber()
        {
            
        }

        public ContractNumber(ContractNumber other)
        {
            Copy(other);
        }

        public void Copy(ContractNumber other)
        {
            this.user_phone_number = other.user_phone_number;
            this.sequence_number = other.sequence_number;
            this.option = other.option;
            this.format = other.format;
        }
    }
}
