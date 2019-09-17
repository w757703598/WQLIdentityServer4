using System;
using System.Collections.Generic;
using System.Text;
using WQLIdentity.Domain.Entities;
using WQLIdentity.Domain.Interface;
using WQLIdentity.Infra.Data.Repository;

namespace WQLIdentity.Infra.Data.Mysql.Repositorys
{
    public class MysqlApplicationRepository<TEntity> : BaseRepository<TEntity, MysqlApplicationDbContext>, IApplicationRepository<TEntity> where TEntity : Entity
    {
        public MysqlApplicationRepository(MysqlApplicationDbContext applicationDb) : base(applicationDb)
        {
        }
    }
}
