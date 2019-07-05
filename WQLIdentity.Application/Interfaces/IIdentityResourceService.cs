using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WQLIdentity.Application.Dtos.IdentityResources;
using WQLIdentityServer.Infra.Dto;

namespace WQLIdentity.Application.Interfaces
{
    public interface IIdentityResourceService
    {
        Pagelist<IdentityResourceListDto> GetIdentityResourcesAsync(PageInputDto pageInput);

        Task<IdentityResourceDto> GetIdentityResourceAsync(int Id);

        Task<bool> CanInsertIdentityResourceAsync(IdentityResourceDto identityResource);

        Task<bool> AddIdentityResourceAsync(CreateIdentityResourceDto identityResource);

        Task<bool> UpdateIdentityResourceAsync(CreateIdentityResourceDto identityResource);

        Task<bool> DeleteIdentityResourceAsync(int Id);


        Pagelist<IdentityResourceProperty> GetIdentityResourcePropertiesAsync(PageInputDto pageInput, int identityResourceId);


        Task<bool> AddIdentityResourcePropertyAsync(IdentityResourcePropertyDto identityResourceProperties);

        Task<bool> DeleteIdentityResourcePropertyAsync(int id);

    
    }
}
