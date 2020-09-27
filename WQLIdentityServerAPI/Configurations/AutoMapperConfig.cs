using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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
