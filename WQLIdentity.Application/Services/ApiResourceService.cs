using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WQLIdentity.Application.Dtos.ApiResources;
using WQLIdentity.Application.Interfaces;
using WQLIdentity.Domain.Interface;
using WQLIdentityServer.Infra.Dto;
using WQLIdentityServer.Infra.Extensions;
using ApiResource = IdentityServer4.EntityFramework.Entities.ApiResource;

namespace WQLIdentity.Application.Services
{
    public class ApiResourceService : IApiResourceService
    {
        private IConfigurationRepository<ApiResource> _apiresourceRepository;
        private IConfigurationRepository<ApiScope> _apiscopeRepository;
        private IConfigurationRepository<ApiResourceSecret> _apisecretRepository;
        private IConfigurationRepository<ApiResourceProperty> _apipropertyRepository;
        private IMapper _mapper;
        public ApiResourceService(IConfigurationRepository<ApiResource> apiresourceRepository, IConfigurationRepository<ApiScope> apiscopeRepository, IConfigurationRepository<ApiResourceSecret> apisecretRepository, IConfigurationRepository<ApiResourceProperty> apipropertyRepository, IMapper mapper)
        {
            _apipropertyRepository = apipropertyRepository;

            _apiresourceRepository = apiresourceRepository;
            _apisecretRepository = apisecretRepository;
            _mapper = mapper;
        }
        public Pagelist<ApiResourceListDto> GetApiResources(PageInputDto pageInput)
        {
            var resources = _apiresourceRepository.GetAll().PageBy(pageInput, p => p.Id);
            return _mapper.Map<Pagelist<ApiResourceListDto>>(resources);
        }

        public async Task<ApiResourceDto> GetApiResource(int Id)
        {
            var resource = await _apiresourceRepository.GetAll().Include(a => a.UserClaims).AsNoTracking().FirstOrDefaultAsync(d => d.Id == Id);

            return _mapper.Map<ApiResourceDto>(resource);
        }



        public async Task<bool> Remove(int Id)
        {
            var entity = await _apiresourceRepository.GetByIdAsync(Id);
            _apiresourceRepository.Remove(entity);
            return await _apiresourceRepository.SaveChangesAsync() > 0;
        }



        public async Task<bool> CreateApiResource(CreateApiResouce model)
        {
            var apiresource = _mapper.Map<ApiResource>(model);
            await _apiresourceRepository.AddAsync(apiresource);
            return await _apiresourceRepository.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(UpdateApiResource model)
        {
            var entity = await _apiresourceRepository.GetAll().Include(c => c.UserClaims).FirstOrDefaultAsync(d => d.Id == model.Id);
            var apiresource = _mapper.Map<UpdateApiResource, ApiResource>(model, entity);
            _apiresourceRepository.Update(apiresource);
            return await _apiresourceRepository.SaveChangesAsync() > 0;
        }

        public Pagelist<ApiScopeDto> GetScopes(PageInputDto pageInput, int apiresourceId)
        {

            var scopes = _apiresourceRepository.GetAll().Where(d => d.Id == apiresourceId).Include(d => d.Scopes).SelectMany(d => d.Scopes).PageBy(pageInput, d => d.Id);
            return _mapper.Map<Pagelist<ApiScopeDto>>(scopes);
        }


        public async Task<bool> AddScope(ApiScopeResourceDto apiScope)
        {
            var apiresource = await _apiresourceRepository.GetByIdAsync(apiScope.ResourceId);
            if (apiresource == null)
            {
                throw new Exception("apiResource不存在");
            }
       
            apiresource.Scopes.Add(_mapper.Map<ApiResourceScope>(apiScope));
            return await _apiresourceRepository.SaveChangesAsync() > 0;

        }

        public async Task<bool> RemoveScope(ApiScopeResourceDto removeApiScope)
        {
            var apiResource = await _apiresourceRepository.GetByIdAsync(removeApiScope.ResourceId);
            if (apiResource == null)
            {
                throw new Exception("apiResource不存在");
            }
            if (apiResource.Scopes.FirstOrDefault(d => d.Scope == removeApiScope.ScopeName) != null)
            {
                throw new Exception("不存在" + removeApiScope.ScopeName);
            }
            apiResource.Scopes.RemoveAll(d => d.Scope == removeApiScope.ScopeName);
            return await _apiresourceRepository.SaveChangesAsync() > 0;
        }






        public Pagelist<ApiResourceProperty> GetProperties(PageInputDto pageInput, int apiresourceId)
        {
            var scopes = _apipropertyRepository.GetAll().Where(d => d.ApiResourceId == apiresourceId).PageBy(pageInput, d => d.Id);
            return _mapper.Map<Pagelist<ApiResourceProperty>>(scopes);
        }

        public async Task<bool> RemovePropertiest(int Id)
        {
            var property = await _apipropertyRepository.GetByIdAsync(Id);
            if (property == null)
            {
                throw new Exception("apiResource不存在");
            }
            _apipropertyRepository.Remove(property);
            return await _apipropertyRepository.SaveChangesAsync() > 0;

        }

        public async Task<bool> AddProperties(CreateApiPropertiesDto apiProperties)
        {
            var apiresource = await _apiresourceRepository.GetByIdAsync(apiProperties.ApiResourceId);
            var entity = _mapper.Map<ApiResourceProperty>(apiProperties);
            entity.ApiResource = apiresource ?? throw new Exception("apiResource不存在");

            await _apipropertyRepository.AddAsync(entity);
            return await _apipropertyRepository.SaveChangesAsync() > 0;
        }

        public Pagelist<ApiResourceSecret> GetSecrets(PageInputDto pageInput, int apiresourceId)
        {
            var secret = _apisecretRepository.GetAll().Where(d => d.ApiResourceId == apiresourceId).PageBy(pageInput, d => d.Id);
            return _mapper.Map<Pagelist<ApiResourceSecret>>(secret);
        }

        public async Task<bool> RemoveSecret(int secretId)
        {
            var secret = await _apisecretRepository.GetByIdAsync(secretId);
            if (secret == null)
            {
                throw new Exception("apiResource不存在");
            }
            _apisecretRepository.Remove(secret);
            return await _apisecretRepository.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddSecret(CreateApiSecretDto apisecret)
        {
            var apiresource = await _apiresourceRepository.GetByIdAsync(apisecret.ApiResourceId);
            var entity = _mapper.Map<ApiResourceSecret>(apisecret);
            entity.ApiResource = apiresource ?? throw new Exception("apiResource不存在");
            await _apisecretRepository.AddAsync(entity);
            return await _apisecretRepository.SaveChangesAsync() > 0;
        }
    }
}
