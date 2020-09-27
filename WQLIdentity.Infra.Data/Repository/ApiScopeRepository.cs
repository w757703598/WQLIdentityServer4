using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using WQLIdentity.Domain.Interface;

namespace WQLIdentity.Infra.Data.Repository
{
    public class ApiScopeRepository : BaseRepository<ApiScope, ConfigurationDbContext>, IScopeRepository
    {
        public ApiScopeRepository(ConfigurationDbContext applicationDb) : base(applicationDb)
        {
        }

    }
}
