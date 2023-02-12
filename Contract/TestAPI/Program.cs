using LibContract;
using LibContract.HttpModels;
using NReco.PdfGenerator;
using System;
using System.Collections.Generic;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
//using TLSharp.Core;

namespace TestAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            TestHtmlPdf(); 
            Console.ReadLine();
        }

        static void SendMessage()
        {
            var accountSid = "ACaabae6218993aca7c7509cfdd1ee0654";
            var authToken = "a0b32fc81e2255eb2c8bafa17392348a";
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(new PhoneNumber("+821084410694"));
            messageOptions.Body = "Hello from Kontract maker";
            var message = MessageResource.Create(messageOptions);
            
            Console.WriteLine(message.Body);
        }

        static void TestPdfSharpCore()
        {
            CreateContractInfo contractInfo = new CreateContractInfo();
            contractInfo.contract_number = "ASAKA-00050";
            contractInfo.amount_of_qqs = "10";
            contractInfo.contract_currency = "Sum (UZS)";

            UserCompanyInfo userCompany = new UserCompanyInfo()
            {
                company_name = "Apilsin",
                address_of_company = "Toshkent shaxar, Afrosiob k",
                position_of_signer = "Direktor",
                account_number = "00050",
                name_of_bank = "ASAKA",
                bank_code = "123456",
                stir_of_company = "65821",
                company_phone_number = "+9987556354",
                name_of_signer = "Alisher Ibragimovich"
            };

            ClientCompanyInfo clientCompany = new ClientCompanyInfo()
            {
                company_name = "Anor",
                address_of_company = "Jizzax shaxar, Afrosiob k",
                position_of_signer = "Menejer",
                account_number = "01050",
                name_of_bank = "XUMO",
                bank_code = "987542",
                stir_of_company = "741258",
                company_phone_number = "+9987555566",
                name_of_signer = "Jabborov Ikrom o'gli"
            };

            ContractTemplate contractTemplate = new ContractTemplate()
            {
                contract_address = ""
            };

            List<ServicesInfo> serviceList = new List<ServicesInfo>();
            ServicesInfo item1 = new ServicesInfo()
            {
                name_of_service = "AAAAAAAAAAAAA XXXXXXXXXXXXXXX VVVVVVVVVVVV DDDDDDDDDDDDD RRRRRRRRRR",
                amount_value = 12,
                amount_value_price = "10"
            };
            ServicesInfo item2 = new ServicesInfo()
            {
                name_of_service = "Service 2",
                amount_value = 3,
                amount_value_price = "28"
            };
            ServicesInfo item3 = new ServicesInfo()
            {
                name_of_service = "Service 3",
                amount_value = 20,
                amount_value_price = "50"
            };
            ServicesInfo item4 = new ServicesInfo()
            {
                name_of_service = "Service 4",
                amount_value = 78,
                amount_value_price = "40"
            };
            ServicesInfo item5 = new ServicesInfo()
            {
                name_of_service = "Service 5",
                amount_value = 120,
                amount_value_price = "86"
            };
            ServicesInfo item6 = new ServicesInfo()
            {
                name_of_service = "Service 6",
                amount_value = 14,
                amount_value_price = "40"
            };
            ServicesInfo item7 = new ServicesInfo()
            {
                name_of_service = "Service 7",
                amount_value = 60,
                amount_value_price = "86"
            };
            ServicesInfo item8 = new ServicesInfo()
            {
                name_of_service = "Service 8",
                amount_value = 15,
                amount_value_price = "8"
            };
            ServicesInfo item9 = new ServicesInfo()
            {
                name_of_service = "Service 9",
                amount_value = 3,
                amount_value_price = "9"
            };
            serviceList.Add(item1);
            serviceList.Add(item2);
            serviceList.Add(item3);
            serviceList.Add(item4);
            serviceList.Add(item5);
            serviceList.Add(item6);
            serviceList.Add(item7);
            serviceList.Add(item8);
            serviceList.Add(item9);

            CreatePDF pdf = new CreatePDF(CreateClauses.Normal());
            pdf.ContractInfo = contractInfo;
            pdf.UserCompany = userCompany;
            pdf.ClientCompany = clientCompany;
            pdf.Template = contractTemplate;
            pdf.Services = serviceList;

            pdf.CreateContract("Contract.pdf", "");
        }

        static void TestHtmlPdf()
        {
            CreateContractInfo contractInfo = new CreateContractInfo();
            contractInfo.contract_number = "ASAKA-00050";
            contractInfo.amount_of_qqs = "10";
            contractInfo.contract_currency = "Sum (UZS)";

            UserCompanyInfo userCompany = new UserCompanyInfo()
            {
                company_name = "Apilsin",
                address_of_company = "Toshkent shaxar, Afrosiob k",
                position_of_signer = "Direktor",
                account_number = "00050",
                name_of_bank = "ASAKA",
                bank_code = "123456",
                stir_of_company = "65821",
                company_phone_number = "+9987556354",
                name_of_signer = "Alisher Ibragimovich"
            };

            ClientCompanyInfo clientCompany = new ClientCompanyInfo()
            {
                company_name = "Anor",
                address_of_company = "Jizzax shaxar, Afrosiob k",
                position_of_signer = "Menejer",
                account_number = "01050",
                name_of_bank = "XUMO",
                bank_code = "987542",
                stir_of_company = "741258",
                company_phone_number = "+9987555566",
                name_of_signer = "Jabborov Ikrom o'gli"
            };

            ContractTemplate contractTemplate = new ContractTemplate()
            {
                contract_address = "Qarshi"
            };

            List<ServicesInfo> serviceList = new List<ServicesInfo>();
            ServicesInfo item1 = new ServicesInfo()
            {
                name_of_service = "AAAAAAAAAAAAA XXXXXXXXXXXXXXX VVVVVVVVVVVV DDDDDDDDDDDDD RRRRRRRRRR",
                amount_value = 12,
                amount_value_price = "10"
            };
            ServicesInfo item2 = new ServicesInfo()
            {
                name_of_service = "Service 2",
                amount_value = 3,
                amount_value_price = "28"
            };
            ServicesInfo item3 = new ServicesInfo()
            {
                name_of_service = "Service 3",
                amount_value = 20,
                amount_value_price = "50"
            };
            ServicesInfo item4 = new ServicesInfo()
            {
                name_of_service = "Service 4",
                amount_value = 78,
                amount_value_price = "40"
            };
            ServicesInfo item5 = new ServicesInfo()
            {
                name_of_service = "Service 5",
                amount_value = 120,
                amount_value_price = "86"
            };
            ServicesInfo item6 = new ServicesInfo()
            {
                name_of_service = "Service 6",
                amount_value = 14,
                amount_value_price = "40"
            };
            ServicesInfo item7 = new ServicesInfo()
            {
                name_of_service = "Service 7",
                amount_value = 60,
                amount_value_price = "86"
            };
            ServicesInfo item8 = new ServicesInfo()
            {
                name_of_service = "Service 8",
                amount_value = 15,
                amount_value_price = "8"
            };
            ServicesInfo item9 = new ServicesInfo()
            {
                name_of_service = "Service 9",
                amount_value = 3,
                amount_value_price = "9"
            };
            serviceList.Add(item1);
            serviceList.Add(item2);
            serviceList.Add(item3);
            serviceList.Add(item4);
            serviceList.Add(item5);
            serviceList.Add(item6);
            serviceList.Add(item7);
            serviceList.Add(item8);
            serviceList.Add(item9);

            CreateHtmlPDF pdf = new CreateHtmlPDF(CreateClauses.Simple());
            pdf.ContractInfo = contractInfo;
            pdf.UserCompany = userCompany;
            pdf.ClientCompany = clientCompany;
            pdf.Template = contractTemplate;
            pdf.Services = serviceList;

            pdf.CreateContract("Contract.html");
        }

        static void TestLibrary()
        {
            var htmlToPdf = new HtmlToPdfConverter();
            htmlToPdf.GeneratePdfFromFile("http://65.20.68.60:5000/Upload/contract.html", null, "export.pdf");
        }
    }
}
