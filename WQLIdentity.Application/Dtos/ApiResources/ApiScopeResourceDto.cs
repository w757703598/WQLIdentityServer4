using AutoMapper;
using IdentityServer4.EntityFramework.Entities;

namespace WQLIdentity.Application.Dtos.ApiResources
{
    [AutoMap(typeof(ApiResourceScope))]
    public class ApiScopeResourceDto
    {
        public int ResourceId { get; set; }
        public string ScopeName { get; set; }
    }
}
