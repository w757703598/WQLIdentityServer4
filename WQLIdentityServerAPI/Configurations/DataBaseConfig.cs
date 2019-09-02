using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WQLIdentityServerAPI.Configurations.Consts;

namespace WQLIdentityServerAPI.Configurations
{
    public static class DataBaseConfig
    {
        public static void ConfigDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseType = configuration.GetSection("Settings")["DatabaseType"];
            if (databaseType.ToLower() == DatabaseConst.Mysql)
            {
                //注册asp.net Identity
                services.ConfigIdentityByMysql(configuration);
                //注册Identityserver4认证服务
                services.ConfigIdentityServerByMysql(configuration);
            }
            else
            {
                //注册asp.net Identity
                services.ConfigIdentityBySqlServer(configuration);
                //注册Identityserver4认证服务
                services.ConfigIdentityServerBySqlServer(configuration);
            }
         

        }
    }
}
