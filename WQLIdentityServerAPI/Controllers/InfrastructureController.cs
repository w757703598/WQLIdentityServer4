using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WQLIdentity.Application.Interfaces;
using WQLIdentity.Domain.Entities;
using WQLIdentityServer.Infra.Dto;
using WQLIdentityServer.Infra.Helpers;

namespace WQLIdentityServerAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InfrastructureController : ControllerBase
    {
        private IApplicationBaseService<Claims> _claimService;
        public InfrastructureController(IApplicationBaseService<Claims> claimService)
        {
            _claimService = claimService;

        }
        /// <summary>
        /// 获取客户端类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<SelectItemDto>> GetClientTypes()
        {
            var result = EnumHelpers.ToSelectList<ClientType>();
            return result;
        }
        /// <summary>
        /// 协议类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<SelectItemDto>> GetProtocolTypes()
        {
            return new List<SelectItemDto> { new SelectItemDto("oidc", "OpenID Connect") };
        }
        /// <summary>
        /// 获取声明信息用于下拉框使用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetClaims()
        {
            var claims = _claimService.GetAll();
            var temp = claims.GroupBy(d => d.Type).Select(c => new { type = c.Key, value = c.Select(v => v.Value) });
            return Ok(temp);
        }


        /// <summary>
        /// 授权类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetGrantTypes()
        {
            return new List<string>
                {
                    "implicit",
                    "client_credentials",
                    "authorization_code",
                    "hybrid",
                    "password",
                    "urn:ietf:params:oauth:grant-type:device_code"
                }; ;
        }
        /// <summary>
        /// 密钥类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetSecretTypes()
        {
            return new List<string>
                {
                   "SharedSecret",
                "X509Thumbprint",
                "X509Name",
                "X509CertificateBase64"
                }; ;
        }
    }
}