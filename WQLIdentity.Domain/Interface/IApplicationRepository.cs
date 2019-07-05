using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WQLIdentity.Domain.Entities;

namespace WQLIdentity.Domain.Interface
{
    public interface IApplicationRepository<TEntity> where TEntity : Entity
    {
        void Add(TEntity obj);
        TEntity GetById<T>(T id);
        IQueryable<TEntity> GetAll();
        void Update(TEntity obj);
        void Remove(TEntity id);
        int SaveChanges();
        Task AddAsync(TEntity obj);
        Task<TEntity> GetByIdAsync<T>(T id);
        Task<int> SaveChangesAsync();
    }
}
