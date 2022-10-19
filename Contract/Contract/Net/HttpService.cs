using Contract.HttpModels;
using Contract.HttpResponse;
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
        public static string DATA_URL = "https://192.168.219.102:5001/";
        public static string SERVER_URL = "https://192.168.219.102:5001/api/";
        public static string URL_LOGIN = SERVER_URL + "LoginSignUp/login";
        public static string URL_SIGN_UP = SERVER_URL + "LoginSignUp/signUp";
        
        public static string URL_GET_USER = SERVER_URL + "UserInfo/getUser/"; //phoneNumber
        public static string URL_UPDATE_USER_PASSWORD = SERVER_URL + "UserInfo/updateUserPassword";

        #region User info
        public static string URL_GET_USER_COMPANY_INFO = SERVER_URL + "CompanyInfo/getUserCompanyInfo/"; //phoneNumber
        public static string URL_SET_USER_COMPANY_INFO = SERVER_URL + "CompanyInfo/setUserCompanyInfo";
        public static string URL_SET_USER_COMPANY_INFO_WITH_FILE = SERVER_URL + "CompanyInfo/setUserCompanyInfoWithFile";
        public static string URL_UPDATE_USER_COMPANY_INFO = SERVER_URL + "CompanyInfo/updateUserCompanyInfo";
        public static string URL_UPDATE_USER_COMPANY_INFO_WITH_FILE = SERVER_URL + "CompanyInfo/updateUserCompanyInfoWithFile";
        public static string URL_DELETE_USER_COMPANY_INFO = SERVER_URL + "CompanyInfo/deleteUserCompanyInfo";
        #endregion

        #region Client info
        public static string URL_GET_CLIENT_COMPANY_INFO = SERVER_URL + "CompanyInfo/getClientCompanyInfo/"; //phoneNumber
        public static string URL_SET_CLIENT_COMPANY_INFO = SERVER_URL + "CompanyInfo/setClientCompanyInfo";
        public static string URL_SET_CLIENT_COMPANY_INFO_WITH_FILE = SERVER_URL + "CompanyInfo/setClientCompanyInfoWithFile";
        public static string URL_UPDATE_CLIENT_COMPANY_INFO = SERVER_URL + "CompanyInfo/updateClientCompanyInfo";
        public static string URL_UPDATE_CLIENT_COMPANY_INFO_WITH_FILE = SERVER_URL + "CompanyInfo/updateClientCompanyInfoWithFile";
        public static string URL_DELETE_CLIENT_COMPANY_INFO = SERVER_URL + "CompanyInfo/deleteClientCompanyInfo";
        #endregion

        #region Unapproved contract
        public static string URL_GET_UNAPPROVED_CONTRACT = SERVER_URL + "UnapprovedContract/getUnapprovedContract/"; //phoneNumber
        public static string URL_SET_UNAPPROVED_CONTRACT = SERVER_URL + "UnapprovedContract/setUnapprovedContract";
        public static string URL_DELETE_UNAPPROVED_CONTRACT = SERVER_URL + "UnapprovedContract/deleteUnapprovedContract";
        public static string URL_DELETE_UNAPPROVED_CONTRACT_AND_SET_CANCELED_CONTRACT = SERVER_URL + "UnapprovedContract/deleteUnapprovedContractAndSetCanceledContract";
        #endregion

        #region Applicable contract
        public static string URL_GET_APPLICABLE_CONTRACT = SERVER_URL + "ApplicableContract/getApplicableContract/"; //phoneNumber
        public static string URL_SET_APPLICABLE_CONTRACT = SERVER_URL + "ApplicableContract/setApplicableContract";
        public static string URL_DELETE_APPLICABLE_CONTRACT = SERVER_URL + "ApplicableContract/deleteApplicableContract";
        public static string URL_DELETE_APPLICABLE_CONTRACT_AND_SET_CANCELED_CONTRACT = SERVER_URL + "ApplicableContract/deleteApplicableContractAndSetCanceledContract";
        #endregion

        #region Service Info
        public static string URL_SET_SERVICE_INFO = SERVER_URL + "ServiceInfo/setContractServiceInfo";
        public static string URL_DELETE_SERVICE_INFO = SERVER_URL + "ServiceInfo/deleteContractServiceInfo"; //contract_number
        #endregion

        public static string URL_GET_PURPOSE_OF_CONTRACT = SERVER_URL + "Contract/getPurposeOfContract/"; //phoneNumber
        public static string URL_SET_PURPOSE_OF_CONTRACT = SERVER_URL + "Contract/setPurposeOfContract";
        public static string URL_CREATE_CONTRACT = SERVER_URL + "Contract/createContract";
        public static string URL_CANCEL_CONTRACT = SERVER_URL + "Contract/cancelContract";
        public static string URL_DELETE_CONTRACT = SERVER_URL + "Contract/deleteContract"; //contract_number

        public static string URL_GET_CANCELED_CONTRACTS = SERVER_URL + "CanceledContract/getCanceledContract/"; //phoneNumber
        public static string URL_SET_CANCELED_CONTRACTS = SERVER_URL + "CanceledContract/setCanceledContract";
        public static string URL_DELETE_CANCELED_CONTRACTS = SERVER_URL + "CanceledContract/deleteCanceledContract";
        public static string URL_GET_ABOUT_APP = SERVER_URL + "App/getAboutApp/"; //lan_code
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

        #region User company info
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

        public async static Task<ResponseUserCompanyInfo> SetUserCompanyInfo(CompanyInfo data, bool hasFile = false)
        {
            Response response = new Response();
            try
            {
                string url = hasFile ? URL_SET_USER_COMPANY_INFO_WITH_FILE : URL_SET_USER_COMPANY_INFO;

                var receivedData = hasFile ? await RequestMethodWithFile(url, data, Method.POST) : 
                                             await RequestPostMethod(url, data);
                response = JsonConvert.DeserializeObject<ResponseUserCompanyInfo>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseUserCompanyInfo>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseUserCompanyInfo>(); }

            ResponseUserCompanyInfo responseLogin = ConvertResponseObj<ResponseUserCompanyInfo>(response);

            return responseLogin;
        }

        public async static Task<ResponseUserCompanyInfo> UpdateUserCompanyInfo(CompanyInfo data, bool hasFile = false)
        {
            Response response = new Response();
            try
            {
                string url = hasFile ? URL_UPDATE_USER_COMPANY_INFO_WITH_FILE : URL_UPDATE_USER_COMPANY_INFO;

                var receivedData = hasFile? await RequestMethodWithFile(url, data, Method.PUT) :  
                                            await RequestPutMethod(url, data);
                response = JsonConvert.DeserializeObject<ResponseUserCompanyInfo>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseUserCompanyInfo>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseUserCompanyInfo>(); }

            ResponseUserCompanyInfo responseLogin = ConvertResponseObj<ResponseUserCompanyInfo>(response);

            return responseLogin;
        }

        public async static Task<ResponseUserCompanyInfo> DeleteUserCompanyInfo(DeleteCompanyInfo data)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestPostMethod(URL_DELETE_USER_COMPANY_INFO, data);
                response = JsonConvert.DeserializeObject<ResponseUserCompanyInfo>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseUserCompanyInfo>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseUserCompanyInfo>(); }

            ResponseUserCompanyInfo responseLogin = ConvertResponseObj<ResponseUserCompanyInfo>(response);

            return responseLogin;
        }
        #endregion

        #region Client company info
        public async static Task<ResponseClientCompanyInfo> GetClientCompanyInfo(string phoneNumbera)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestGetMethod($"{URL_GET_CLIENT_COMPANY_INFO}{phoneNumbera}");
                response = JsonConvert.DeserializeObject<ResponseClientCompanyInfo>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseClientCompanyInfo>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseClientCompanyInfo>(); }

            ResponseClientCompanyInfo responseLogin = ConvertResponseObj<ResponseClientCompanyInfo>(response);

            return responseLogin;
        }

        public async static Task<ResponseClientCompanyInfo> SetClientCompanyInfo(CompanyInfo data, bool hasFile = false)
        {
            Response response = new Response();
            try
            {
                string url = hasFile ? URL_SET_CLIENT_COMPANY_INFO_WITH_FILE : URL_SET_CLIENT_COMPANY_INFO;

                var receivedData = hasFile? await RequestMethodWithFile(url, data, Method.POST) : 
                                            await RequestPostMethod(url, data);
                response = JsonConvert.DeserializeObject<ResponseClientCompanyInfo>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseClientCompanyInfo>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseClientCompanyInfo>(); }

            ResponseClientCompanyInfo responseLogin = ConvertResponseObj<ResponseClientCompanyInfo>(response);

            return responseLogin;
        }

        public async static Task<ResponseClientCompanyInfo> UpdateClientCompanyInfo(CompanyInfo data, bool hasFile = false)
        {
            Response response = new Response();
            try
            {
                string url = hasFile ? URL_UPDATE_CLIENT_COMPANY_INFO_WITH_FILE : URL_UPDATE_CLIENT_COMPANY_INFO;

                var receivedData = hasFile? await RequestMethodWithFile(url, data, Method.PUT) : 
                                            await RequestPutMethod (url, data);
                response = JsonConvert.DeserializeObject<ResponseClientCompanyInfo>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseClientCompanyInfo>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseClientCompanyInfo>(); }

            ResponseClientCompanyInfo responseLogin = ConvertResponseObj<ResponseClientCompanyInfo>(response);

            return responseLogin;
        }

        public async static Task<ResponseClientCompanyInfo> DeleteClientCompanyInfo(DeleteCompanyInfo data)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestPostMethod(URL_DELETE_CLIENT_COMPANY_INFO, data);
                response = JsonConvert.DeserializeObject<ResponseClientCompanyInfo>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseClientCompanyInfo>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseClientCompanyInfo>(); }

            ResponseClientCompanyInfo responseLogin = ConvertResponseObj<ResponseClientCompanyInfo>(response);

            return responseLogin;
        }
        #endregion

        #region Service Info
        public async static Task<ResponseServiceInfo> SetServiceInfo(List<ServicesInfo> data)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestPostMethod(URL_SET_SERVICE_INFO, data);
                response = JsonConvert.DeserializeObject<ResponseServiceInfo>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseServiceInfo>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseServiceInfo>(); }

            ResponseServiceInfo responseLogin = ConvertResponseObj<ResponseServiceInfo>(response);

            return responseLogin;
        }

        public async static Task<ResponseServiceInfo> DeleteServiceinfo(string contract_number)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestDeleteMethod($"{URL_DELETE_SERVICE_INFO}{contract_number}");
                response = JsonConvert.DeserializeObject<ResponseServiceInfo>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseServiceInfo>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseServiceInfo>(); }

            ResponseServiceInfo responseLogin = ConvertResponseObj<ResponseServiceInfo>(response);

            return responseLogin;
        }
        #endregion

        public async static Task<ResponseCreateContract> CreateContract(CreateContractInfo data)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestPostMethod(URL_CREATE_CONTRACT, data);
                response = JsonConvert.DeserializeObject<ResponseCreateContract>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseCreateContract>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseCreateContract>(); }

            ResponseCreateContract responseLogin = ConvertResponseObj<ResponseCreateContract>(response);

            return responseLogin;
        }

        public async static Task<ResponseCreateContract> CancelContract(CreateContractInfo data)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestPutMethod(URL_CANCEL_CONTRACT, data);
                response = JsonConvert.DeserializeObject<ResponseCreateContract>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseCreateContract>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseCreateContract>(); }

            ResponseCreateContract responseLogin = ConvertResponseObj<ResponseCreateContract>(response);

            return responseLogin;
        }

        public async static Task<ResponseCreateContract> DeleteContract(string contract_number)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestDeleteMethod($"{URL_DELETE_CONTRACT}{contract_number}");
                response = JsonConvert.DeserializeObject<ResponseCreateContract>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseCreateContract>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseCreateContract>(); }

            ResponseCreateContract responseLogin = ConvertResponseObj<ResponseCreateContract>(response);

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
                var receivedData = await RequestGetMethod($"{URL_GET_PURPOSE_OF_CONTRACT}{phoneNumbera}");
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
                var receivedData = await RequestPostMethod(URL_SET_PURPOSE_OF_CONTRACT, data);
                response = JsonConvert.DeserializeObject<ResponsePurposeOfContract>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponsePurposeOfContract>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponsePurposeOfContract>(); }

            ResponsePurposeOfContract responseLogin = ConvertResponseObj<ResponsePurposeOfContract>(response);

            return responseLogin;
        }

        #region Unapproved
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

        public async static Task<ResponseUnapprovedContract> DeleteUnapprovedContract(UnapprovedContract data)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestDeleteMethod(URL_DELETE_UNAPPROVED_CONTRACT, data);
                response = JsonConvert.DeserializeObject<ResponseUnapprovedContract>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseUnapprovedContract>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseUnapprovedContract>(); }

            ResponseUnapprovedContract responseLogin = ConvertResponseObj<ResponseUnapprovedContract>(response);

            return responseLogin;
        }

        public async static Task<ResponseCanceledContract> DeleteUnapprovedContractAndSetCanceledContract(CanceledContract data)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestDeleteMethod(URL_DELETE_UNAPPROVED_CONTRACT_AND_SET_CANCELED_CONTRACT, data);
                response = JsonConvert.DeserializeObject<ResponseCanceledContract>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseCanceledContract>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseCanceledContract>(); }

            ResponseCanceledContract responseLogin = ConvertResponseObj<ResponseCanceledContract>(response);

            return responseLogin;
        }
        #endregion
         
        #region ApplicableContract
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

        public async static Task<ResponseApplicableContract> DeleteApplicableContract(ApplicableContract data)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestDeleteMethod(URL_DELETE_APPLICABLE_CONTRACT, data);
                response = JsonConvert.DeserializeObject<ResponseApplicableContract>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseApplicableContract>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseApplicableContract>(); }

            ResponseApplicableContract responseLogin = ConvertResponseObj<ResponseApplicableContract>(response);

            return responseLogin;
        }

        public async static Task<ResponseCanceledContract> DeleteApplicableContractAndSetCanceledContract(CanceledContract data)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestDeleteMethod(URL_DELETE_APPLICABLE_CONTRACT_AND_SET_CANCELED_CONTRACT, data);
                response = JsonConvert.DeserializeObject<ResponseCanceledContract>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseCanceledContract>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseCanceledContract>(); }

            ResponseCanceledContract responseLogin = ConvertResponseObj<ResponseCanceledContract>(response);

            return responseLogin;
        }
        #endregion
         
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

        /// <summary>
        /// This method suitable for POST and PUT
        /// </summary>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <param name="hasFile"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private static async Task<string> RequestMethodWithFile(string url, CompanyInfo obj, Method method)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var request = new RestRequest(method);
             
            request.AddParameter("user_phone_number", obj.user_phone_number);
            request.AddParameter("company_name", obj.company_name);
            request.AddParameter("address_of_company", obj.address_of_company);
            request.AddParameter("account_number", obj.account_number);
            request.AddParameter("stir_of_company", obj.stir_of_company);
            request.AddParameter("name_of_bank", obj.name_of_bank);
            request.AddParameter("bank_code", obj.bank_code);
            request.AddParameter("are_you_qqs_payer", obj.are_you_qqs_payer);
            request.AddParameter("qqs_number", obj.qqs_number);
            request.AddParameter("company_phone_number", obj.company_phone_number);
            request.AddParameter("position_of_signer", obj.position_of_signer);
            request.AddParameter("name_of_signer", obj.name_of_signer);
            request.AddParameter("is_accountant_provided", obj.is_accountant_provided);
            request.AddParameter("accountant_name", obj.accountant_name);
            request.AddParameter("is_legal_counsel_provided", obj.is_legal_counsel_provided);
            request.AddParameter("counsel_name", obj.counsel_name);
            request.AddFile("company_logo_url", obj.company_logo_url);
            request.AddParameter("created_date", "");
              
            IRestResponse response = await client.ExecuteAsync(request);
            return response.Content;
        }

        private static async Task<string> RequestDeleteMethod(string url, Object obj)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(obj), ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteAsync(request);

            return response.Content;
        }

        private static async Task<string> RequestDeleteMethod(string url)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var request = new RestRequest(Method.DELETE);
            IRestResponse response = await client.ExecuteAsync(request);

            return response.Content;
        }

        private static async Task<string> RequestPutMethod(string url, Object obj)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(obj), ParameterType.RequestBody);
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
            //t.Check();
            return t;
        }

        private static T ConvertResponseObj<T>(Response response) where T : IResponse, new()
        {
            T t = (T)Convert.ChangeType(response, typeof(T));
            if (t == null)
                t = new T();
            //t.Check();

            return t;
        }
    }
     
    //#region Request
    //public class Login
    //{
    //    public string phone_number { get; set; }
    //    public string password { get; set; }
    //}

    //public class User
    //{
    //    public string phone_number { get; set; }
    //    public string password { get; set; }
    //    public string reg_date { get; set; }

    //    public void Copy(User other)
    //    {
    //        this.phone_number = other.phone_number;
    //        this.password = other.password;
    //        this.reg_date = other.reg_date;
    //    }
    //}

    //public class AboutApp
    //{
    //    public string lan_code { get; set; }
    //    public string text { get; set; }

    //    public void Copy(AboutApp other)
    //    {
    //        this.lan_code = other.lan_code;
    //        this.text = other.text;
    //    }
    //}

    //public class BaseContractTableInfo
    //{
    //    public string user_phone_number { get; set; }
    //    public string preparer { get; set; }
    //    public string contract_number { get; set; }
    //    public string company_contractor_name { get; set; }
    //    public string date_of_contract { get; set; }
    //    public string contract_price { get; set; }

    //    public void Copy(BaseContractTableInfo other)
    //    {
    //        this.user_phone_number = other.user_phone_number;
    //        this.preparer = other.preparer;
    //        this.contract_number = other.contract_number;
    //        this.company_contractor_name = other.company_contractor_name;
    //        this.date_of_contract = other.date_of_contract;
    //        this.contract_price = other.contract_price;
    //    }
    //}

    //public class ApplicableContract : BaseContractTableInfo
    //{
    //    public string payment_percent { get; set; }

    //    public void Copy(ApplicableContract other)
    //    {
    //        base.Copy(other);
    //        this.payment_percent = other.payment_percent;
    //    }
    //}

    //public class CanceledContract //: ContractTableInfo
    //{
    //    public string user_phone_number { get; set; }
    //    public string preparer { get; set; }
    //    public string contract_number { get; set; } 
    //    public string client_company_name { get; set; }
    //    public string user_company_name { get; set; }
    //    public string open_client_info { get; set; }
    //    public string open_search_client { get; set; }
    //    public string client_stir { get; set; }
    //    public string service_type { get; set; }
    //    public int service_type_indx { get; set; }
    //    public string contract_currency { get; set; }
    //    public int contract_currency_index { get; set; }
    //    public string amount_of_qqs { get; set; }
    //    public string amount_of_qqs_index { get; set; }
    //    public string is_execise_tax { get; set; }
    //    public string interest_text { get; set; } 
    //    public string total_cost_text { get; set; }
    //    public string agree { get; set; } 
    //    public string comment { get; set; }
    //    public string created_date { get; set; }

    //    public void Copy(CanceledContract other)
    //    {
    //        user_phone_number = other.user_phone_number;
    //        preparer = other.preparer;
    //        contract_number = other.contract_number;
    //        client_company_name = other.client_company_name;
    //        user_company_name = other.user_company_name;
    //        open_client_info = other.open_client_info;
    //        open_search_client = other.open_search_client;
    //        client_stir = other.client_stir;
    //        service_type = other.service_type;
    //        service_type_indx = other.service_type_indx;
    //        contract_currency = other.contract_currency;
    //        contract_currency_index = other.contract_currency_index;
    //        amount_of_qqs = other.amount_of_qqs;
    //        amount_of_qqs_index = other.amount_of_qqs_index;
    //        is_execise_tax = other.is_execise_tax;
    //        interest_text = other.interest_text;
    //        total_cost_text = other.total_cost_text;
    //        agree = other.agree;
    //        comment = other.comment;
    //        created_date = other.created_date;
    //    }
    //}

    //public class UnapprovedContract : BaseContractTableInfo
    //{
    //    public void Copy(UnapprovedContract other)
    //    {
    //        base.Copy(other);
    //    }
    //}

    //public class PurposeOfContract
    //{
    //    public string user_phone_number { get; set; }
    //    public string specify_service_type { get; set; }
    //    public string contract_number { get; set; }
    //    public string contract_currency { get; set; }
    //    public string amount_of_qqs { get; set; }
    //    public int is_there_excise_tax { get; set; }
    //    public string name_of_service_type { get; set; }
    //    public string unit_of_measure { get; set; }
    //    public int amount { get; set; }
    //    public int price { get; set; }
    //    public string total_cost_of_service { get; set; }
    //    public int public_offer { get; set; }

    //    public void Copy(PurposeOfContract other)
    //    {
    //        this.user_phone_number = other.user_phone_number;
    //        this.specify_service_type = other.specify_service_type;
    //        this.contract_number = other.contract_number;
    //        this.contract_currency = other.contract_currency;
    //        this.amount_of_qqs = other.amount_of_qqs;
    //        this.is_there_excise_tax = other.is_there_excise_tax;
    //        this.name_of_service_type = other.name_of_service_type;
    //        this.unit_of_measure = other.unit_of_measure;
    //        this.amount = other.amount;
    //        this.price = other.price;
    //        this.total_cost_of_service = other.total_cost_of_service;
    //        this.public_offer = other.public_offer;
    //    }
    //}

    //public class CompanyInfo
    //{
    //    public string user_phone_number { get; set; }
    //    /// <summary>
    //    /// 1 = yes, 0 = no
    //    /// </summary>
    //    public string company_name { get; set; }
    //    public string address_of_company { get; set; }
    //    public string account_number { get; set; }
    //    public string stir_of_company { get; set; }
    //    public string name_of_bank { get; set; }
    //    public string bank_code { get; set; }
    //    /// <summary>
    //    /// 1 = yes, 0 = no
    //    /// </summary>
    //    public int are_you_qqs_payer { get; set; }
    //    public string qqs_number { get; set; }
    //    public string company_phone_number { get; set; }
    //    public string position_of_signer { get; set; }
    //    public int position_of_signer_index { get; set; }
    //    public string name_of_signer { get; set; }
    //    /// <summary>
    //    /// 1 = yes, 0 = no
    //    /// </summary>
    //    public int is_accountant_provided { get; set; }
    //    public string accountant_name { get; set; }
    //    /// <summary>
    //    /// 1 = yes, 0 = no
    //    /// </summary>
    //    public int is_legal_counsel_provided { get; set; }
    //    public string counsel_name { get; set; }
    //    public string company_logo_url { get; set; }
    //    public string created_date { get; set; }

    //    public CompanyInfo()
    //    {
            
    //    }

    //    public CompanyInfo(CompanyInfo other)
    //    {
    //        this.Copy(other);
    //    }

    //    public void Copy(CompanyInfo other)
    //    {
    //        this.user_phone_number = other.user_phone_number;
    //        this.company_name = other.company_name;
    //        this.address_of_company = other.address_of_company;
    //        this.account_number = other.account_number;
    //        this.stir_of_company = other.stir_of_company;
    //        this.name_of_bank = other.name_of_bank;
    //        this.bank_code = other.bank_code;
    //        this.are_you_qqs_payer = other.are_you_qqs_payer;
    //        this.qqs_number = other.qqs_number;
    //        this.company_phone_number = other.company_phone_number;
    //        this.position_of_signer = other.position_of_signer;
    //        this.position_of_signer_index = other.position_of_signer_index;
    //        this.name_of_signer = other.name_of_signer;
    //        this.is_accountant_provided = other.is_accountant_provided;
    //        this.accountant_name = other.accountant_name;
    //        this.is_legal_counsel_provided = other.is_legal_counsel_provided;
    //        this.counsel_name = other.counsel_name;
    //        this.company_logo_url = other.company_logo_url;
    //        this.created_date = other.created_date;
    //    }
    //}

    //public class CreateContractInfo
    //{
    //    #region Properties
    //    public string user_phone_number { get; set; }
    //    public int open_client_info { get; set; }
    //    public int open_search_client { get; set; }
    //    public string client_stir { get; set; }
    //    public string client_company_name { get; set; }
    //    public string user_company_name { get; set; }
    //    public string service_type { get; set; }
    //    public string contract_number { get; set; }
    //    public string contract_currency { get; set; }
    //    public string amount_of_qqs { get; set; }
    //    public int is_execise_tax { get; set; }
    //    public string interest_text { get; set; }
    //    public string total_cost_text { get; set; }
    //    public int agree { get; set; }
    //    public string created_date { get; set; }
    //    #endregion

    //    public CreateContractInfo()
    //    {

    //    }

    //    public CreateContractInfo(CreateContractInfo other)
    //    {
    //        this.Copy(other);
    //    }

    //    public void Copy(CreateContractInfo other)
    //    {
    //        this.user_phone_number = other.user_phone_number;
    //        this.open_client_info = other.open_client_info;
    //        this.open_search_client = other.open_search_client;
    //        this.client_stir = other.client_stir;
    //        this.client_company_name = other.client_company_name;
    //        this.user_company_name = other.user_company_name;
    //        this.service_type = other.service_type;
    //        this.contract_number = other.contract_number;
    //        this.contract_currency = other.contract_currency;
    //        this.amount_of_qqs = other.amount_of_qqs;
    //        this.is_execise_tax = other.is_execise_tax;
    //        this.interest_text = other.interest_text;
    //        this.total_cost_text = other.total_cost_text;
    //        this.agree = other.agree;
    //        this.created_date = other.created_date;
    //    }
    //}

    //public class ServicesInfo
    //{
    //    public string contract_number { get; set; }
    //    public string name_of_service { get; set; }
    //    public string unit_of_measure { get; set; }
    //    public int amount_value { get; set; }
    //    public string amount_value_price { get; set; }
    //    public string currency { get; set; }
    //    public string created_date { get; set; }

    //    public ServicesInfo()
    //    {

    //    }

    //    public ServicesInfo(ServicesInfo other)
    //    {
    //        this.Copy(other);
    //    }

    //    public void Copy(ServicesInfo other)
    //    {
    //        this.contract_number = other.contract_number;
    //        this.name_of_service = other.name_of_service;
    //        this.unit_of_measure = other.unit_of_measure;
    //        this.amount_value = other.amount_value;
    //        this.amount_value_price = other.amount_value_price;
    //        this.currency = other.currency;
    //        this.created_date = other.created_date;
    //    }
    //}

    //public class CreateContract
    //{
    //    public CompanyInfo client_company_info { get; set; }
    //    public CreateContractInfo contract_info { get; set; }
    //    public List<ServicesInfo> service_list { get; set; }
    //}
    //#endregion

    //#region Response
    //public interface IResponse
    //{
    //    void Check();
    //}

    //public class Response
    //{
    //    public bool result { get; set; }
    //    public String message { get; set; }
    //    public int error_code { get; set; }

    //    public Response()
    //    {
    //        result = false;
    //        message = "";
    //        error_code = (int)HttpStatusCode.NotFound;
    //    }

    //    public virtual void Check()
    //    {
             
    //    }
    //}

    //public class ResponseLogin : Response, IResponse
    //{
    //    public User userInfo { get; set; } = new User();
    //}

    //public class ResponseSignUp : Response, IResponse
    //{
    //}

    //public class ResponseAboutApp : Response, IResponse
    //{
    //    public AboutApp data { get; set; } = new AboutApp();
    //}

    //public class ResponseApplicableContract : Response, IResponse
    //{
    //    public List<ApplicableContract> data { get; set; } = new List<ApplicableContract>();
    //}

    //public class ResponseCanceledContract : Response, IResponse
    //{
    //    public List<CanceledContract> data { get; set; } = new List<CanceledContract>();
    //}

    //public class ResponsePurposeOfContract : Response, IResponse
    //{
    //    public PurposeOfContract data { get; set; } = new PurposeOfContract();
    //}

    //public class ResponseUnapprovedContract : Response, IResponse
    //{
    //    public List<UnapprovedContract> data { get; set; } = new List<UnapprovedContract>();
    //}

    //public class ResponseUser : Response, IResponse
    //{
    //    public User data { get; set; } = new User();
    //}

    //public class ResponseUserCompanyInfo : Response, IResponse
    //{
    //    public CompanyInfo data { get; set; } = new CompanyInfo();
    //}

    //public class ResponseClientCompanyInfo : Response, IResponse
    //{
    //    public List<CompanyInfo> data { get; set; } = new List<CompanyInfo>();
    //}

    //public class ResponseCreateContract : Response, IResponse
    //{

    //}
    //#endregion
}
