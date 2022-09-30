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
using ContractAPI.Controllers.UnapprovedContracts.service;
using ContractAPI.UnapprovedContracts.service.impl;
using ContractAPI.ApplicableContracts.service.impl;
using ContractAPI.ApplicableContracts.service;
using ContractAPI.CompanyInformation.service;
using ContractAPI.CompanyInformation.service.impl;
using ContractAPI.CanceledContractInfo.service;
using ContractAPI.CanceledContractInfo.service.impl;
using ContractAPI.LoginSignUp.service;
using ContractAPI.LoginSignUp.service.impl;

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
            services.AddScoped<IUnapprovedContractService, UnapprovedContractService>();
            services.AddScoped<IApplicableContractService, ApplicableContractService>();
            services.AddScoped<ICompanyInfoService, CompanyInfoService>();
            services.AddScoped<ICanceledContractService, CanceledContractService>();
            services.AddScoped<ILoginSignUpService, LoginSignUpService>();

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
