using IdentityServer4;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WQLIdentityServerAPI.Configurations
{
    public static class AuthenticationConfig
    {
        public static void ConfigAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

       

            services.AddAuthentication()
             .AddJwtBearer("Bearer", options =>
            {
                
                var configUrl = configuration["authUrls"];
                options.Authority = configUrl;
                options.RequireHttpsMetadata = false;
                options.Audience = "IdentityServer";
                
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("secret")),
                    ValidateLifetime = true,
                    RoleClaimType = "role"
                };

            });




            //services
            // .AddAuthentication(options =>
            // {
            //     options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            //     options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            // })
            // .AddIdentityServerAuthentication(options =>
            // {
            //     var configUrl = configuration["authUrls"];
            //     options.Authority = configUrl;
            //     options.RequireHttpsMetadata = false;

            //     options.ApiName = "api1";

            // });

        }
    }
}
