using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WQLIdentity.Application.Interfaces;

namespace WQLIdentityServerAPI.Configurations
{
    public static class SwaggerConfig
    {
        public  static void ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "IdentityServer",
                    Version = "V1",
                    Description = "用户认证授权中心"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into filed",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference{Type=ReferenceType.SecurityScheme,Id="Bearer"}
                        },
                        new List<string>()
                    }

                });
                c.IncludeXmlComments(XmlModelsFilePath, true);
                c.IncludeXmlComments(XmlAPIFilePath, true);
            });
        }

        static string XmlModelsFilePath
        {
            get
            {

                var fileName = typeof(IUserAppService).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(System.AppContext.BaseDirectory, fileName);
            }
        }
        static string XmlAPIFilePath
        {
            get
            {

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                return Path.Combine(System.AppContext.BaseDirectory, xmlFile);
            }
        }
    }
}
