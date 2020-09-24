using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using WQLIdentity.Domain.Interface;
using WQLIdentity.Infra.Data.Repository;

namespace WQLIdentity.Infra.Data.Mysql.Repositorys
{
    public class MysqlApiScopeRepository: BaseRepository<ApiScope, MysqlConfigurationDbContext>, IScopeRepository
    {
        public MysqlApiScopeRepository(MysqlConfigurationDbContext applicationDb) : base(applicationDb)
        {
        }
    }
}
