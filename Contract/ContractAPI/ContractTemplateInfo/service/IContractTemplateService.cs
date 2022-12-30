using LibContract.HttpModels;
using LibContract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.ContractTemplateInfo.service
{
    public interface IContractTemplateService
    {
        Task<ResponseContractTemplate> getContractTemplate(string userPhoneNumber);
        Task<ResponseContractTemplate> setContractTemplate(ContractTemplate info);
        Task<ResponseContractTemplate> updateContractTemplate(ContractTemplate info);
        Task<ResponseContractTemplate> deleteContractTemplate(ContractTemplate info);
        Task<ResponseReadyTemplate> getAllReadyTemplate();
        Task<ResponseReadyTemplate> getAllReadyTemplateUrl();
    }
}
