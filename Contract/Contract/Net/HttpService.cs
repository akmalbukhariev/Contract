using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Contract.Net
{
   public  class HttpService
    {
        #region Url
        public static string SERVER_URL = "https://localhost:5001/";
        public static string URL_LOGIN = SERVER_URL + "login";
        public static string URL_SIGN_UP = SERVER_URL + "signUp";
        public static string URL_GET_USER = SERVER_URL + "getUser/"; //phoneNumber
        public static string URL_GET_USER_COMPANY_INFO = SERVER_URL + "getUserCompanyInfo/"; //phoneNumber
        public static string URL_SET_USER_COMPANY_INFO = SERVER_URL + "setUserCompanyInfo";
        public static string URL_UPDATE_USER_COMPANY_INFO = SERVER_URL + "updateUserCompanyInfo";
        public static string URL_UPDATE_USER_PASSWORD = SERVER_URL + "updateUserPassword";
        public static string URL_GET_PURPOSE_OF_COMPANY = SERVER_URL + "getPurposeOfCompany/"; //phoneNumber
        public static string URL_SET_PURPOSE_OF_COMPANY = SERVER_URL + "setPurposeOfCompany";
        public static string URL_GET_UNAPPROVED_CONTRACT = SERVER_URL + "getUnapprovedContract/"; //phoneNumber
        public static string URL_SET_UNAPPROVED_CONTRACT = SERVER_URL + "setUnapprovedContract";
        public static string URL_GET_APPLICABLE_CONTRACT = SERVER_URL + "getApplicableContract/"; //phoneNumber
        public static string URL_SET_APPLICABLE_CONTRACT = SERVER_URL + "setApplicableContract";
        public static string URL_GET_CANCELED_CONTRACTS = SERVER_URL + "getCanceledContract/"; //phoneNumber
        public static string URL_SET_CANCELED_CONTRACTS = SERVER_URL + "setCanceledContract";
        public static string URL_GET_ABOUT_APP = SERVER_URL + "getAboutApp/"; //lan_code
        #endregion

        private static HttpStatusCode StatusCode;
        private static readonly WebClient webClient = new WebClient();
        private static JsonSerializerSettings settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };
    }
}
