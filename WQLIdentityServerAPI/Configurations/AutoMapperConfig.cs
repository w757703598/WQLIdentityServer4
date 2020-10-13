using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WQLIdentity.Application.Dtos.UserManager;
using WQLIdentity.Infra.Data.Entities;
using WQLIdentityServer.Infra.Dto;

namespace WQLIdentityServerAPI.Configurations
{
    public static class AutoMapperConfig
    {
        public static void ConfigAutoMapper(this IServiceCollection services)
        {
            var temp = Assembly.GetEntryAssembly();
            services.AddAutoMapper(Assembly.Load("WQLIdentity.Infra.Data"), Assembly.Load("WQLIdentityServer.Infra"), Assembly.Load("WQLIdentity.Domain"), Assembly.Load("WQLIdentity.Application"), Assembly.Load("WQLIdentityServerAPI"));



        }
    }

    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap(typeof( Pagelist<>),typeof( Pagelist<>));
        }
    }
}
