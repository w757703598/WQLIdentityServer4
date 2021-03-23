using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Reflection;
using WQLIdentity.Infra.Data;
using WQLIdentity.Infra.Data.Entities;
using WQLIdentityServerAPI.IdentityServers;

namespace WQLIdentityServerAPI.Configurations
{
    public static class IdentityServerConfig
    {
        public static void ConfigIdentityServerBySqlServer(this IServiceCollection services, IConfiguration configuration, string connectionString)
        {



            var migrationAssembly = typeof(ApplicationUser).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentityServer(options =>
            {

                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })

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
                    options.EnableTokenCleanup = true;
                })
                .AddAspNetIdentity<ApplicationUser>()
                .AddResourceOwnerValidator<CustomResourceOwnerPasswordValidtor<ApplicationUser, ApplicationRole>>()
                .AddExtensionGrantValidator<SmsAuthCodeValidator>()   //短信扩展验证
                .AddProfileService<CustomProfileService<ApplicationUser>>();



        }

        public static void ConfigIdentityServerByMysql(this IServiceCollection services, IConfiguration configuration, string connectionString)
        {


            var migrationAssembly = typeof(MysqlConfigurationDbContext).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentityServer(options =>
            {

                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
               .AddDeveloperSigningCredential()

                .AddConfigurationStore<MysqlConfigurationDbContext>(options =>
                {
                    //ConfigurationDbContext
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseMySql(connectionString,MySqlServerVersion.LatestSupportedServerVersion,
                            sql =>
                            {
                                sql.MigrationsAssembly(migrationAssembly);
                                //sql.ServerVersion(new Version(5, 5, 47), ServerType.MySql);
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
                    builder.UseMySql(connectionString,MySqlServerVersion.LatestSupportedServerVersion,
                        sql =>
                        {
                            sql.MigrationsAssembly(migrationAssembly);
                           // sql.ServerVersion(new Version(5, 5, 47), ServerType.MySql);
                        });
                    //builder.UseMySQL(connectionString, sql =>
                    //{
                    //    sql.MigrationsAssembly(migrationAssembly);

                    //});
                    options.EnableTokenCleanup = true;
                };

            })
            .AddAspNetIdentity<ApplicationUser>()
            .AddResourceOwnerValidator<CustomResourceOwnerPasswordValidtor<ApplicationUser, ApplicationRole>>()
            .AddExtensionGrantValidator<SmsAuthCodeValidator>()   //短信扩展验证
            .AddProfileService<CustomProfileService<ApplicationUser>>();



        }

    }
}
