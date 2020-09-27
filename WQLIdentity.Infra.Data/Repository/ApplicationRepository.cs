using WQLIdentity.Domain.Entities;
using WQLIdentity.Domain.Interface;

namespace WQLIdentity.Infra.Data.Repository
{
    public class ApplicationRepository<TEntity> : BaseRepository<TEntity, ApplicationDbContext>, IApplicationRepository<TEntity> where TEntity : Entity
    {
        public ApplicationRepository(ApplicationDbContext applicationDb) : base(applicationDb)
        {
        }
    }
}
