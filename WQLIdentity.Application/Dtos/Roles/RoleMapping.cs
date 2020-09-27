using AutoMapper;
using WQLIdentity.Infra.Data.Entities;

namespace WQLIdentity.Application.Dtos.Roles
{
    public class RoleMapping : Profile
    {
        public RoleMapping()
        {
            CreateMap<CreateRoleDto, ApplicationRole>(MemberList.None);
            CreateMap<UpdateRoleDto, ApplicationUser>(MemberList.None);

        }
    }
}
