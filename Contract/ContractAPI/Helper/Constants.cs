using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI
{
    public class Constants
    {
        public const string Success = "Success";
        public const string NotFound = "Not Found!";
        public const string Found = "Found!";
        public const string BadRequest = "Bad Request!";
        public const string DoNotExist = "Do not exist!";
        public const string Exist = "Exist!";

        public const string TimeFormat = "yyyyMMdd_hhmmss.fff";

        public const string SaveCompanyImagePath = "\\Upload\\images\\company\\";
        public const string SaveSignImagePath = "\\Upload\\images\\signature\\";
        public const string SaveContractPdfPath = "\\Upload\\contracts_pdf\\";
        public const string SaveContractPdfUrl = "Upload\\contracts_pdf\\";
        public const string NotificationKey = "\\Notification\\contract_private_key.json";
    }
}
