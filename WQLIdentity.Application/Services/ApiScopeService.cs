using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WQLIdentity.Application.Dtos.ApiResources;
using WQLIdentity.Application.Interfaces;
using WQLIdentity.Domain.Interface;

namespace WQLIdentity.Application.Services
{
    public class ApiScopeService : IApiScopeService
    {

        private IConfigurationRepository<ApiScope> _apiscopeRepository;
        private IMapper _mapper;
        public ApiScopeService(IConfigurationRepository<ApiScope> apiscopeRepository, IMapper mapper)
        {

            _apiscopeRepository = apiscopeRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddApiScope(ApiScopeDto apiScope)
        {
            var scope = await _apiscopeRepository.GetAll().Include(s => s.Properties).Include(i => i.UserClaims).FirstOrDefaultAsync(d => d.Name == apiScope.Name);
            if (scope != null)
            {
                return false;
            }
            var entity = _mapper.Map<ApiScope>(scope);
            await _apiscopeRepository.AddAsync(entity);
            return await _apiscopeRepository.SaveChangesAsync() > 0;


        }
        public async Task<ApiScope> GetScope(string scopeName)
        {
            var entity = await _apiscopeRepository.GetAll().Include(d => d.UserClaims).FirstOrDefaultAsync(d => d.Name == scopeName);
            return entity;
        }
        public async Task<bool> RemoveApiScope(ApiScopeDto apiScope)
        {
            var scope = await _apiscopeRepository.GetAll().FirstOrDefaultAsync(d => d.Name == apiScope.Name);
            if (scope == null)
            {
                return false;
            }
            _apiscopeRepository.Remove(scope);
            return await _apiscopeRepository.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateApiScope(ApiScopeDto apiScope)
        {
            var scope = await _apiscopeRepository.GetAll().Include(s => s.Properties).Include(i => i.UserClaims).FirstOrDefaultAsync(d => d.Name == apiScope.Name);
            if (scope == null)
            {
                return false;
            }
            var entity = _mapper.Map<ApiScopeDto, ApiScope>(apiScope, scope);
            _apiscopeRepository.Update(entity);
            return await _apiscopeRepository.SaveChangesAsync() > 0;
        }
    }
}
