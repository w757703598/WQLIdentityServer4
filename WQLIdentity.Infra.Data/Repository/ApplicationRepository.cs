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
    public class ApplicationRepository<TEntity> : IApplicationRepository<TEntity> where TEntity: Entity
    {
        protected readonly ApplicationDbContext Db;
        protected readonly DbSet<TEntity> DbSet;
        public ApplicationRepository(ApplicationDbContext applicationDb)
        {
            Db = applicationDb;
            DbSet = Db.Set<TEntity>();
        }
        public virtual void Add(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public virtual TEntity GetById<T>(T id)
        {
            return DbSet.Find(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public virtual void Update(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public virtual void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public async Task AddAsync(TEntity obj)
        {
            await DbSet.AddAsync(obj);
        }

        public async Task<TEntity> GetByIdAsync<T>(T id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Db.SaveChangesAsync();
        }
    }
}
