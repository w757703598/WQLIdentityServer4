using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using WQLIdentity.Application.Dtos;
using WQLIdentity.Application.Dtos.Roles;
using WQLIdentityServer.Infra.Dto;

namespace WQLIdentity.Application.Interfaces
{
    public interface IRoleAppService
    {
        Pagelist<RoleListDto> GetRoles(PageInputDto input);
        Task<IdentityResult> CreateRoleAsync(CreateRoleDto roleDto);
        Task<IdentityResult> UpdateRoleAsync(UpdateRoleDto roleDto);
        Task<IdentityResult> DeleteRoleAsync(string roleId);

        Task<IdentityResult> CreateRoleClaim(CreateUserOrRoleClaimDto dto);
        Task<IdentityResult> RemoveRoleClaim(CreateUserOrRoleClaimDto dto);
        Task<List<ClaimViewDto>> GetRoleClaims(string roleId);

    }
}
