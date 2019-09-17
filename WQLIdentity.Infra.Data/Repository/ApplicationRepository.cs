using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
