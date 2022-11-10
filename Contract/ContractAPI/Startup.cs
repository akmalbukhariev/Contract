using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ContractAPI.DataAccess; 
 
using ContractAPI.CompanyInformation.service;
using ContractAPI.CompanyInformation.service.impl;
using ContractAPI.CanceledContractInfo.service;
using ContractAPI.CanceledContractInfo.service.impl;
using ContractAPI.LoginSignUp.service;
using ContractAPI.LoginSignUp.service.impl;
using ContractAPI.AppInfo.service;
using ContractAPI.AppInfo.service.impl;
using ContractAPI.ContractInfo.service.impl;
using ContractAPI.ContractInfo.service;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using ContractAPI.ContractServices.service;
using ContractAPI.ContractServices.service.impl;
using ContractAPI.ApprovedUnapprovedContract.service;
using ContractAPI.ApprovedUnapprovedContract.service.impl; 
using ContractAPI.Users.service;
using ContractAPI.Users.service.impl;
using ContractAPI.OfferObjection.service;
using ContractAPI.OfferObjection.service.impl;
using ContractAPI.ContractNumberInfo.service;
using ContractAPI.ContractNumberInfo.service.impl;

namespace ContractAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        { 
            services.AddDbContext<ContractMakerContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("Default"));
            });
            services.AddScoped<IContractServiceInfoService, ContractServiceInfoService>(); 
            services.AddScoped<IApprovedUnapprovedContractService, ApprovedUnapprovedContractService>();
            services.AddScoped<ICanceledContractService, CanceledContractService>();
            services.AddScoped<IOfferObjectionService, OfferObjectionService>();
            services.AddScoped<IContractNumberService, ContractNumberService>();
            services.AddScoped<ICompanyInfoService, CompanyInfoService>();
            services.AddScoped<ILoginSignUpService, LoginSignUpService>();
            services.AddScoped<IUserInfoService, UserInfoService>();
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IAppService, AppService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ContractAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ContractAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\images")),
            //    RequestPath = new PathString("/images")
            //});

            //app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload")),
            //    RequestPath = new PathString("/Upload")
            //});

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
