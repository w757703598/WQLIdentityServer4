using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WQLIdentity.Application.Dtos.ApiResources;
using WQLIdentity.Application.Interfaces;

namespace WQLIdentityServerAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiScopeController : BaseApiController
    {
        private IApiScopeService  _apiScopeService;


        public ApiScopeController(IApiScopeService apiScopeService)
        {
            _apiScopeService = apiScopeService;

        }
        /// <summary>
        /// 更新ApiScope
        /// </summary>
        /// <param name="updateScope"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateScope(ApiScopeDto updateScope)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _apiScopeService.UpdateApiScope(updateScope);
            return ResultResponse(result, "更新ApiScope成功");
        }
        /// <summary>
        /// 获取ApiScope
        /// </summary>
        /// <param name="scopeName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> UpdateScope(string  scopeName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _apiScopeService.GetScope(scopeName);
            return Ok(result);
        }
        /// <summary>
        /// 获取ApiScope
        /// </summary>
        /// <param name="scopeName"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateScope(ApiScopeDto  apiScope)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _apiScopeService.AddApiScope(apiScope);
            return ResultResponse(result, "添加ApiScope成功");
        }
        /// <summary>
        /// 获取ApiScope
        /// </summary>
        /// <param name="apiScope"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteScope(ApiScopeDto apiScope)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _apiScopeService.RemoveApiScope(apiScope);
            return ResultResponse(result, "删除ApiScope成功");
        }
    }
}