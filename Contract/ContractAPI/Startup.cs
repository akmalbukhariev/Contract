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
using ContractAPI.ContractTemplateInfo.service;
using ContractAPI.ContractTemplateInfo.service.impl;
using ContractAPI.CreatePdf.service;
using ContractAPI.CreatePdf.service.impl;
using Microsoft.AspNetCore.Authorization;
using ContractAPI.Signature.service;
using ContractAPI.Signature.service.impl;

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
            services.AddScoped<IApprovedUnapprovedContractService, ApprovedUnapprovedContractService>();
            services.AddScoped<IContractServiceInfoService, ContractServiceInfoService>(); 
            services.AddScoped<ICanceledContractService, CanceledContractService>();
            services.AddScoped<IContractTemplateService, ContractTemplateService>();
            services.AddScoped<IOfferObjectionService, OfferObjectionService>();
            services.AddScoped<IContractNumberService, ContractNumberService>();
            services.AddScoped<ICompanyInfoService, CompanyInfoService>();
            services.AddScoped<ILoginSignUpService, LoginSignUpService>();
            services.AddScoped<ICreatePdfService, CreatePdfService>();
            services.AddScoped<ISignatureService, SignatureService>();
            services.AddScoped<IUserInfoService, UserInfoService>();
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IAppService, AppService>();

            services.AddControllers();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ContractAPI", Version = "v1" });
            //});

            //services.AddAuthorization(options =>
            //{
            //    options.FallbackPolicy = new AuthorizationPolicyBuilder()
            //        .RequireAuthenticatedUser()
            //        .Build();
            //});
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

            //Console.WriteLine($"ContentRootPath: {env.WebRootPath}");
            //var cacheMaxAgeOneWeek = (60 * 60 * 24 * 7).ToString();
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    //FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload")),
            //    FileProvider = new PhysicalFileProvider(Path.Combine(env.WebRootPath, "\\Upload")),
            //    RequestPath = new PathString("/Files"),
            //     
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
