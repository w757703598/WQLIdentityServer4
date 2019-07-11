using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using WQLIdentity.Application.Interfaces;

using WQLIdentityServerAPI.Configurations;
using WQLIdentityServerAPI.Middleware.Exceptions;

namespace WQLIdentityServerAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration,ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }
        private readonly ILogger _logger;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.ConfigAutoMapper();


            services.AddPolicies();

            services.ConfigIdentity(Configuration);

            services.ConfigIdentityServer(Configuration);

            //添加认证
            services.ConfigAuthentication(Configuration);

            services.AddCors(c => c.AddPolicy("Test", policy =>
            {
                policy
                 .AllowAnyOrigin()//允许任何源
                 .AllowAnyMethod()//允许任何方式
                 .AllowAnyHeader()//允许任何头
                 .AllowCredentials();//允许cookie
            }));





            services.ConfigSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("Test");

            app.UseStaticFiles();

            app.UseIdentityServer();

            app.ConfigureExceptionMidlleware(_logger);//异常捕获


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "KnowledgeBaseAPI");

                //c.RoutePrefix = string.Empty;

            });



            app.UseMvcWithDefaultRoute();

            
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {

            builder.RegisterModule(new AutofacModule());
        }



    }
}
