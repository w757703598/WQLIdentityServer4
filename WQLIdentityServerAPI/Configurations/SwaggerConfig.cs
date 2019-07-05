using Microsoft.Extensions.DependencyInjection;
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
        public  static void ConfigSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "IdentityServer",
                    Version = "V1",
                    Description = "用户认证授权中心"
                });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    In = "header",
                    Description = "Please insert JWT with Bearer into filed",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer",new string[]{ } }
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
