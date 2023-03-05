using ContractAPI.DataAccess;
using ContractAPI.Helper;
using LibContract.HttpModels;
using LibContract.HttpResponse;
using Microsoft.EntityFrameworkCore; 
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ContractAPI.AppInfo.service.impl
{
    public class AppService : AppBaseService, IAppService
    {
        public AppService(ContractMakerContext db)
            :base(db)
        {
        }
         
        public async Task<ResponseAboutApp> getAboutApp(string lan_code)
        {
            ResponseAboutApp response = new ResponseAboutApp();
            response.data = null;
            AboutApp found = await dataBase.AboutApp.Where(item => item.lan_code.Equals(lan_code))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (found == null)
            {
                response.result = false;
                response.message = Constants.NotFound;
                response.error_code = (int)HttpStatusCode.NotFound;

                return response;
            }

            response.data = new AboutApp();
            response.data.Copy(found);

            response.result = true;
            response.error_code = (int)HttpStatusCode.OK;
             
            return response;
        }

        public Task<ResponseAboutApp> createPdf()
        {
            ResponseAboutApp response = new ResponseAboutApp();
            response.data = null;

            response.result = true;
            response.error_code = (int)HttpStatusCode.OK;

            response.message = RunCommand("wkhtmltopdf http://65.20.68.60:5000/Upload/ommabob.html ommabob.pdf");

            return Task.FromResult(response);
        }

        string RunCommand(string command)
        { 
            string result = "";
            using (Process proc = new Process())
            {
                proc.StartInfo.FileName = "/bin/bash";
                proc.StartInfo.Arguments = "-c \" " + command + " \"";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.Start();

                result += proc.StandardOutput.ReadToEnd();
                result += proc.StandardError.ReadToEnd();

                proc.WaitForExit();
            }
            return result;
        }
    }
}
