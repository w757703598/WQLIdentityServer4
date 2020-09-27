using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

                string connectionString = configuration.GetConnectionString(DatabaseConst.MySqlConnection);
                //注册asp.net Identity
                services.ConfigIdentityByMysql(configuration, connectionString);
                //注册Identityserver4认证服务
                services.ConfigIdentityServerByMysql(configuration, connectionString);

            }
            else
            {
                string connectionString = configuration.GetConnectionString(DatabaseConst.SqlServerConnection);
                //注册asp.net Identity
                services.ConfigIdentityBySqlServer(configuration, connectionString);
                //注册Identityserver4认证服务
                services.ConfigIdentityServerBySqlServer(configuration, connectionString);
            }


        }
    }
}
