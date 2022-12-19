using LibContract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.CreatePdf.service
{
    public interface ICreatePdfService
    {
        Task<ResponseCreatePdf> createPdf(string contract_number);
    }
}
