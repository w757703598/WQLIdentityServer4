using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using System.Linq;

namespace WQLIdentity.Application.Dtos.ApiResources
{
    public class ApiResourceMapping : Profile
    {
        public ApiResourceMapping()
        {
            CreateMap<CreateApiResouce, ApiResource>(MemberList.Source).ForMember(x => x.UserClaims, opt => opt.MapFrom(src => src.UserClaims.Select(x => new ApiResourceClaim { Type = x })));

            CreateMap<UpdateApiResource, ApiResource>(MemberList.Source).ForMember(x => x.UserClaims, opt => opt.MapFrom(src => src.UserClaims.Select(x => new ApiResourceClaim { Type = x })));
            //CreateMap<ApiResource, CreateApiResouce>().ForMember(x => x.UserClaims, opt => opt.MapFrom(src => src.UserClaims.Select(c=>c.Type) ));

            CreateMap<ApiResource, ApiResourceDto>(MemberList.Destination).ForMember(x => x.UserClaims, opt => opt.MapFrom(src => src.UserClaims.Select(c => c.Type)));

            CreateMap<ApiResource, ApiResourceListDto>(MemberList.Destination);

            //apiscope
            CreateMap<ApiScope, ApiScopeDto>(MemberList.None).ForMember(x => x.UserClaims, opt => opt.MapFrom(src => src.UserClaims.Select(c => c.Type)));
            CreateMap<CreateApiScopeDto, ApiScope>(MemberList.None).ForMember(x => x.UserClaims, opt => opt.MapFrom(src => src.UserClaims.Select(x => new ApiScopeClaim { Type = x })));
            CreateMap<UpdateScopeDto, ApiScope>(MemberList.None).ForMember(x => x.UserClaims, opt => opt.MapFrom(src => src.UserClaims.Select(x => new ApiScopeClaim { Type = x })));



            //apiscret
            CreateMap<CreateApiSecretDto, ApiResourceSecret>(MemberList.None);
            CreateMap<CreateApiPropertiesDto, ApiResourceProperty>(MemberList.None);
        }
    }
}
