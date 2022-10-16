using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.HttpModels
{
    public class DeleteCompanyInfo
    {
        public string user_phone_number { get; set; } = "";
        public string stir_of_company { get; set; } = "";

        public DeleteCompanyInfo()
        {
            
        }

        public DeleteCompanyInfo(DeleteCompanyInfo other)
        {
            this.Copy(other);
        }

        public void Copy(DeleteCompanyInfo other)
        {
            this.user_phone_number = other.user_phone_number;
            this.stir_of_company = other.stir_of_company;
        }
    }
}
