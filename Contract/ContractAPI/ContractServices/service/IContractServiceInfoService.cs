using LibContract.HttpModels;
using LibContract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.ContractServices.service
{
    public interface IContractServiceInfoService
    {
        Task<ResponseServiceInfo> setContractServiceInfo(List<ServicesInfo> infoList);
        Task<ResponseServiceInfo> deleteContractServiceInfo(string contract_number);
    }
}
