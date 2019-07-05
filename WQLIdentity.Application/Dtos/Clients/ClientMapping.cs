using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using WQLIdentity.Application.Dtos.ApiResources;

namespace WQLIdentity.Application.Dtos.Clients
{
    public class ClientMapping:Profile
    {
        public ClientMapping()
        {
            CreateMap<CreateClientDto, Client>();

            CreateMap<ClientDto, Client>();


            CreateMap<ClientGrantType, string>()
                   .ConstructUsing(src => src.GrantType)
                   .ReverseMap()
                   .ForMember(dest => dest.GrantType, opt => opt.MapFrom(src => src));

            CreateMap<ClientRedirectUri, string>()
                .ConstructUsing(src => src.RedirectUri)
                .ReverseMap()
                .ForMember(dest => dest.RedirectUri, opt => opt.MapFrom(src => src));

            CreateMap<ClientPostLogoutRedirectUri, string>()
                .ConstructUsing(src => src.PostLogoutRedirectUri)
                .ReverseMap()
                .ForMember(dest => dest.PostLogoutRedirectUri, opt => opt.MapFrom(src => src));

            CreateMap<ClientScope, string>()
                .ConstructUsing(src => src.Scope)
                .ReverseMap()
                .ForMember(dest => dest.Scope, opt => opt.MapFrom(src => src));

            CreateMap<ClientIdPRestriction, string>()
                .ConstructUsing(src => src.Provider)
                .ReverseMap()
                .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => src));

            CreateMap<ClientCorsOrigin, string>()
                .ConstructUsing(src => src.Origin)
                .ReverseMap()
                .ForMember(dest => dest.Origin, opt => opt.MapFrom(src => src));



            CreateMap<ClientSecret, ClientSecretDto>();
            CreateMap<ClientSecretDto, ClientSecret>();
            CreateMap<ClientClaim, ClientClaimDto>();
            CreateMap<ClientProperty, ClientPropertyDto>();
            CreateMap<ClientPropertyDto, ClientProperty>();


            CreateMap<UpdateClientDto, Client>();


        }
    }
}
