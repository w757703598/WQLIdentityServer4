using IdentityServer4.EntityFramework.Entities;
using System.Threading.Tasks;
using WQLIdentity.Application.Dtos.ApiResources;

namespace WQLIdentity.Application.Interfaces
{
    public interface IApiScopeService
    {
        Task<ApiScope> GetScope(string scopeName);
        Task<bool> AddApiScope(ApiScopeDto apiScope);

        Task<bool> UpdateApiScope(ApiScopeDto apiScope);

        Task<bool> RemoveApiScope(ApiScopeDto apiScope);
    }
}
