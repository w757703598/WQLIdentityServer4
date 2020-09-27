using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using WQLIdentity.Application.Dtos;
using WQLIdentity.Application.Dtos.UserManager;
using WQLIdentityServer.Infra.Dto;

namespace WQLIdentity.Application.Interfaces
{
    public interface IUserAppService
    {
        Task<IdentityResult> Register(CreateUserDto model);

        Pagelist<UserListDto> GetUsers(PageInputDto input);

        Task<UserDetailDto> GetUser(string userId);

        Task<IdentityResult> DeleteAsync(string userId);

        Task<IdentityResult> UpdateAsync(UpdateUserDto userDto);
        /// <summary>
        /// 根据手机号判断创建还是返回用户
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <returns></returns>
        Task<int> CheckOrCreate(string phone);
        /// <summary>
        /// 检查手机号是否已经注册
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        Task<bool> CheckUserByPhone(string phone);
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
