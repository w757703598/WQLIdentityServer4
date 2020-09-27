using WQLIdentity.Domain.Interface;
using WQLIdentity.Infra.Data.Repository;

namespace WQLIdentity.Infra.Data.Mysql.Repositorys
{
    public class MysqlConfigurationRepository<TEntity> : BaseRepository<TEntity, MysqlConfigurationDbContext>, IConfigurationRepository<TEntity> where TEntity : class
    {
        public MysqlConfigurationRepository(MysqlConfigurationDbContext applicationDb) : base(applicationDb)
        {
        }
    }
}
