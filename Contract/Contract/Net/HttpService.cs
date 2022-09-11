using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Net
{
   public  class HttpService
    {
        #region Url 
        public static string SERVER_URL = "https://192.168.219.101:5001/api/ContractMaker/";
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

        public async static Task<ResponseLogin> Login(Login data)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestPostMethod(URL_LOGIN, data);
                response = JsonConvert.DeserializeObject<ResponseLogin>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseLogin>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseLogin>(); }

            ResponseLogin responseLogin = ConvertResponseObj<ResponseLogin>(response);
             
            return responseLogin;
        }

        public async static Task<ResponseSignUp> SignUp(User data)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestPostMethod(URL_SIGN_UP, data);
                response = JsonConvert.DeserializeObject<ResponseSignUp>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseSignUp>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseSignUp>(); }

            ResponseSignUp responseLogin = ConvertResponseObj<ResponseSignUp>(response);

            return responseLogin;
        }

        public async static Task<ResponseUser> GetUser(string phoneNumbera)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestGetMethod($"{URL_GET_USER}{phoneNumbera}");
                response = JsonConvert.DeserializeObject<ResponseUser>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseUser>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseUser>(); }

            ResponseUser responseLogin = ConvertResponseObj<ResponseUser>(response);

            return responseLogin;
        }

        public async static Task<ResponseUserCompanyInfo> GetUserCompanyInfo(string phoneNumbera)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestGetMethod($"{URL_GET_USER_COMPANY_INFO}{phoneNumbera}");
                response = JsonConvert.DeserializeObject<ResponseUserCompanyInfo>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseUserCompanyInfo>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseUserCompanyInfo>(); }

            ResponseUserCompanyInfo responseLogin = ConvertResponseObj<ResponseUserCompanyInfo>(response);

            return responseLogin;
        }

        public async static Task<ResponseUserCompanyInfo> SetUserCompanyInfo(UserCompanyInfo data)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestPostMethod(URL_SET_USER_COMPANY_INFO, data);
                response = JsonConvert.DeserializeObject<ResponseUserCompanyInfo>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseUserCompanyInfo>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseUserCompanyInfo>(); }

            ResponseUserCompanyInfo responseLogin = ConvertResponseObj<ResponseUserCompanyInfo>(response);

            return responseLogin;
        }

        public async static Task<ResponseUserCompanyInfo> UpdateUserCompanyInfo(UserCompanyInfo data)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestPutMethod(URL_UPDATE_USER_COMPANY_INFO, data);
                response = JsonConvert.DeserializeObject<ResponseUserCompanyInfo>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseUserCompanyInfo>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseUserCompanyInfo>(); }

            ResponseUserCompanyInfo responseLogin = ConvertResponseObj<ResponseUserCompanyInfo>(response);

            return responseLogin;
        }

        public async static Task<ResponseLogin> UpdateUserPassword(User data)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestPutMethod(URL_UPDATE_USER_PASSWORD, data);
                response = JsonConvert.DeserializeObject<ResponseLogin>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseLogin>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseLogin>(); }

            ResponseLogin responseLogin = ConvertResponseObj<ResponseLogin>(response);

            return responseLogin;
        }

        public async static Task<ResponsePurposeOfContract> GetPurposeOfCompany(string phoneNumbera)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestGetMethod($"{URL_GET_PURPOSE_OF_COMPANY}{phoneNumbera}");
                response = JsonConvert.DeserializeObject<ResponsePurposeOfContract>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponsePurposeOfContract>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponsePurposeOfContract>(); }

            ResponsePurposeOfContract responseLogin = ConvertResponseObj<ResponsePurposeOfContract>(response);

            return responseLogin;
        }

        public async static Task<ResponsePurposeOfContract> SetPurposeOfCompany(PurposeOfContract data)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestPostMethod(URL_SET_PURPOSE_OF_COMPANY, data);
                response = JsonConvert.DeserializeObject<ResponsePurposeOfContract>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponsePurposeOfContract>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponsePurposeOfContract>(); }

            ResponsePurposeOfContract responseLogin = ConvertResponseObj<ResponsePurposeOfContract>(response);

            return responseLogin;
        }

        public async static Task<ResponseUnapprovedContract> GetUnapprovedContract(string phoneNumbera)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestGetMethod($"{URL_GET_UNAPPROVED_CONTRACT}{phoneNumbera}");
                response = JsonConvert.DeserializeObject<ResponseUnapprovedContract>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseUnapprovedContract>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseUnapprovedContract>(); }

            ResponseUnapprovedContract responseLogin = ConvertResponseObj<ResponseUnapprovedContract>(response);

            return responseLogin;
        }

        public async static Task<ResponseUnapprovedContract> SetUnapprovedContract(UnapprovedContract data)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestPostMethod(URL_SET_UNAPPROVED_CONTRACT, data);
                response = JsonConvert.DeserializeObject<ResponseUnapprovedContract>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseUnapprovedContract>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseUnapprovedContract>(); }

            ResponseUnapprovedContract responseLogin = ConvertResponseObj<ResponseUnapprovedContract>(response);

            return responseLogin;
        }

        public async static Task<ResponseApplicableContract> GetApplicableContract(string phoneNumbera)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestGetMethod($"{URL_GET_APPLICABLE_CONTRACT}{phoneNumbera}");
                response = JsonConvert.DeserializeObject<ResponseApplicableContract>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseApplicableContract>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseApplicableContract>(); }

            ResponseApplicableContract responseLogin = ConvertResponseObj<ResponseApplicableContract>(response);

            return responseLogin;
        }

        public async static Task<ResponseApplicableContract> SetApplicableContract(ApplicableContract data)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestPostMethod(URL_SET_APPLICABLE_CONTRACT, data);
                response = JsonConvert.DeserializeObject<ResponseApplicableContract>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseApplicableContract>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseApplicableContract>(); }

            ResponseApplicableContract responseLogin = ConvertResponseObj<ResponseApplicableContract>(response);

            return responseLogin;
        }

        public async static Task<ResponseCanceledContract> GetCanceledContract(string phoneNumbera)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestGetMethod($"{URL_GET_CANCELED_CONTRACTS}{phoneNumbera}");
                response = JsonConvert.DeserializeObject<ResponseCanceledContract>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseCanceledContract>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseCanceledContract>(); }

            ResponseCanceledContract responseLogin = ConvertResponseObj<ResponseCanceledContract>(response);

            return responseLogin;
        }

        public async static Task<ResponseCanceledContract> SetCanceledContract(CanceledContract data)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestPostMethod(URL_SET_CANCELED_CONTRACTS, data);
                response = JsonConvert.DeserializeObject<ResponseCanceledContract>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseCanceledContract>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseCanceledContract>(); }

            ResponseCanceledContract responseLogin = ConvertResponseObj<ResponseCanceledContract>(response);

            return responseLogin;
        }

        public async static Task<ResponseAboutApp> GetAboutApp(string lan_code)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestGetMethod($"{URL_GET_ABOUT_APP}{lan_code}");
                response = JsonConvert.DeserializeObject<ResponseAboutApp>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseAboutApp>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseAboutApp>(); }

            ResponseAboutApp responseLogin = ConvertResponseObj<ResponseAboutApp>(response);

            return responseLogin;
        }
         


        /// <summary>
        /// Save data to the server.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static async Task<string> RequestPostMethod(string url, Object obj)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(obj), ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteAsync(request);

            return response.Content;
        }

        private static async Task<string> RequestPutMethod(string url, Object obj)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var request = new RestRequest(Method.PUT);
            IRestResponse response = await client.ExecuteAsync(request);

            return response.Content;
        }

        /// <summary>
        /// Get data from server.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static async Task<string> RequestGetMethod(string url)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);

            return response.Content;
        }

        private static async Task<string> RequestGetMethod(string url, Object data)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var request = new RestRequest(Method.GET);

            request.AlwaysMultipartFormData = true;

            request.AddParameter("data", JsonConvert.SerializeObject(data));

            IRestResponse response = await client.ExecuteAsync(request);
            return response.Content;
        }

        private static T CreateResponseObj<T>() where T : IResponse, new()
        {
            T t = new T();
            t.Check();
            return t;
        }

        private static T ConvertResponseObj<T>(Response response) where T : IResponse, new()
        {
            T t = (T)Convert.ChangeType(response, typeof(T));
            if (t == null)
                t = new T();
            t.Check();

            return t;
        }
    }
     
    #region Request
    public class Login
    {
        public string phone_number { get; set; }
        public string password { get; set; }
    }

    public class User
    {
        public string phone_number { get; set; }
        public string password { get; set; }
        public string reg_date { get; set; }

        public void Copy(User other)
        {
            this.phone_number = other.phone_number;
            this.password = other.password;
            this.reg_date = other.reg_date;
        }
    }

    public class AboutApp
    {
        public string lan_code { get; set; }
        public string text { get; set; }

        public void Copy(AboutApp other)
        {
            this.lan_code = other.lan_code;
            this.text = other.text;
        }
    }

    public class ContractTableInfo
    {
        public string user_phone_number { get; set; }
        public string preparer { get; set; }
        public string contract_number { get; set; }
        public string company_contractor_name { get; set; }
        public string date_of_contract { get; set; }
        public string contract_price { get; set; }

        public void Copy(ContractTableInfo other)
        {
            this.user_phone_number = other.user_phone_number;
            this.preparer = other.preparer;
            this.contract_number = other.contract_number;
            this.company_contractor_name = other.company_contractor_name;
            this.date_of_contract = other.date_of_contract;
            this.contract_price = other.contract_price;
        }
    }

    public class ApplicableContract : ContractTableInfo
    {
        public string payment_percent { get; set; }

        public void Copy(ApplicableContract other)
        {
            base.Copy(other);
            this.payment_percent = other.payment_percent;
        }
    }

    public class CanceledContract : ContractTableInfo
    {
        public string comment { get; set; }
        public void Copy(CanceledContract other)
        {
            base.Copy(other);
            this.comment = other.comment;
        }
    }

    public class UnapprovedContract : ContractTableInfo
    {
        public void Copy(UnapprovedContract other)
        {
            base.Copy(other);
        }
    }

    public class PurposeOfContract
    {
        public string user_phone_number { get; set; }
        public string specify_service_type { get; set; }
        public string contract_number { get; set; }
        public string contract_currency { get; set; }
        public string amount_of_qqs { get; set; }
        public int is_there_excise_tax { get; set; }
        public string name_of_service_type { get; set; }
        public string unit_of_measure { get; set; }
        public int amount { get; set; }
        public int price { get; set; }
        public string total_cost_of_service { get; set; }
        public int public_offer { get; set; }

        public void Copy(PurposeOfContract other)
        {
            this.user_phone_number = other.user_phone_number;
            this.specify_service_type = other.specify_service_type;
            this.contract_number = other.contract_number;
            this.contract_currency = other.contract_currency;
            this.amount_of_qqs = other.amount_of_qqs;
            this.is_there_excise_tax = other.is_there_excise_tax;
            this.name_of_service_type = other.name_of_service_type;
            this.unit_of_measure = other.unit_of_measure;
            this.amount = other.amount;
            this.price = other.price;
            this.total_cost_of_service = other.total_cost_of_service;
            this.public_offer = other.public_offer;
        }
    }

    public class UserCompanyInfo
    {
        public string user_phone_number { get; set; }
        public string company_name { get; set; }
        public string address_of_company { get; set; }
        public string account_number { get; set; }
        public string ctr_of_company { get; set; }
        public string name_of_bank { get; set; }
        public string bank_code { get; set; }
        /// <summary>
        /// 1 = yes ,0 = no
        /// </summary>
        public int are_you_tax_payer { get; set; }
        public string qqs_number { get; set; }
        public string company_phone_number { get; set; }
        public string position_of_signer { get; set; }
        public string name_of_signer { get; set; }
        /// <summary>
        /// 1 = yes ,0 = no
        /// </summary>
        public int is_accountant_provided { get; set; }
        public string accountant_name { get; set; }
        /// <summary>
        /// 1 = yes ,0 = no
        /// </summary>
        public int is_legal_counsel_provided { get; set; }
        public string counsel_name { get; set; }

        public void Copy(UserCompanyInfo other)
        {
            this.user_phone_number = other.user_phone_number;
            this.company_name = other.company_name;
            this.address_of_company = other.address_of_company;
            this.account_number = other.account_number;
            this.ctr_of_company = other.ctr_of_company;
            this.name_of_bank = other.name_of_bank;
            this.bank_code = other.bank_code;
            this.are_you_tax_payer = other.are_you_tax_payer;
            this.qqs_number = other.qqs_number;
            this.company_phone_number = other.company_phone_number;
            this.position_of_signer = other.position_of_signer;
            this.name_of_signer = other.name_of_signer;
            this.is_accountant_provided = other.is_accountant_provided;
            this.accountant_name = other.accountant_name;
            this.is_legal_counsel_provided = other.is_legal_counsel_provided;
            this.counsel_name = other.counsel_name;
        }
    }
    #endregion

    #region Response
    public interface IResponse
    {
        void Check();
    }

    public class Response
    {
        public bool result { get; set; }
        public String message { get; set; }
        public int error_code { get; set; }

        public Response()
        {
            result = false;
            message = "";
            error_code = (int)HttpStatusCode.NotFound;
        }

        public virtual void Check()
        {
             
        }
    }

    public class ResponseLogin : Response, IResponse
    {
        public User userInfo { get; set; } = new User();
    }

    public class ResponseSignUp : Response, IResponse
    {
    }

    public class ResponseAboutApp : Response, IResponse
    {
        public AboutApp data { get; set; } = new AboutApp();
    }

    public class ResponseApplicableContract : Response, IResponse
    {
        public List<ApplicableContract> data { get; set; } = new List<ApplicableContract>();
    }

    public class ResponseCanceledContract : Response, IResponse
    {
        public CanceledContract data { get; set; } = new CanceledContract();
    }

    public class ResponsePurposeOfContract : Response, IResponse
    {
        public PurposeOfContract data { get; set; } = new PurposeOfContract();
    }

    public class ResponseUnapprovedContract : Response, IResponse
    {
        public List<UnapprovedContract> data { get; set; } = new List<UnapprovedContract>();
    }

    public class ResponseUser : Response, IResponse
    {
        public User data { get; set; } = new User();
    }

    public class ResponseUserCompanyInfo : Response, IResponse
    {
        public UserCompanyInfo data { get; set; } = new UserCompanyInfo();
    }
    #endregion
}
