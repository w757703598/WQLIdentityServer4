using Microsoft.Extensions.DependencyInjection;

namespace WQLIdentityServerAPI.Configurations
{
    public static class CorsConfig
    {
        public static void ConfigCors(this IServiceCollection services, string name)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy(name, builder => builder
                //.WithOrigins("10.53.28.165")
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()


                .SetIsOriginAllowed((host) => true)
                //.SetIsOriginAllowedToAllowWildcardSubdomains()
                //.AllowCredentials()
                );
            });
        }
    }
}
