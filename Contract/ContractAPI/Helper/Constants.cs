using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI
{
    public class Constants
    {
        #region Language
        public const string LanUz = "Uzbek";
        public const string LanUzCyrl = "Uzbek (Cyrillic)";
        public const string LanEn = "English";
        public const string LanRu = "Russian";
        #endregion

        public const string Success = "Success";
        public const string NotFound = "Not Found!";
        public const string Found = "Found!";
        public const string BadRequest = "Bad Request!";
        public const string DoNotExist = "Do not exist!";
        public const string Exist = "Exist!";

        public const string TimeFormat = "yyyyMMdd_hhmmss.fff";

        public const string SaveCompanyImagePath = "/Upload/images/company/";
        public const string SaveSignImagePath = "/Upload/images/signature/";
        public const string SaveContractPdfPath = "/Upload/contracts_pdf/";
        public const string SaveContractHtmlPath = "/Upload/contracts_html/";
        public const string SaveContractPdfUrl = "Upload/contracts_pdf/";
        public const string SaveContractHtmlUrl = "Upload/contracts_html/";
        public const string NotificationKey = "/Notification//contract_private_key.json";
    }
}
