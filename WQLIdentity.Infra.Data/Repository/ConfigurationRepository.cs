
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WQLIdentity.Domain.Interface;

namespace WQLIdentity.Infra.Data.Repository
{
    public class ConfigurationRepository<TEntity> : BaseRepository<TEntity, ConfigurationDbContext>, IConfigurationRepository<TEntity> where TEntity : class
    {
        public ConfigurationRepository(ConfigurationDbContext applicationDb) : base(applicationDb)
        {
        }
    }
}
