using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WQLIdentity.Application.Dtos.IdentityResources;
using WQLIdentity.Application.Interfaces;
using WQLIdentity.Domain.Interface;
using WQLIdentityServer.Infra.Dto;
using WQLIdentityServer.Infra.Extensions;

namespace WQLIdentity.Application.Services
{
    public class IdentityResourceService : IIdentityResourceService
    {
        private IConfigurationRepository<IdentityResource> _identityResouceRepository;
        private IConfigurationRepository<IdentityResourceProperty> _identityResourcePropertyRepository;
        private IMapper _mapper;

        public IdentityResourceService(IConfigurationRepository<IdentityResource> identityResouceRepository, IConfigurationRepository<IdentityResourceProperty> identityResourcePropertyRepository, IMapper mapper)
        {
            _identityResouceRepository = identityResouceRepository;
            _identityResourcePropertyRepository = identityResourcePropertyRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddIdentityResourceAsync(CreateIdentityResourceDto identityResource)
        {
            var apiresource = _mapper.Map<IdentityResource>(identityResource);
            apiresource.Created = DateTime.Now;
            await _identityResouceRepository.AddAsync(apiresource);
            return await _identityResouceRepository.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddIdentityResourcePropertyAsync(IdentityResourcePropertyDto identityResourceProperties)
        {
            var identityResource = await _identityResouceRepository.GetByIdAsync(identityResourceProperties.IdentityResourceId);
            var entity = _mapper.Map<IdentityResourceProperty>(identityResourceProperties);

            entity.IdentityResource = identityResource ?? throw new Exception("IdentityResource不存在");
            await _identityResourcePropertyRepository.AddAsync(entity);
            return await _identityResourcePropertyRepository.SaveChangesAsync() > 0;
        }



        public Task<bool> CanInsertIdentityResourceAsync(IdentityResourceDto identityResource)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CanInsertIdentityResourcePropertyAsync(IdentityResourcePropertyDto property)
        {
            var result = await _identityResourcePropertyRepository.GetAll().FirstOrDefaultAsync(x => x.Key == property.Key && x.IdentityResourceId == property.IdentityResourceId);
            return result == null;
        }

        public async Task<bool> DeleteIdentityResourceAsync(int Id)
        {
            var entity = await _identityResouceRepository.GetByIdAsync(Id);
            _identityResouceRepository.Remove(entity);
            return await _identityResouceRepository.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteIdentityResourcePropertyAsync(int Id)
        {
            var secret = await _identityResourcePropertyRepository.GetByIdAsync(Id);
            if (secret == null)
            {
                throw new Exception("IdentityResource不存在");
            }
            _identityResourcePropertyRepository.Remove(secret);
            return await _identityResourcePropertyRepository.SaveChangesAsync() > 0;
        }

        public async Task<IdentityResourceDto> GetIdentityResourceAsync(int Id)
        {
            var resource = await _identityResouceRepository.GetAll().Include(a => a.UserClaims).AsNoTracking().FirstOrDefaultAsync(d => d.Id == Id);

            return _mapper.Map<IdentityResourceDto>(resource);
        }

        public Pagelist<IdentityResourceProperty> GetIdentityResourcePropertiesAsync(PageInputDto pageInput, int identityResourceId)
        {
            var identityResourceProperty = _identityResourcePropertyRepository.GetAll().Where(d => d.IdentityResourceId == identityResourceId).PageBy(pageInput, d => d.Id);
            return identityResourceProperty;
        }



        public Pagelist<IdentityResourceListDto> GetIdentityResourcesAsync(PageInputDto pageInput)
        {
            var resources = _identityResouceRepository.GetAll().PageBy(pageInput, p => p.Id);
            return _mapper.Map<Pagelist<IdentityResourceListDto>>(resources);
        }

        public async Task<bool> UpdateIdentityResourceAsync(CreateIdentityResourceDto identityResource)
        {
            var entity = await _identityResouceRepository.GetAll().Include(c => c.UserClaims).FirstOrDefaultAsync(d => d.Id == identityResource.Id);
            var apiresource = _mapper.Map<CreateIdentityResourceDto, IdentityResource>(identityResource, entity);
            apiresource.Updated = DateTime.Now;
            _identityResouceRepository.Update(apiresource);
            return await _identityResouceRepository.SaveChangesAsync() > 0;
        }
    }
}
