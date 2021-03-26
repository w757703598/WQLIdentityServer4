
using IdentityServer4.EntityFramework.DbContexts;
using WQLIdentity.Domain.Interface;

namespace WQLIdentity.Infra.Data.Repository
{
    public class ConfigurationRepository<TEntity> : BaseRepository<TEntity, CustomConfigurationDbContext>, IConfigurationRepository<TEntity> where TEntity : class
    {
        public ConfigurationRepository(CustomConfigurationDbContext applicationDb) : base(applicationDb)
        {
        }
    }
}
