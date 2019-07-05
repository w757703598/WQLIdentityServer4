using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WQLIdentity.Application.Dtos;
using WQLIdentity.Application.Dtos.Roles;
using WQLIdentity.Application.Interfaces;
using WQLIdentity.Domain.Entities;
using WQLIdentityServer.Infra.Dto;
using WQLIdentityServerAPI.Configurations.Consts;

namespace WQLIdentityServerAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles ="Administrator", AuthenticationSchemes = "Bearer")]
    public class RoleAppController : BaseApiController
    {
        private IRoleAppService _roleAppService;
        public RoleAppController(IRoleAppService roleAppService)
        {
            _roleAppService = roleAppService;
        }
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Pagelist<RoleListDto>> GetRoles([FromQuery]PageInputDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }

            var result = _roleAppService.GetRoles(input);
            return result;
          
        }
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="roleDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleDto roleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _roleAppService.CreateRoleAsync(roleDto);
            return IdentityResponse(result, "创建角色成功");
        }
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="roleDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateRoleDto roleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _roleAppService.UpdateRoleAsync(roleDto);
            return IdentityResponse(result, "修改角色成功");
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteRole([FromBody] string roleId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _roleAppService.DeleteRoleAsync(roleId);
            return IdentityResponse(result, "删除角色成功");
        }
        /// <summary>
        /// 创建角色声明
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateRoleClaim(CreateUserOrRoleClaimDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _roleAppService.CreateRoleClaim(dto);
            return IdentityResponse(result, "创建角色声明成功");
        }
        /// <summary>
        /// 移除角色声明
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]

        public async Task<IActionResult> RemoveRoleClaim(CreateUserOrRoleClaimDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _roleAppService.RemoveRoleClaim(model);
            return IdentityResponse(result, "删除角色声明成功");
        }
        /// <summary>
        /// 获取角色声明
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<ClaimViewDto>>> GetRoleClaims(string roleId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _roleAppService.GetRoleClaims(roleId);
            return Ok(result);
        }
    }
}