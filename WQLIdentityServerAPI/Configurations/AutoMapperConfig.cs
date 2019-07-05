using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WQLIdentityServerAPI.Configurations
{
    public static class AutoMapperConfig
    {
        public static void ConfigAutoMapper(this IServiceCollection services)
        {


            services.AddAutoMapper(Assembly.Load("WQLIdentityServer.Infra"), Assembly.Load("WQLIdentity.Application"), Assembly.GetEntryAssembly());

        }
    }
}
