using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using IdentityServer4.EntityFramework.Interfaces;
using Microsoft.Extensions.Configuration;
using WQLIdentity.Application.Interfaces;
using WQLIdentity.Application.Services;
using WQLIdentity.Domain.Interface;
using WQLIdentity.Infra.Data;
using WQLIdentity.Infra.Data.Mysql.Repositorys;
using WQLIdentity.Infra.Data.Repository;
using WQLIdentityServerAPI.Configurations.Consts;
using WQLIdentityServerAPI.IdentityServers.Services;

namespace WQLIdentityServerAPI.Configurations
{
    internal class AutofacModule : Autofac.Module
    {
        private IConfiguration _configuration;
        internal AutofacModule( IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {

            // builder.Register(c => new SimulationService(c.Resolve<IConfiguration>())).InstancePerLifetimeScope();


            var Services = Assembly.Load("WQLIdentity.Application");
            var IRepository = Assembly.Load("WQLIdentity.Domain");
            var Repository = Assembly.Load("WQLIdentity.Infra.Data");

            //根据名称约定（服务层的接口和实现均以Services结尾），实现服务接口和服务实现的依赖
            builder.RegisterAssemblyTypes(Services)
              .Where(t => t.Name.EndsWith("Service"))
              .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Repository, IRepository)
             .Where(t => t.Name.EndsWith("Repository"))
             .AsImplementedInterfaces();

            var databaseType = _configuration.GetSection("Settings")["DatabaseType"];
            if (databaseType.ToLower() == DatabaseConst.Mysql)
            {
                builder.RegisterGeneric(typeof(MysqlApplicationRepository<>)).As(typeof(IApplicationRepository<>));
                builder.RegisterGeneric(typeof(MysqlConfigurationRepository<>)).As(typeof(IConfigurationRepository<>));
            }
            else
            {
                builder.RegisterGeneric(typeof(ApplicationRepository<>)).As(typeof(IApplicationRepository<>));
                builder.RegisterGeneric(typeof(ConfigurationRepository<>)).As(typeof(IConfigurationRepository<>));
            }


            builder.RegisterGeneric(typeof(ApplicationBaseService<>)).As(typeof(IApplicationBaseService<>));
            builder.RegisterType<AuthCodeService>().As<IAuthCodeService>();


            //builder.RegisterType<MysqlPersistedGrantDbContext>().As<IPersistedGrantDbContext>();
            //builder.RegisterType<MysqlConfigurationDbContext>().As<IConfigurationDbContext>();

            //builder.RegisterGeneric(typeof(BaseService<>)).As(typeof(IBaseService<>));


        }
    }
}
