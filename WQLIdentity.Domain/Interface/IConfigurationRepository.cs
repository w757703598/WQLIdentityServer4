using System.Linq;
using System.Threading.Tasks;

namespace WQLIdentity.Domain.Interface
{
    public interface IConfigurationRepository<TEntity> where TEntity : class
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
