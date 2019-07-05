using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WQLIdentity.Application.Dtos;
using WQLIdentity.Application.Dtos.UserManager;
using WQLIdentityServer.Infra.Dto;
using System.Linq;
using WQLIdentity.Application.Dtos.Roles;

namespace WQLIdentity.Application.Interfaces
{
    public interface IUserAppService
    {
        Task<IdentityResult> Register(CreateUserDto model);

        Pagelist<UserListDto> GetUsers(PageInputDto input);

        Task<UserDetailDto> GetUser(string userId);

        Task<IdentityResult> DeleteAsync(string userId);

        Task<IdentityResult> UpdateAsync(UpdateUserDto userDto);
        Task<IdentityResult> ChangePasswordAsync(PasswordDto dto);
        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>角色名称集合</returns>
        Task<IEnumerable<string>> GetUserRolesAsync(string userId);
        /// <summary>
        /// 给用户添加角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IdentityResult> AddToRoleAsync(UserRoleDto dto);
        /// <summary>
        /// 移除用户角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IdentityResult> RemoveUserRoleAsync(UserRoleDto dto);
        /// <summary>
        /// 创建用户声明
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IdentityResult> CreateUserClaim(CreateUserOrRoleClaimDto dto);
        /// <summary>
        /// 获取所有用户声明
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<ClaimViewDto>> GetUserClaims(string userId);
        /// <summary>
        /// 移除用户声明
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IdentityResult> RemoveUserClaim(CreateUserOrRoleClaimDto dto);





    }
}
