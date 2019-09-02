using IdentityServer4;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WQLIdentity.Infra.Data;
using WQLIdentity.Infra.Data.Entities;
using WQLIdentityServerAPI.IdentityServers;

namespace WQLIdentityServerAPI.Configurations
{
    public static class IdentityServerConfig
    {
        public static void ConfigIdentityServerBySqlServer(this IServiceCollection services, IConfiguration configuration)
        {
   


            string connectionString = configuration.GetConnectionString("DefaultConnection");

            var migrationAssembly = typeof(ApplicationUser).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentityServer()
                
                .AddDeveloperSigningCredential()

                   .AddConfigurationStore(options =>
                   {
                       //ConfigurationDbContext
                       options.ConfigureDbContext = builder =>
                       {
                           builder.UseSqlServer(connectionString,
                               sql => sql.MigrationsAssembly(migrationAssembly));
                       };
                   })
                .AddOperationalStore(options =>
                {

                    //PersistedGrantDbContext
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationAssembly));
                    };
                })
                  .AddAspNetIdentity<ApplicationUser>()
                .AddResourceOwnerValidator<CustomResourceOwnerPasswordValidtor<ApplicationUser, ApplicationRole>>()
                .AddProfileService<CustomProfileService<ApplicationUser>>();
           


        }

        public static void ConfigIdentityServerByMysql(this IServiceCollection services, IConfiguration configuration)
        {



            string connectionString = configuration.GetConnectionString("DefaultConnection");

            var migrationAssembly = typeof(ApplicationUser).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentityServer()

                .AddDeveloperSigningCredential()

                   .AddConfigurationStore(options =>
                   {
                       //ConfigurationDbContext
                       options.ConfigureDbContext = builder =>
                       {
                           builder.UseMySql(connectionString,
                               sql => sql.MigrationsAssembly(migrationAssembly));
                       };
                   })
                .AddOperationalStore(options =>
                {

                    //PersistedGrantDbContext
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseMySql(connectionString,
                            sql => sql.MigrationsAssembly(migrationAssembly));
                    };
                })
                  .AddAspNetIdentity<ApplicationUser>()
                .AddResourceOwnerValidator<CustomResourceOwnerPasswordValidtor<ApplicationUser, ApplicationRole>>()
                .AddProfileService<CustomProfileService<ApplicationUser>>();



        }

    }
}
