using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Helper
{
    public static class LanguageConverter
    {
        public static string GetNotificationTitle(string lanCode)
        {
            string strRes = "";

            switch (lanCode)
            {
                case Constants.LanUz:
                    strRes = "Shartnoma";
                    break;
                case Constants.LanUzCyrl:
                    strRes = "Шартнома";
                    break;
                case Constants.LanEn:
                    strRes = "Contract";
                    break;
                case Constants.LanRu:
                    strRes = "Контракт";
                    break;
            }
             
            return strRes;
        }

        public static string MessageNewContract(string lanCode)
        {
            string strRes = "";

            switch (lanCode)
            {
                case Constants.LanUz:
                    strRes = "Yangi shartnoma";
                    break;
                case Constants.LanUzCyrl:
                    strRes = "Янги шартнома";
                    break;
                case Constants.LanEn:
                    strRes = "New Contract";
                    break;
                case Constants.LanRu:
                    strRes = "Новый контракт";
                    break;
            }

            return strRes;
        }

        public static string MessageContractApproved(string lanCode)
        {
            string strRes = "";

            switch (lanCode)
            {
                case Constants.LanUz:
                    strRes = "Shartnoma tasdiqlandi";
                    break;
                case Constants.LanUzCyrl:
                    strRes = "Шартнома тасдиқланди";
                    break;
                case Constants.LanEn:
                    strRes = "Contract has been approved";
                    break;
                case Constants.LanRu:
                    strRes = "Контракт был одобрен";
                    break;
            }

            return strRes;
        }
    }
}
