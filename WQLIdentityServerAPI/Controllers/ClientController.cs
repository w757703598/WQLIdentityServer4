using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WQLIdentity.Application.Dtos.Clients;
using WQLIdentity.Application.Interfaces;
using WQLIdentityServer.Infra.Dto;
using WQLIdentityServerAPI.Configurations.Consts;

namespace WQLIdentityServerAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Policy = PolicyConst.Admin, AuthenticationSchemes = "Bearer")]
    public class ClientController : BaseApiController
    {
        private IClientAppService _clientAppService;
        public ClientController(IClientAppService clientAppService)
        {
            _clientAppService = clientAppService;
        }

        [HttpPost]
        public async Task<IActionResult> AddClient([FromBody] CreateClientDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _clientAppService.AddClient(input);
            return ResultResponse(result, "创建客户端成功");
        }

        [HttpGet]
        public ActionResult<Pagelist< ClientListDto>> GetClients([FromQuery]PageInputDto pageInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = _clientAppService.GetClients(pageInput);
            return Ok( result);


        }

        [HttpGet]
        public async Task<ActionResult<ClientDto>> GetClient(int Id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _clientAppService.GetClient(Id);
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateClient(UpdateClientDto client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _clientAppService.UpdateClient(client);
            return ResultResponse(result,"修改客户端成功");
        }
        [HttpPost]
        public async Task<IActionResult> RemoveClient([FromBody]int clientId)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _clientAppService.RemoveClient(clientId);
            return ResultResponse(result, "移除客户端成功");
        }
         
        [HttpGet]
        public ActionResult<Pagelist<ClientSecretDto>> GetApiSecrets([FromQuery]PageInputDto pageInput, int clientId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = _clientAppService.GetClientSecrets(pageInput, clientId);
            return result;
        }
        /// <summary>
        /// 添加ApiScret
        /// </summary>
        /// <param name="apiSecret"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddApiSecret(ClientSecretDto apiSecret)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _clientAppService.AddClientSecretAsync(apiSecret);
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
            var result = await _clientAppService.DeleteClientSecretAsync(secretId);
            return ResultResponse(result, "删除ApiSecret成功");
        }

        [HttpGet]
        public ActionResult<Pagelist<ClientPropertyDto>> GetApiProperties([FromQuery]PageInputDto pageInput, int clientId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = _clientAppService.GetClientProperties(pageInput, clientId);
            return result;
        }
        /// <summary>
        /// 添加ApiProperties
        /// </summary>
        /// <param name="apiProperties"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddApiProperties(ClientPropertyDto apiProperties)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var result = await _clientAppService.AddClientPropertyAsync(apiProperties);
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
            var result = await _clientAppService.DeleteClientPropertyAsync(propertiesId);
            return ResultResponse(result, "删除ApiProperties成功");
        }
    }
}