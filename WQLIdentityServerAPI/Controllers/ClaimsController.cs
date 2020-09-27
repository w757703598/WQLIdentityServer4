using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WQLIdentity.Application.Dtos.Claims;
using WQLIdentity.Application.Interfaces;
using WQLIdentity.Domain.Entities;
using WQLIdentityServer.Infra.Dto;
using WQLIdentityServerAPI.Configurations.Consts;

namespace WQLIdentityServerAPI.Controllers
{
    /// <summary>
    /// 配置声明
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Policy = PolicyConst.Admin, AuthenticationSchemes = "Bearer")]
    public class ClaimsController : BaseApiController
    {
        private IApplicationBaseService<Claims> _claimService;
        private IMapper _mapper;

        public ClaimsController(IApplicationBaseService<Claims> claimService, IMapper mapper)
        {
            _claimService = claimService;
            _mapper = mapper;
        }
        /// <summary>
        /// 创建声明
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateClaim([FromBody]CreateClaimDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }
            var temp = _claimService.GetAll().FirstOrDefault(c => c.Type == dto.Type && c.Value == dto.Value);
            if (temp != null)
            {
                return ResultResponse(false, "已存在,请勿重复添加");
            }
            var claims = _mapper.Map<Claims>(dto);
            var result = await _claimService.AddAsync(claims);
            return ResultResponse(result, "插入成功");
        }
        /// <summary>
        /// 获取所有声明
        /// </summary>
        /// <param name="input">分页参数</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Pagelist<Claims>> GetClaims([FromQuery] PageInputDto input)
        {
            var claims = _claimService.GetAll(input);

            return Ok(claims);
        }
        /// <summary>
        /// 返回声明类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<string>> GetClaimTypes()
        {
            var types = _claimService.GetAll().Select(c => c.Type).Distinct().ToList();
            return Ok(types);
        }
        /// <summary>
        /// 移除声明
        /// </summary>
        /// <param name="claimId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult RemoveClaim([FromBody]int claimId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors);
            }

            var result = _claimService.Remove(claimId); ;
            return ResultResponse(result, "移除声明成功");
        }
    }
}