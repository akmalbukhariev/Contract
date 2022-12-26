using ContractAPI.DataAccess;
using ContractAPI.Helper;
using LibContract;
using LibContract.HttpModels;
using LibContract.HttpResponse;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ContractAPI.CreatePdf.service.impl
{
    public class CreatePdfService : AppBaseService, ICreatePdfService
    {
        public static IWebHostEnvironment _environment;
        public CreatePdfService(ContractMakerContext db, IWebHostEnvironment environment)
        {
            dataBase = db;
            _environment = environment;
        }

        public async Task<ResponseCreatePdf> createPdf(string contract_number)
        {
            ResponseCreatePdf response = new ResponseCreatePdf();

            CreateContractInfo contractInfo = await dataBase.CreateContractInfo
                .Where(item => item.contract_number.Equals(contract_number))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (contractInfo == null)
            {
                response.message = "Contract number is not exist.";
                response.error_code = (int)HttpStatusCode.NotFound;
                return response;
            }
            contractInfo.CheckNull();

            UserCompanyInfo userInfo = await dataBase.UserCompanyInfo
                .Where(item => item.user_phone_number.Equals(contractInfo.user_phone_number))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (userInfo == null)
            {
                response.message = "Cannot find user info.";
                response.error_code = (int)HttpStatusCode.NotFound;
                return response;
            }
            userInfo.CheckNull();

            ClientCompanyInfo clientInfo = await dataBase.ClientCompanyInfo
                .Where(item => item.stir_of_company.Equals(contractInfo.client_stir))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (clientInfo == null)
            {
                response.message = "Cannot find client info.";
                response.error_code = (int)HttpStatusCode.NotFound;
                return response;
            }
            clientInfo.CheckNull();

            ContractTemplate contractTemplate = await dataBase.ContractTemplate
                .Where(item => item.id == contractInfo.template_id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (contractTemplate == null)
            {
                response.message = "Cannot find template info.";
                response.error_code = (int)HttpStatusCode.NotFound;
                return response;
            }
            contractInfo.CheckNull();

            List<ServicesInfo> serviceList = await dataBase.ServicesInfo
                .Where(item => item.contract_number.Equals(contractInfo.contract_number))
                .AsNoTracking()
                .ToListAsync();

            CreatePDF pdf = new(contractTemplate.clauses);
            pdf.ContractInfo = contractInfo;
            pdf.UserCompany = userInfo;
            pdf.ClientCompany = clientInfo;
            pdf.Template = contractTemplate;
            pdf.Services = serviceList;

            //Console.WriteLine($"WebRootPath: {_environment.WebRootPath}");
            string strPath = Path.Combine(Directory.GetCurrentDirectory(), Constants.SaveContractPdfPath);
            string savePathFile = $"{strPath}{contractInfo.contract_number}.pdf";
            await Task.Run(() =>
            {
                pdf.CreateContract(savePathFile);
            });

            response.pdf_url = savePathFile;
            response.result = true;
            response.message = Constants.Success;

            return response;
        }
    }
}
