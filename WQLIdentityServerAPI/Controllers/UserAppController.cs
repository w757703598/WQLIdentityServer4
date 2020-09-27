using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WQLIdentity.Application.Dtos;
using WQLIdentity.Application.Dtos.UserManager;
using WQLIdentity.Application.Interfaces;
using WQLIdentityServer.Infra.Dto;
using WQLIdentityServerAPI.Configurations.Consts;

namespace WQLIdentityServerAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserAppController : BaseApiController
    {
        private readonly IUserAppService _userAppService;
        public UserAppController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = PolicyConst.Admin, AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }

            var result = await _userAppService.Register(input);
            return IdentityResponse(result, "注册用户成功");
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = PolicyConst.Admin, AuthenticationSchemes = "Bearer")]
        public ActionResult<Pagelist<UserListDto>> GetUsers([FromQuery] PageInputDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = _userAppService.GetUsers(input);
            return result;


        }
        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = PolicyConst.Manager, AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<UserDetailDto>> GetUser(string userId)
        {
            var temp = User;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _userAppService.GetUser(userId);
            return result;
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = PolicyConst.Manager, AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DleteUser([FromBody]string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _userAppService.DeleteAsync(userId);
            return IdentityResponse(result, "删除用户成功");
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = PolicyConst.Admin, AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateUser([FromBody]UpdateUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _userAppService.UpdateAsync(user);
            return IdentityResponse(result, "修改用户成功");
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = PolicyConst.Manager, AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ChangePassword([FromBody]PasswordDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _userAppService.ChangePasswordAsync(dto);
            return IdentityResponse(result, "修改密码成功");
        }
        /// <summary>
        /// 分配用户角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = PolicyConst.Manager, AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddToRole([FromBody]UserRoleDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _userAppService.AddToRoleAsync(dto);
            return IdentityResponse(result, "分配角色成功");
        }
        /// <summary>
        /// 移除用户角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = PolicyConst.Manager, AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> RemoveUserRole([FromBody]UserRoleDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _userAppService.RemoveUserRoleAsync(dto);
            return IdentityResponse(result, "移除用户角色成功");
        }
        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = PolicyConst.Manager, AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<string>>> GetUserRoles(string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _userAppService.GetUserRolesAsync(userId);
            return Ok(result);
        }
        /// <summary>
        /// 创建用户声明
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = PolicyConst.Manager, AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateUserClaim(CreateUserOrRoleClaimDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _userAppService.CreateUserClaim(model);
            return IdentityResponse(result, "添加用户声明成功");

        }
        /// <summary>
        /// 移除用户声明
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = PolicyConst.Manager, AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> RemoveUserClaim(CreateUserOrRoleClaimDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _userAppService.RemoveUserClaim(model);
            return IdentityResponse(result, "移除用户声明成功");
        }
        /// <summary>
        /// 获取用户声明
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = PolicyConst.Manager, AuthenticationSchemes = "Bearer")]

        public async Task<IActionResult> GetUserClaims(string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _userAppService.GetUserClaims(userId);
            return Ok(result);
        }

    }
}