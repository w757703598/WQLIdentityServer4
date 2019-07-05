using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WQLIdentity.Application.Dtos.IdentityResources;
using WQLIdentity.Application.Interfaces;
using WQLIdentityServer.Infra.Dto;
using WQLIdentityServerAPI.Configurations.Consts;

namespace WQLIdentityServerAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Policy = PolicyConst.Admin, AuthenticationSchemes = "Bearer")]
    public class IdentityResourceController : BaseApiController
    {
        private IIdentityResourceService  _identityResourceService;


        public IdentityResourceController(IIdentityResourceService identityResourceService)
        {
            _identityResourceService = identityResourceService;

        }
        /// <summary>
        /// 获取身份资源列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Pagelist<IdentityResourceListDto>> GetIdentityResources([FromQuery]PageInputDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            return _identityResourceService.GetIdentityResourcesAsync(input);
        }
        /// <summary>
        /// 获取身份信息资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IdentityResourceDto>> GetIdentityResource(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            return await _identityResourceService.GetIdentityResourceAsync(id);
        }
        /// <summary>
        /// 创建身份资源
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateIdentityResource([FromBody]CreateIdentityResourceDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _identityResourceService.AddIdentityResourceAsync(input);
            return ResultResponse(result, "创建身份资源成功");
        }
        /// <summary>
        /// 修改API资源
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateIdentityResource([FromBody]CreateIdentityResourceDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _identityResourceService.UpdateIdentityResourceAsync(input);
            return ResultResponse(result, "修改身份资源成功");
        }
        /// <summary>
        /// 移除api资源
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RemoveIdentityResource([FromBody]int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _identityResourceService.DeleteIdentityResourceAsync(Id);
            return ResultResponse(result, "移除身份资源成功");
        }


        /// <summary>
        /// 获取身份资源属性列表
        /// </summary>
        /// <param name="pageInput"></param>
        /// <param name="identityResourceId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Pagelist<IdentityResourceProperty>> GetIdentityProperties([FromQuery]PageInputDto pageInput, int identityResourceId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result =  _identityResourceService.GetIdentityResourcePropertiesAsync(pageInput, identityResourceId);
            return result;
        }
        /// <summary>
        /// 添加身份资源属性
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddIdentityProperties(IdentityResourcePropertyDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _identityResourceService.AddIdentityResourcePropertyAsync(input);
            return ResultResponse(result, "添加身份资源属性成功");
        }
        /// <summary>
        /// 删除身份资源属性
        /// </summary>
        /// <param name="propertiesId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RemoveIdentityProperties([FromBody]int propertiesId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _identityResourceService.DeleteIdentityResourcePropertyAsync(propertiesId);
            return ResultResponse(result, "删除身份资源属性成功");
        }
    }
}