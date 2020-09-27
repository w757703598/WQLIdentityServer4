using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using System.Linq;

namespace WQLIdentity.Application.Dtos.ApiResources
{
    public class ApiResourceMapping : Profile
    {
        public ApiResourceMapping()
        {
            CreateMap<CreateApiResouce, ApiResource>().ForMember(x => x.UserClaims, opt => opt.MapFrom(src => src.UserClaims.Select(x => new ApiResourceClaim { Type = x })));
            CreateMap<UpdateApiResource, ApiResource>().ForMember(x => x.UserClaims, opt => opt.MapFrom(src => src.UserClaims.Select(x => new ApiResourceClaim { Type = x })));
            //CreateMap<ApiResource, CreateApiResouce>().ForMember(x => x.UserClaims, opt => opt.MapFrom(src => src.UserClaims.Select(c=>c.Type) ));

            CreateMap<ApiResource, ApiResourceDto>().ForMember(x => x.UserClaims, opt => opt.MapFrom(src => src.UserClaims.Select(c => c.Type)));



            //apiscope
            CreateMap<ApiScope, ApiScopeDto>().ForMember(x => x.UserClaims, opt => opt.MapFrom(src => src.UserClaims.Select(c => c.Type)));
            CreateMap<CreateApiScopeDto, ApiScope>().ForMember(x => x.UserClaims, opt => opt.MapFrom(src => src.UserClaims.Select(x => new ApiScopeClaim { Type = x })));
            CreateMap<UpdateScopeDto, ApiScope>().ForMember(x => x.UserClaims, opt => opt.MapFrom(src => src.UserClaims.Select(x => new ApiScopeClaim { Type = x })));


            //apiscret
            CreateMap<CreateApiSecretDto, ApiResourceSecret>();
            CreateMap<CreateApiPropertiesDto, ApiResourceProperty>();
        }
    }
}
