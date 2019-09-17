using IdentityServer4;
using IdentityServer4.EntityFramework.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WQLIdentity.Infra.Data;
using WQLIdentity.Infra.Data.Entities;
using WQLIdentityServerAPI.Configurations.Consts;
using WQLIdentityServerAPI.IdentityServers;

namespace WQLIdentityServerAPI.Configurations
{
    public static class IdentityServerConfig
    {
        public static void ConfigIdentityServerBySqlServer(this IServiceCollection services, IConfiguration configuration,string connectionString)
        {



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
                .AddExtensionGrantValidator<SmsAuthCodeValidator>()   //短信扩展验证
                .AddProfileService<CustomProfileService<ApplicationUser>>();
           


        }

        public static void ConfigIdentityServerByMysql(this IServiceCollection services, IConfiguration configuration, string connectionString)
        {


            var migrationAssembly = typeof(MysqlConfigurationDbContext).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentityServer()

                .AddDeveloperSigningCredential()

                   .AddConfigurationStore<MysqlConfigurationDbContext>(options =>
                   {
                       //ConfigurationDbContext
                       options.ConfigureDbContext = builder =>
                       {
                           builder.UseMySql(connectionString,
                               sql =>
                               {
                                   sql.MigrationsAssembly(migrationAssembly);
                                   sql.ServerVersion(new Version(5, 5, 47), ServerType.MySql);
                               });
                           //builder.UseMySQL(connectionString, sql =>
                           //{
                           //    sql.MigrationsAssembly(migrationAssembly);

                           //});
                       };
                       
         
                   })
                .AddOperationalStore<MysqlPersistedGrantDbContext>(options =>
                {

                    //PersistedGrantDbContext
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseMySql(connectionString,
                            sql =>
                            {
                                sql.MigrationsAssembly(migrationAssembly);
                                sql.ServerVersion(new Version(5, 5, 47), ServerType.MySql);
                            });
                        //builder.UseMySQL(connectionString, sql =>
                        //{
                        //    sql.MigrationsAssembly(migrationAssembly);

                        //});
                    };
                    
                })
                .AddAspNetIdentity<ApplicationUser>()
                .AddResourceOwnerValidator<CustomResourceOwnerPasswordValidtor<ApplicationUser, ApplicationRole>>()
                 .AddExtensionGrantValidator<SmsAuthCodeValidator>()   //短信扩展验证
                .AddProfileService<CustomProfileService<ApplicationUser>>();



        }

    }
}
