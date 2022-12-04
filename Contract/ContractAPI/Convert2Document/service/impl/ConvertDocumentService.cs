using ContractAPI.DataAccess;
using ContractAPI.Helper;
using ContractAPI.service.Convert2Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Convert2Document.service.impl
{
    public class ConvertDocumentService : AppBaseService, IConvertDocumentService
    {
        public ConvertDocumentService(ContractMakerContext db)
        {
            dataBase = db;
        }

    }
}
