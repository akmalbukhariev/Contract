using System;
using System.Collections.Generic;
using System.Text;

namespace LibContract.HttpModels
{
    public class LastUsedContractNumber
    {
        public string user_phone_number { get; set; }
        public string sequence_number { get; set; }

        public LastUsedContractNumber(LastUsedContractNumber other)
        {
            Copy(other);
        }

        public LastUsedContractNumber()
        {
            
        }

        public void Copy(LastUsedContractNumber other)
        {
            this.user_phone_number = other.user_phone_number;
            this.sequence_number = other.sequence_number;
        }
    }
}
