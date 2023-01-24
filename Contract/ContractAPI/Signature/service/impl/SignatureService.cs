using ContractAPI.DataAccess;
using ContractAPI.Helper;
using ContractAPI.Models;
using LibContract.HttpModels;
using LibContract.HttpResponse;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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

            try
            {
                string fileName = $"{info.phone_number}_sign.png";
                FileSystemControl.CreateFile($"{_environment.WebRootPath}{Constants.SaveSignImagePath}", fileName, info.dataStream);
                //Console.WriteLine($"PATH: {_environment.WebRootPath}{Constants.SaveSignImagePath}{fileName}");

                SignatureInfo newInfo = new SignatureInfo()
                {
                    phone_number = info.phone_number,
                    sign_url = $"{Constants.SaveSignImagePath}{fileName}"
                };

                SignatureInfo found = await dataBase.SignatureInfo.Where(item => item.phone_number.Equals(info.phone_number))
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
                
                if (found == null)
                    dataBase.SignatureInfo.Add(newInfo);
                else
                    dataBase.SignatureInfo.Update(newInfo);

                await dataBase.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.result = false;
                response.message = ex.Message;
                return response;
            }
            
            response.result = true;
            response.message = "Success";

            return response;
        }

        public async Task<ResponseSignatureInfo> checkSignature(string phone_number)
        {
            ResponseSignatureInfo response = new ResponseSignatureInfo();

            await Task.Run(() =>
            {
                string strFile = $"{_environment.WebRootPath}{Constants.SaveSignImagePath}{phone_number}_sign.png";
                if (File.Exists(strFile))
                {
                    response.result = true;
                    response.message = Constants.Exist;
                }
            });
             
            return response;
        }
    }
}
