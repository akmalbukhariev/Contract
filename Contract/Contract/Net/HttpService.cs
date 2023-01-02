using LibContract.HttpModels;
using LibContract.HttpResponse;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Net
{
    public static class StreamHelpers
    {
        public static byte[] ReadFully(this Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }

    public class HttpService
    {
        //http://192.168.219.102:5000/api/CreatePdf/createPdf/12_00002_Kukmin
        #region Url 
        public static string DATA_URL = "http://192.168.219.102:5000/";
        public static string SERVER_URL = "http://192.168.219.102:5000/api/";
        public static string URL_LOGIN = SERVER_URL + "LoginSignUp/login";
        public static string URL_SIGN_UP = SERVER_URL + "LoginSignUp/signUp";
        
        public static string URL_GET_USER = SERVER_URL + "UserInfo/getUser/"; //phoneNumber
        public static string URL_UPDATE_USER_PASSWORD = SERVER_URL + "UserInfo/updateUserPassword";
        public static string URL_UPDATE_DEFAULT_TMEPLATE = SERVER_URL + "UserInfo/updateDefaultTemplate";

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

        #region Approved and Unapproved contract
        public static string URL_GET_APPROVED_OR_UNAPPROVED_CONTRACT = SERVER_URL + "ApprovedUnapprovedContract/getApprovedOrUnapprovedContract"; 
        public static string URL_SET_APPROVED_CONTRACT = SERVER_URL + "ApprovedUnapprovedContract/setApprovedContract/";   //contract_number
        public static string URL_SET_UNAPPROVED_CONTRACT = SERVER_URL + "ApprovedUnapprovedContract/setUnapprovedContract/"; //contract_number
        #endregion
          
        #region Service Info
        public static string URL_SET_SERVICE_INFO = SERVER_URL + "ServiceInfo/setContractServiceInfo";
        public static string URL_DELETE_SERVICE_INFO = SERVER_URL + "ServiceInfo/deleteContractServiceInfo"; //contract_number
        #endregion

        #region Contract number
        public static string URL_GET_CONTRACT_NUMBER = SERVER_URL + "ContractNumber/geContractNumber/"; //phoneNumber
        public static string URL_SET_CONTRACT_NUMBER = SERVER_URL + "ContractNumber/setContractNumber";
        public static string URL_UPDATE_CONTRACT_NUMBER = SERVER_URL + "ContractNumber/updateContractNumber";
        #endregion

        #region Contract template
        public static string URL_GET_CONTRACT_TEMPLATE = SERVER_URL + "ContractTemplate/getContractTemplate/"; //phoneNumber
        public static string URL_SET_CONTRACT_TEMPLATE = SERVER_URL + "ContractTemplate/setContractTemplate";
        public static string URL_UPDATE_CONTRACT_TEMPLATE = SERVER_URL + "ContractTemplate/updateContractTemplate";
        public static string URL_DELETE_CONTRACT_TEMPLATE = SERVER_URL + "ContractTemplate/deleteContractTemplate";
        public static string URL_GET_ALLREADY_TEMPLATE = SERVER_URL + "ContractTemplate/getAllReadyTemplate";
        public static string URL_GET_ALLREADY_TEMPLATE_URL = SERVER_URL + "ContractTemplate/getAllReadyTemplateUrl";
        #endregion

        #region Contract
        public static string URL_GET_PURPOSE_OF_CONTRACT = SERVER_URL + "Contract/getPurposeOfContract/"; //phoneNumber
        public static string URL_SET_PURPOSE_OF_CONTRACT = SERVER_URL + "Contract/setPurposeOfContract";

        public static string URL_GET_NEW_CONTRACT_NUMBER = SERVER_URL + "Contract/getNewContractNumber/"; //phoneNumber
        public static string URL_CREATE_CONTRACT = SERVER_URL + "Contract/createContract";
        public static string URL_CANCEL_CONTRACT = SERVER_URL + "Contract/cancelContract";
        public static string URL_DELETE_CONTRACT = SERVER_URL + "Contract/deleteContract"; //contract_number
        public static string URL_CREATE_CONTRACT_PDF = SERVER_URL + "CreatePdf/createPdf/"; //contract_number
        #endregion

        public static string URL_GET_CANCELED_CONTRACTS = SERVER_URL + "Contract/getCanceledContract"; 
        public static string URL_SET_CANCELED_CONTRACTS = SERVER_URL + "CanceledContract/setCanceledContract";
        public static string URL_DELETE_CANCELED_CONTRACTS = SERVER_URL + "CanceledContract/deleteCanceledContract";
        public static string URL_SAVE_OFFER_OBJECTION = SERVER_URL + "OfferObjection/Save";
        public static string URL_SAVE_SIGNATURE = SERVER_URL + "Signature/setSignature";
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
            ResponseLogin response = new ResponseLogin();
            try
            {
                var receivedData = await RequestPostMethod(URL_LOGIN, data);
                response = JsonConvert.DeserializeObject<ResponseLogin>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseLogin>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseLogin>(); }
              
            return response;
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
            ResponseUser response = new ResponseUser();
            try
            {
                var receivedData = await RequestGetMethod($"{URL_GET_USER}{phoneNumbera}");
                response = JsonConvert.DeserializeObject<ResponseUser>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseUser>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseUser>(); }
             
            return response;
        }

        #region User company info
        public async static Task<ResponseUserCompanyInfo> GetUserCompanyInfo(string phoneNumbera)
        {
            ResponseUserCompanyInfo response = new ResponseUserCompanyInfo();
            try
            {
                var receivedData = await RequestGetMethod($"{URL_GET_USER_COMPANY_INFO}{phoneNumbera}");
                response = JsonConvert.DeserializeObject<ResponseUserCompanyInfo>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseUserCompanyInfo>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseUserCompanyInfo>(); }
             
            return response;
        }

        public async static Task<ResponseUserCompanyInfo> SetUserCompanyInfo(CompanyInfo data, bool hasFile = false)
        {
            ResponseUserCompanyInfo response = new ResponseUserCompanyInfo();
            try
            {
                string url = hasFile ? URL_SET_USER_COMPANY_INFO_WITH_FILE : URL_SET_USER_COMPANY_INFO;

                var receivedData = hasFile ? await RequestMethodWithFile(url, data, Method.POST) : 
                                             await RequestPostMethod(url, data);
                response = JsonConvert.DeserializeObject<ResponseUserCompanyInfo>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseUserCompanyInfo>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseUserCompanyInfo>(); }
             
            return response;
        }

        public async static Task<ResponseUserCompanyInfo> UpdateUserCompanyInfo(CompanyInfo data, bool hasFile = false)
        {
            ResponseUserCompanyInfo response = new ResponseUserCompanyInfo();
            try
            {
                string url = hasFile ? URL_UPDATE_USER_COMPANY_INFO_WITH_FILE : URL_UPDATE_USER_COMPANY_INFO;

                var receivedData = hasFile? await RequestMethodWithFile(url, data, Method.PUT) :  
                                            await RequestPutMethod(url, data);
                response = JsonConvert.DeserializeObject<ResponseUserCompanyInfo>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseUserCompanyInfo>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseUserCompanyInfo>(); }
             
            return response;
        }

        public async static Task<ResponseUserCompanyInfo> DeleteUserCompanyInfo(DeleteCompanyInfo data)
        {
            ResponseUserCompanyInfo response = new ResponseUserCompanyInfo();
            try
            {
                var receivedData = await RequestPostMethod(URL_DELETE_USER_COMPANY_INFO, data);
                response = JsonConvert.DeserializeObject<ResponseUserCompanyInfo>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseUserCompanyInfo>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseUserCompanyInfo>(); }
             
            return response;
        }
        #endregion

        #region Client company info
        public async static Task<ResponseClientCompanyInfo> GetClientCompanyInfo(string phoneNumbera)
        {
            ResponseClientCompanyInfo response = new ResponseClientCompanyInfo();
            try
            {
                var receivedData = await RequestGetMethod($"{URL_GET_CLIENT_COMPANY_INFO}{phoneNumbera}");
                response = JsonConvert.DeserializeObject<ResponseClientCompanyInfo>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseClientCompanyInfo>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseClientCompanyInfo>(); }
             
            return response;
        }

        public async static Task<ResponseClientCompanyInfo> SetClientCompanyInfo(CompanyInfo data, bool hasFile = false)
        {
            ResponseClientCompanyInfo response = new ResponseClientCompanyInfo();
            try
            {
                string url = hasFile ? URL_SET_CLIENT_COMPANY_INFO_WITH_FILE : URL_SET_CLIENT_COMPANY_INFO;

                var receivedData = hasFile? await RequestMethodWithFile(url, data, Method.POST) : await RequestPostMethod(url, data);
                response = JsonConvert.DeserializeObject<ResponseClientCompanyInfo>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseClientCompanyInfo>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseClientCompanyInfo>(); }
             
            return response;
        }

        public async static Task<ResponseClientCompanyInfo> UpdateClientCompanyInfo(CompanyInfo data, bool hasFile = false)
        {
            ResponseClientCompanyInfo response = new ResponseClientCompanyInfo();
            try
            {
                string url = hasFile ? URL_UPDATE_CLIENT_COMPANY_INFO_WITH_FILE : URL_UPDATE_CLIENT_COMPANY_INFO;

                var receivedData = hasFile? await RequestMethodWithFile(url, data, Method.PUT) : await RequestPutMethod (url, data);
                response = JsonConvert.DeserializeObject<ResponseClientCompanyInfo>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseClientCompanyInfo>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseClientCompanyInfo>(); }
             
            return response;
        }

        public async static Task<ResponseClientCompanyInfo> DeleteClientCompanyInfo(DeleteCompanyInfo data)
        {
            ResponseClientCompanyInfo response = new ResponseClientCompanyInfo();
            try
            {
                var receivedData = await RequestPostMethod(URL_DELETE_CLIENT_COMPANY_INFO, data);
                response = JsonConvert.DeserializeObject<ResponseClientCompanyInfo>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseClientCompanyInfo>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseClientCompanyInfo>(); }
             
            return response;
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

        #region Contract number
        public async static Task<ResponseContractNumberTemplate> GetContractNumber(string phoneNumbera)
        {
            ResponseContractNumberTemplate response = new ResponseContractNumberTemplate();
            try
            {
                var receivedData = await RequestGetMethod($"{URL_GET_CONTRACT_NUMBER}{phoneNumbera}");
                response = JsonConvert.DeserializeObject<ResponseContractNumberTemplate>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseContractNumberTemplate>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseContractNumberTemplate>(); }

            return response;
        }

        public async static Task<ResponseContractNumberTemplate> SetContractNumber(ContractNumberTemplate data)
        {
            ResponseContractNumberTemplate response = new ResponseContractNumberTemplate();
            try
            {
                var receivedData = await RequestPostMethod(URL_SET_CONTRACT_NUMBER, data);
                response = JsonConvert.DeserializeObject<ResponseContractNumberTemplate>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseContractNumberTemplate>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseContractNumberTemplate>(); }

            return response;
        }

        public async static Task<ResponseContractNumberTemplate> UpdateContractNumber(ContractNumberTemplate data)
        {
            ResponseContractNumberTemplate response = new ResponseContractNumberTemplate();
            try
            {
                var receivedData = await RequestPutMethod(URL_UPDATE_CONTRACT_NUMBER, data);
                response = JsonConvert.DeserializeObject<ResponseContractNumberTemplate>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseContractNumberTemplate>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseContractNumberTemplate>(); }

            return response;
        }
        #endregion

        #region Contract template
        public async static Task<ResponseContractTemplate> GetContractTemplate(string phoneNumbera)
        {
            ResponseContractTemplate response = new ResponseContractTemplate();
            try
            {
                var receivedData = await RequestGetMethod($"{URL_GET_CONTRACT_TEMPLATE}{phoneNumbera}");
                response = JsonConvert.DeserializeObject<ResponseContractTemplate>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseContractTemplate>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseContractTemplate>(); }

            return response;
        }

        public async static Task<ResponseContractTemplate> SetContractTemplate(ContractTemplate data)
        {
            ResponseContractTemplate response = new ResponseContractTemplate();
            try
            {
                var receivedData = await RequestPostMethod(URL_SET_CONTRACT_TEMPLATE, data);
                response = JsonConvert.DeserializeObject<ResponseContractTemplate>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseContractTemplate>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseContractTemplate>(); }

            return response;
        }

        public async static Task<ResponseContractTemplate> UpdateContractTemplate(ContractTemplate data)
        {
            ResponseContractTemplate response = new ResponseContractTemplate();
            try
            {
                var receivedData = await RequestPutMethod(URL_UPDATE_CONTRACT_TEMPLATE, data);
                response = JsonConvert.DeserializeObject<ResponseContractTemplate>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseContractTemplate>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseContractTemplate>(); }

            return response;
        }

        public async static Task<ResponseContractTemplate> DeleteContractTemplate(ContractTemplate data)
        {
            ResponseContractTemplate response = new ResponseContractTemplate();
            try
            {
                var receivedData = await RequestDeleteMethod(URL_DELETE_CONTRACT_TEMPLATE, data);
                response = JsonConvert.DeserializeObject<ResponseContractTemplate>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseContractTemplate>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseContractTemplate>(); }

            return response;
        }

        public async static Task<ResponseReadyTemplate> GetAllReadyTemplate()
        {
            ResponseReadyTemplate response = new ResponseReadyTemplate();
            try
            {
                var receivedData = await RequestGetMethod($"{URL_GET_ALLREADY_TEMPLATE}");
                response = JsonConvert.DeserializeObject<ResponseReadyTemplate>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseReadyTemplate>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseReadyTemplate>(); }

            return response;
        }

        public async static Task<ResponseReadyTemplate> GetAllReadyTemplateUrl()
        {
            ResponseReadyTemplate response = new ResponseReadyTemplate();
            try
            {
                var receivedData = await RequestGetMethod($"{URL_GET_ALLREADY_TEMPLATE_URL}");
                response = JsonConvert.DeserializeObject<ResponseReadyTemplate>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseReadyTemplate>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseReadyTemplate>(); }

            return response;
        }
        #endregion

        #region Contract
        public async static Task<ResponseCreateContract> GetNewContractNumber(string phoneNumber)
        {
            ResponseCreateContract response = new ResponseCreateContract();
            try
            {
                var receivedData = await RequestGetMethod($"{URL_GET_NEW_CONTRACT_NUMBER}{phoneNumber}");
                response = JsonConvert.DeserializeObject<ResponseCreateContract>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseCreateContract>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseCreateContract>(); }
            
            return response;
        }
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

        public async static Task<ResponseCreatePdf> CreateContractPdf(string contract_number)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestPostMethod($"{URL_CREATE_CONTRACT_PDF}{contract_number}");
                response = JsonConvert.DeserializeObject<ResponseCreatePdf>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseCreatePdf>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseCreatePdf>(); }

            ResponseCreatePdf responseLogin = ConvertResponseObj<ResponseCreatePdf>(response);

            return responseLogin;
        }
        #endregion

        public async static Task<ResponseSignatureInfo> SetSignature(SignatureInfo data)
        {
            ResponseSignatureInfo response = new ResponseSignatureInfo();
            try
            {
                var receivedData = await RequestPostSignature(URL_SAVE_SIGNATURE, data);
                response = JsonConvert.DeserializeObject<ResponseSignatureInfo>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseSignatureInfo>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseSignatureInfo>(); }

            return response;
        }

        public async static Task<ResponseLogin> UpdateUserPassword(ChnagePassword data)
        {
            ResponseLogin response = new ResponseLogin();
            try
            {
                var receivedData = await RequestPutMethod(URL_UPDATE_USER_PASSWORD, data);
                response = JsonConvert.DeserializeObject<ResponseLogin>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseLogin>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseLogin>(); }
             
            return response;
        }

        public async static Task<ResponseLogin> UpdateDefaultTemplate(User data)
        {
            ResponseLogin response = new ResponseLogin();
            try
            {
                var receivedData = await RequestPutMethod(URL_UPDATE_DEFAULT_TMEPLATE, data);
                response = JsonConvert.DeserializeObject<ResponseLogin>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseLogin>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseLogin>(); }

            return response;
        }

        public async static Task<ResponseOfferObjection> SaveOfferObjection(OfferAndObjection data)
        {
            Response response = new Response();
            try
            {
                var receivedData = await RequestPostMethod(URL_SAVE_OFFER_OBJECTION, data);
                response = JsonConvert.DeserializeObject<ResponseOfferObjection>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseOfferObjection>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseOfferObjection>(); }

            ResponseOfferObjection responseLogin = ConvertResponseObj<ResponseOfferObjection>(response);

            return responseLogin;
        }

        public async static Task<ResponsePurposeOfContract> GetPurposeOfCompany(string phoneNumbera)
        {
            ResponsePurposeOfContract response = new ResponsePurposeOfContract();
            try
            {
                var receivedData = await RequestGetMethod($"{URL_GET_PURPOSE_OF_CONTRACT}{phoneNumbera}");
                response = JsonConvert.DeserializeObject<ResponsePurposeOfContract>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponsePurposeOfContract>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponsePurposeOfContract>(); }
             
            return response;
        }

        public async static Task<ResponsePurposeOfContract> SetPurposeOfCompany(PurposeOfContract data)
        {
            ResponsePurposeOfContract response = new ResponsePurposeOfContract();
            try
            {
                var receivedData = await RequestPostMethod(URL_SET_PURPOSE_OF_CONTRACT, data);
                response = JsonConvert.DeserializeObject<ResponsePurposeOfContract>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponsePurposeOfContract>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponsePurposeOfContract>(); }
             
            return response;
        }

        #region #region Approved and Unapproved contract
        public async static Task<ResponseApprovedUnapprovedContract> GetApprovedOrUnapprovedContract(ApprovedUnapprovedContract data)
        {
            ResponseApprovedUnapprovedContract response = new ResponseApprovedUnapprovedContract();
            try
            {
                var receivedData = await RequestPostMethod(URL_GET_APPROVED_OR_UNAPPROVED_CONTRACT, data);
                response = JsonConvert.DeserializeObject<ResponseApprovedUnapprovedContract>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseApprovedUnapprovedContract>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseApprovedUnapprovedContract>(); }
             
            return response;
        }

        public async static Task<ResponseApprovedUnapprovedContract> SetApprovedContract(string contract_number)
        {
            ResponseApprovedUnapprovedContract response = new ResponseApprovedUnapprovedContract();
            try
            {
                var receivedData = await RequestPostMethod($"{URL_SET_APPROVED_CONTRACT}{contract_number}");
                response = JsonConvert.DeserializeObject<ResponseApprovedUnapprovedContract>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseApprovedUnapprovedContract>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseApprovedUnapprovedContract>(); }
             
            return response;
        }

        public async static Task<ResponseApprovedUnapprovedContract> SetUnapprovedContract(string contract_number)
        {
            ResponseApprovedUnapprovedContract response = new ResponseApprovedUnapprovedContract();
            try
            {
                var receivedData = await RequestPostMethod($"{URL_SET_UNAPPROVED_CONTRACT}{contract_number}");
                response = JsonConvert.DeserializeObject<ResponseApprovedUnapprovedContract>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseApprovedUnapprovedContract>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseApprovedUnapprovedContract>(); }
             
            return response;
        }
        #endregion
          
        public async static Task<ResponseCanceledContract> GetCanceledContract(CreateContractInfo data)
        {
            ResponseCanceledContract response = new ResponseCanceledContract();
            try
            {
                var receivedData = await RequestPostMethod(URL_GET_CANCELED_CONTRACTS, data);
                response = JsonConvert.DeserializeObject<ResponseCanceledContract>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseCanceledContract>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseCanceledContract>(); }
             
            return response;
        }

        public async static Task<ResponseCanceledContract> SetCanceledContract(CanceledContract data)
        {
            ResponseCanceledContract response = new ResponseCanceledContract();
            try
            {
                var receivedData = await RequestPostMethod(URL_SET_CANCELED_CONTRACTS, data);
                response = JsonConvert.DeserializeObject<ResponseCanceledContract>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseCanceledContract>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseCanceledContract>(); }
             
            return response;
        }

        public async static Task<ResponseAboutApp> GetAboutApp(string lan_code)
        {
            ResponseAboutApp response = new ResponseAboutApp();
            try
            {
                var receivedData = await RequestGetMethod($"{URL_GET_ABOUT_APP}{lan_code}");
                response = JsonConvert.DeserializeObject<ResponseAboutApp>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseAboutApp>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseAboutApp>(); }
             
            return response;
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

            request.AddParameter("id", obj.id);
            request.AddParameter("user_phone_number", obj.user_phone_number);
            request.AddParameter("company_name", obj.company_name);
            request.AddParameter("document", obj.document);
            request.AddParameter("document_index", obj.document_index);
            request.AddParameter("address_of_company", obj.address_of_company);
            request.AddParameter("account_number", obj.account_number);
            request.AddParameter("stir_of_company", obj.stir_of_company);
            request.AddParameter("name_of_bank", obj.name_of_bank);
            request.AddParameter("bank_code", obj.bank_code);
            request.AddParameter("are_you_qqs_payer", obj.are_you_qqs_payer);
            request.AddParameter("qqs_number", obj.qqs_number);
            request.AddParameter("company_phone_number", obj.company_phone_number);
            request.AddParameter("position_of_signer", obj.position_of_signer);
            request.AddParameter("position_of_signer_index", obj.position_of_signer_index);
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

        private static async Task<string> RequestPostSignature(string url, SignatureInfo obj)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var request = new RestRequest(Method.POST);

            request.AddParameter("fileName", obj.fileName); 
            request.AddFile("dataStream", obj.dataStream.ReadFully(), obj.fileName);

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

        private static async Task<string> RequestPostMethod(string url)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var request = new RestRequest(Method.POST);
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
}
