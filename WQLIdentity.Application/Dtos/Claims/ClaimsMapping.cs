using AutoMapper;

namespace WQLIdentity.Application.Dtos.Claims
{
    public class ClaimsMapping : Profile
    {
        public ClaimsMapping()
        {
            CreateMap<CreateClaimDto, WQLIdentity.Domain.Entities.Claims>(MemberList.Source);
        }
    }
}
