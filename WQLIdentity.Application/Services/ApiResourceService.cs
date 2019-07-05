using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
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
        private IConfigurationRepository<ApiSecret> _apisecretRepository;
        private IConfigurationRepository<ApiResourceProperty> _apipropertyRepository;
        private IMapper _mapper;
        public ApiResourceService(IConfigurationRepository<ApiResource> apiresourceRepository, IConfigurationRepository<ApiScope> apiscopeRepository, IConfigurationRepository<ApiResourceProperty> apipropertyRepository, IConfigurationRepository<ApiSecret> apisecretRepository,IMapper mapper)
        {
            _apipropertyRepository = apipropertyRepository;
            _apisecretRepository = apisecretRepository;
            _apiresourceRepository = apiresourceRepository;
            _apiscopeRepository = apiscopeRepository;
            _mapper = mapper;
        }
        public  Pagelist<ApiResourceListDto> GetApiResources(PageInputDto pageInput)
        {
            var resources=  _apiresourceRepository.GetAll().PageBy(pageInput,p=>p.Id);
            return _mapper.Map<Pagelist<ApiResourceListDto>>(resources);
        }

        public async Task<ApiResourceDto> GetApiResource(int Id)
        {
            var resource =await _apiresourceRepository.GetAll().Include(a=>a.UserClaims).AsNoTracking().FirstOrDefaultAsync(d=>d.Id==Id);

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
            var entity =await _apiresourceRepository.GetAll().Include(c=>c.UserClaims).FirstOrDefaultAsync(d=>d.Id==model.Id);
            var apiresource = _mapper.Map<UpdateApiResource,ApiResource>(model, entity);
            _apiresourceRepository.Update(apiresource);
            return await _apiresourceRepository.SaveChangesAsync() > 0;
        }

        public  Pagelist<ApiScopeDto> GetScopes(PageInputDto pageInput, int apiresourceId)
        {
            var scopes =  _apiscopeRepository.GetAll().Where(d => d.ApiResourceId == apiresourceId).Include(d=>d.UserClaims).PageBy(pageInput,d=>d.Id);
            return _mapper.Map<Pagelist<ApiScopeDto>>(scopes);
        }

        public async Task<ApiScope> GetScope(int scopeId)
        {
            var entity = await _apiscopeRepository.GetAll().Include(d => d.UserClaims).FirstOrDefaultAsync(d=>d.Id==scopeId);
            return entity;
        }

        public async Task<bool> AddScope(CreateApiScopeDto apiScope)
        {
            var apiresource =await _apiresourceRepository.GetByIdAsync(apiScope.ApiResourceId);
            if (apiresource == null)
            {
                throw new Exception("apiResource不存在");
            }
            var entity = _mapper.Map<ApiScope>(apiScope);
            await _apiscopeRepository.AddAsync(entity);
            return await _apiscopeRepository.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveScope(int scopeId)
        {
            var scope = await _apiscopeRepository.GetByIdAsync(scopeId);
            if (scope == null)
            {
                throw new Exception("apiResource不存在");
            }
            _apiscopeRepository.Remove(scope);
            return await _apiscopeRepository.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateScope(UpdateScopeDto scopeDto)
        {
            var entity = await _apiscopeRepository.GetAll().Include(c => c.UserClaims).FirstOrDefaultAsync(d => d.Id == scopeDto.Id);
            if (entity == null)
            {
                throw new Exception("apiScope不存在");
            }
            var scope = _mapper.Map<UpdateScopeDto, ApiScope>(scopeDto, entity);
            _apiscopeRepository.Update(scope);
            return await _apiscopeRepository.SaveChangesAsync() > 0;
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


        public  Pagelist<ApiSecret> GetSecrets(PageInputDto pageInput, int apiresourceId)
        {
            var scopes = _apisecretRepository.GetAll().Where(d => d.ApiResourceId == apiresourceId).PageBy(pageInput, d => d.Id);
            return _mapper.Map<Pagelist<ApiSecret>>(scopes);
        }

        public async Task<bool> AddSecret(CreateApiSecretDto apiSecret)
        {
            var apiresource = await _apiresourceRepository.GetByIdAsync(apiSecret.ApiResourceId);
            if (apiresource == null)
            {
                throw new Exception("apiResource不存在");
            }
            var entity = _mapper.Map<ApiSecret>(apiSecret);

         

            if (apiSecret.Hash == HashType.Sha256)
            {
                entity.Value = apiSecret.Value.Sha256();
            }
            else if (apiSecret.Hash ==HashType.Sha512)
            {
                entity.Value = apiSecret.Value.Sha512();
            }


            await _apisecretRepository.AddAsync(entity);
            return await _apisecretRepository.SaveChangesAsync() > 0;
        }

        public Pagelist<ApiResourceProperty> GetProperties(PageInputDto pageInput, int apiresourceId)
        {
            var scopes = _apipropertyRepository.GetAll().Where(d => d.ApiResourceId == apiresourceId).PageBy(pageInput, d => d.Id);
            return _mapper.Map<Pagelist<ApiResourceProperty>>(scopes);
        }

        public async Task<bool> RemovePropertiest(int Id)
        {
            var secret = await _apipropertyRepository.GetByIdAsync(Id);
            if (secret == null)
            {
                throw new Exception("apiResource不存在");
            }
            _apipropertyRepository.Remove(secret);
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
    }
}
