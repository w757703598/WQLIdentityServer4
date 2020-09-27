
using IdentityServer4.EntityFramework.DbContexts;
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
