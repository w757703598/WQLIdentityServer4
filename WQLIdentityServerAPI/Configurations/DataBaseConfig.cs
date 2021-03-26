using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WQLIdentityServerAPI.Configurations.Consts;
using WQLIdentityServerAPI.Models;

namespace WQLIdentityServerAPI.Configurations
{
    public static class DataBaseConfig
    {
        public static void ConfigDataBase(this IServiceCollection services,SettingOptions option)
        {
     
            var databaseType = option.DatabaseType;
            if (databaseType.ToLower() == DatabaseConst.Mysql)
            {

                string connectionString = option.MySqlConnection;
                //注册asp.net Identity
                services.ConfigIdentityByMysql(connectionString);
                //注册Identityserver4认证服务
                services.ConfigIdentityServerByMysql( connectionString);

            }
            else
            {
                string connectionString = option.SqlServerConnection;
                //注册asp.net Identity
                services.ConfigIdentityBySqlServer(connectionString);
                //注册Identityserver4认证服务
                services.ConfigIdentityServerBySqlServer( connectionString);
            }


        }
    }
}
