using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WQLIdentity.Application.Interfaces;
using WQLIdentityServerAPI.IdentityServers.Services;

namespace WQLIdentityServerAPI.IdentityServers
{
    /// <summary>
    /// 短信验证
    /// </summary>
    public class SmsAuthCodeValidator : IExtensionGrantValidator
    {
        public string GrantType => "sms";
        private readonly IAuthCodeService _authCodeService;
        private readonly IUserAppService _userAppService;
        public SmsAuthCodeValidator(IAuthCodeService authCodeService,IUserAppService userAppService)
        {
            _authCodeService = authCodeService;
            _userAppService = userAppService;
        }

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var phone = context.Request.Raw["phone"];
            var code = context.Request.Raw["auth_code"];
            var errResult= new GrantValidationResult(TokenRequestErrors.InvalidGrant);
            if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(code))
            {
                context.Result = errResult;
                return;
            }
            if (!_authCodeService.Validate(phone, code))
            {
                context.Result = errResult;
                return;
            }
            var user = await _userAppService.CheckUserByPhone(phone);
            //if (user)
            //{
            //    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant,"该手机号未注册信息"); ;
            //    return;
            //}
            var userId = await _userAppService.CheckOrCreate(phone);
            if (userId <= 0)
            {
                context.Result = errResult;
                return;
            }
            context.Result = new GrantValidationResult(userId.ToString(), GrantType);
        }
    }
}
