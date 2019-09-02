using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WQLIdentity.Infra.Data;
using WQLIdentity.Infra.Data.Entities;

namespace WQLIdentityServerAPI.Configurations
{
    public static class IdentityConfig
    {
        public static void ConfigIdentityBySqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            //const string connectionString = @"Server=.;Database=IdentityServer;Trusted_Connection=True;MultipleActiveResultSets=true";

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                //options.UseSqlServer(connectionString);
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>(opt=> {
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequiredLength = 6;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

        }
        public static void ConfigIdentityByMysql(this IServiceCollection services, IConfiguration configuration)
        {
            //const string connectionString = @"Server=.;Database=IdentityServer;Trusted_Connection=True;MultipleActiveResultSets=true";

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"));
                //options.UseSqlServer(connectionString);
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>(opt => {
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequiredLength = 6;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

        }
    }
}
