using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.HttpModels
{
    public class OfferAndObjection
    {
        public string user_phone_number { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string created_date { get; set; }

        public OfferAndObjection()
        {
            
        }

        public OfferAndObjection(OfferAndObjection other)
        {
            Copy(other);
        }

        public void Copy(OfferAndObjection other)
        {
            this.user_phone_number = other.user_phone_number;
            this.type = other.type;
            this.description = other.description;
            this.created_date = other.created_date;
        }
    }
}
