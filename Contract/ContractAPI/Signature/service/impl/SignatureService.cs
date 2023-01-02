using ContractAPI.DataAccess;
using ContractAPI.Helper;
using ContractAPI.Models;
using LibContract.HttpModels;
using LibContract.HttpResponse;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Signature.service.impl
{
    public class SignatureService : AppBaseService, ISignatureService
    {
        public static IWebHostEnvironment _environment;
        public SignatureService(ContractMakerContext db, IWebHostEnvironment environment)
        {
            dataBase = db;
            _environment = environment;
        }

        public async Task<ResponseSignatureInfo> setSignature(SignatureWithFile info)
        {
            ResponseSignatureInfo response = new ResponseSignatureInfo();

            FileSystemControl.CreateFile($"{_environment.WebRootPath}{Constants.SaveImagePath}", info.fileName, info.dataStream);
            response.result = true;
            response.message = "Success";

            return response;
        }
    }
}
