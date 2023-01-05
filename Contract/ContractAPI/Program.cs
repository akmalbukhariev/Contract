using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ContractAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //string sss = System.IO.Directory.GetCurrentDirectory();
            //string strPath = $"{System.IO.Directory.GetCurrentDirectory()}{Constants.SaveSignImagePath}";
            //string saveSignFile = $"{strPath}{12}_sign.png";

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
