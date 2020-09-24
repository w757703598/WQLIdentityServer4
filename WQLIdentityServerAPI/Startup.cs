using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AntennaKnowledgeBase.Api.Middleware;
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
        private readonly string AllowAnyDomain = "AllowAnyDomain";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }
        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers()
             .AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());

             });

            services.ConfigAuthentication(Configuration);

            //自动转换
            services.ConfigAutoMapper();

            services.ConfigCors(AllowAnyDomain);

            services.ConfigureSwagger(Configuration);


            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options => {
            //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //});



            //注册认证策略
            services.AddPolicies();

            services.ConfigDataBase(Configuration);



            ////注册asp.net Identity
            //services.ConfigIdentity(Configuration);
            ////注册Identityserver4认证服务
            //services.ConfigIdentityServer(Configuration);

            //添加认证
            services.ConfigAuthentication(Configuration);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            app.ConfigureExceptionMidlleware(logger);//异常捕获

            app.UseRouting();


            app.UseCors(AllowAnyDomain);

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });


            app.UseIdentityServer();




            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "KnowledgeBaseAPI");

                c.RoutePrefix = string.Empty;

            });

            
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {

            builder.RegisterModule(new AutofacModule(Configuration));
        }

        

    }
}
