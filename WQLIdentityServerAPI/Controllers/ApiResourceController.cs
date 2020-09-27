using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WQLIdentity.Application.Dtos.ApiResources;
using WQLIdentity.Application.Interfaces;
using WQLIdentityServer.Infra.Dto;
using WQLIdentityServerAPI.Configurations.Consts;

namespace WQLIdentityServerAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Policy = PolicyConst.Admin, AuthenticationSchemes = "Bearer")]
    public class ApiResourceController : BaseApiController
    {
        private IApiResourceService _apiresourceService;


        public ApiResourceController(IApiResourceService apiresourceService)
        {
            _apiresourceService = apiresourceService;

        }
        /// <summary>
        /// 获取API资源列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Pagelist<ApiResourceListDto>> GetApiResources([FromQuery]PageInputDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            return _apiresourceService.GetApiResources(input);
        }
        /// <summary>
        /// 获取API资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ApiResourceDto>> GetApiResource(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            return await _apiresourceService.GetApiResource(id);
        }
        /// <summary>
        /// 创建api资源
        /// </summary>
        /// <param name="apiResource"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateApiResource([FromBody]CreateApiResouce apiResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _apiresourceService.CreateApiResource(apiResource);
            return ResultResponse(result, "创建api资源成功");
        }
        /// <summary>
        /// 修改API资源
        /// </summary>
        /// <param name="apiResource"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateApiResource([FromBody]UpdateApiResource apiResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _apiresourceService.Update(apiResource);
            return ResultResponse(result, "修改api资源成功");
        }
        /// <summary>
        /// 移除api资源
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RemoveApiResource([FromBody]int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _apiresourceService.Remove(Id);
            return ResultResponse(result, "移除api资源成功");
        }
        /// <summary>
        /// 获取API资源的scopes
        /// </summary>
        /// <param name="pageInput">排序参数</param>
        /// <param name="apiresourceId">资源id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Pagelist<ApiScopeDto>> GetApiScopes([FromQuery]PageInputDto pageInput, int apiresourceId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = _apiresourceService.GetScopes(pageInput, apiresourceId);
            return result;
        }

        /// <summary>
        /// 添加ApiResourceScope
        /// </summary>
        /// <param name="apiScope"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddApiScope(ApiScopeResourceDto apiScope)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _apiresourceService.AddScope(apiScope);
            return ResultResponse(result, "添加ApiScope成功");
        }
        /// <summary>
        /// 删除scope
        /// </summary>
        /// <param name="scopeId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RemoveScope(ApiScopeResourceDto scopeId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _apiresourceService.RemoveScope(scopeId);
            return ResultResponse(result, "删除ApiScope成功");
        }

        /// <summary>
        /// 获取ApiSecret列表
        /// </summary>
        /// <param name="pageInput"></param>
        /// <param name="apiresourceId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Pagelist<ApiResourceSecret>> GetApiSecrets([FromQuery]PageInputDto pageInput, int apiresourceId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = _apiresourceService.GetSecrets(pageInput, apiresourceId);
            return result;
        }
        /// <summary>
        /// 添加ApiScret
        /// </summary>
        /// <param name="apiSecret"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddApiSecret(CreateApiSecretDto apiSecret)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _apiresourceService.AddSecret(apiSecret);
            return ResultResponse(result, "添加ApiSecret成功");
        }
        /// <summary>
        /// 移除ApiSecret
        /// </summary>
        /// <param name="secretId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RemoveSecret([FromBody]int secretId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _apiresourceService.RemoveSecret(secretId);
            return ResultResponse(result, "删除ApiSecret成功");
        }
        /// <summary>
        /// 获取ApiProperties列表
        /// </summary>
        /// <param name="pageInput"></param>
        /// <param name="apiresourceId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Pagelist<ApiResourceProperty>> GetApiProperties([FromQuery]PageInputDto pageInput, int apiresourceId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = _apiresourceService.GetProperties(pageInput, apiresourceId);
            return result;
        }
        /// <summary>
        /// 添加ApiProperties
        /// </summary>
        /// <param name="apiProperties"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddApiProperties(CreateApiPropertiesDto apiProperties)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _apiresourceService.AddProperties(apiProperties);
            return ResultResponse(result, "添加ApiProperties成功");
        }
        /// <summary>
        /// 删除ApiProperties
        /// </summary>
        /// <param name="propertiesId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RemoveApiProperties([FromBody]int propertiesId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _apiresourceService.RemovePropertiest(propertiesId);
            return ResultResponse(result, "删除ApiProperties成功");
        }
    }
}

