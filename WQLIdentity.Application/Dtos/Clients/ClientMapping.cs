using AutoMapper;
using IdentityServer4.EntityFramework.Entities;

namespace WQLIdentity.Application.Dtos.Clients
{
    public class ClientMapping : Profile
    {
        public ClientMapping()
        {
          CreateMap<Client, ClientListDto>(MemberList.None);

            CreateMap<CreateClientDto, Client>(MemberList.None);

            CreateMap<ClientDto, Client>(MemberList.None).ReverseMap();


            CreateMap<ClientGrantType, string>(MemberList.None)
                   .ConstructUsing(src => src.GrantType)
                   .ReverseMap()
                   .ForMember(dest => dest.GrantType, opt => opt.MapFrom(src => src));

            CreateMap<ClientRedirectUri, string>(MemberList.None)
                .ConstructUsing(src => src.RedirectUri)
                .ReverseMap()
                .ForMember(dest => dest.RedirectUri, opt => opt.MapFrom(src => src));

            CreateMap<ClientPostLogoutRedirectUri, string>(MemberList.None)
                .ConstructUsing(src => src.PostLogoutRedirectUri)
                .ReverseMap()
                .ForMember(dest => dest.PostLogoutRedirectUri, opt => opt.MapFrom(src => src));

            CreateMap<ClientScope, string>(MemberList.None)
                .ConstructUsing(src => src.Scope)
                .ReverseMap()
                .ForMember(dest => dest.Scope, opt => opt.MapFrom(src => src));

            CreateMap<ClientIdPRestriction, string>(MemberList.None)
                .ConstructUsing(src => src.Provider)
                .ReverseMap()
                .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => src));

            CreateMap<ClientCorsOrigin, string>(MemberList.None)
                .ConstructUsing(src => src.Origin)
                .ReverseMap()
                .ForMember(dest => dest.Origin, opt => opt.MapFrom(src => src));



            CreateMap<ClientSecret, ClientSecretDto>(MemberList.None);
            CreateMap<ClientSecretDto, ClientSecret>(MemberList.None);
            CreateMap<ClientClaim, ClientClaimDto>(MemberList.None);
            CreateMap<ClientProperty, ClientPropertyDto>(MemberList.None);
            CreateMap<ClientPropertyDto, ClientProperty>(MemberList.None);


            CreateMap<UpdateClientDto, Client>(MemberList.None);


        }
    }
}
