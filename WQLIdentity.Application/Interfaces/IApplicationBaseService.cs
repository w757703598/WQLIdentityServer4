using System.Linq;
using System.Threading.Tasks;
using WQLIdentity.Domain.Entities;
using WQLIdentityServer.Infra.Dto;

namespace WQLIdentity.Application.Interfaces
{
    public interface IApplicationBaseService<TEntity> where TEntity : Entity
    {
        bool Add(TEntity obj);
        TEntity GetById<T>(T id);
        IQueryable<TEntity> GetAll();
        Pagelist<TEntity> GetAll(PageInputDto input);
        bool Update(TEntity obj);
        bool Remove<T>(T id);
        int SaveChanges();
        Task<bool> AddAsync(TEntity obj);
        Task<TEntity> GetByIdAsync<T>(T id);
        Task<int> SaveChangesAsync();
    }
}
