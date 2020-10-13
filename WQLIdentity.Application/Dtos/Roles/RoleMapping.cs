using AutoMapper;
using WQLIdentity.Infra.Data.Entities;

namespace WQLIdentity.Application.Dtos.Roles
{
    public class RoleMapping : Profile
    {
        public RoleMapping()
        {
            CreateMap<CreateRoleDto, ApplicationRole>(MemberList.None);
            CreateMap<UpdateRoleDto, ApplicationRole>(MemberList.None);
            CreateMap<ApplicationRole, RoleListDto>(MemberList.None);

        }
    }
}
