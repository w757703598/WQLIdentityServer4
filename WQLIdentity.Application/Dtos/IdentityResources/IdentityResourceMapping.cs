using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using System.Linq;

namespace WQLIdentity.Application.Dtos.IdentityResources
{
    public class IdentityResourceMapping : Profile
    {
        public IdentityResourceMapping()
        {



            CreateMap<CreateIdentityResourceDto, IdentityResource>(MemberList.None).ForMember(x => x.UserClaims, opt => opt.MapFrom(src => src.UserClaims.Select(x => new IdentityResourceClaim { Type = x })));


            CreateMap<IdentityResource, IdentityResourceDto>(MemberList.Destination)
                .ForMember(x => x.UserClaims, opt => opt.MapFrom(src => src.UserClaims.Select(x => x.Type)));



            CreateMap<IdentityResourcePropertyDto, IdentityResourceProperty>(MemberList.None);

            CreateMap<IdentityResource, IdentityResourceListDto>(MemberList.None);
        }
    }
}
