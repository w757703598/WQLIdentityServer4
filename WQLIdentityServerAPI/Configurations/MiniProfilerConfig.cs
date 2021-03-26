using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WQLIdentityServerAPI.Models;

namespace WQLIdentityServerAPI.Configurations
{
    public static class MiniProfilerConfig
    {
         
        public static void ConfigMiniProfiler(this IServiceCollection services,SettingOptions options)
        {

            if (options.UseMinProfiler)
            {
                services.AddMiniProfiler(opt =>
                {
                    opt.RouteBasePath = "/profiler";
                })
                .AddEntityFramework();
            }


        }
    }
}
