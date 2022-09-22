using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Models
{
    public class CreateContract
    {
        #region Properties
        public CompanyInfo client_company_info { get; set; } = new CompanyInfo();
        public CreateContractInfo contract_info { get; set; } = new CreateContractInfo();
        public List<ServicesInfo> service_list { get; set; } = new List<ServicesInfo>();
        #endregion

        public CreateContract()
        {
            
        }

        public CreateContract(CreateContract other)
        {
            this.Copy(other);
        }

        public void Copy(CreateContract other)
        {
            this.client_company_info.Copy(other.client_company_info);
            this.contract_info.Copy(other.contract_info);    

            foreach (ServicesInfo info in other.service_list)
            {
                this.service_list.Add(new ServicesInfo(info));
            }
        }
    }
}
