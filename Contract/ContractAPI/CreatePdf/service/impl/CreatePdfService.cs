using ContractAPI.DataAccess;
using ContractAPI.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.CreatePdf.service.impl
{
    public class CreatePdfService : AppBaseService, ICreatePdfService
    {
        public CreatePdfService(ContractMakerContext db)
        {
            dataBase = db;
        }
    }
}
