using Microsoft.Extensions.DependencyInjection;
using WQLIdentityServerAPI.Configurations.Consts;

namespace WQLIdentityServerAPI.Configurations
{
    public static class ConfigurePolicy
    {
        public static void AddPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyConst.Admin,
                    policy => policy.RequireAssertion(c => c.User.IsInRole("Administrator")));
                options.AddPolicy(PolicyConst.Manager,
                    policy => policy.RequireAuthenticatedUser());


            });

        }
    }
}
