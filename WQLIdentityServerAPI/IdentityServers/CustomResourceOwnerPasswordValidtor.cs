using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WQLIdentity.Infra.Data.Entities;
using WQLIdentityServerAPI.Models.Identity;
using static IdentityModel.OidcConstants;

namespace WQLIdentityServerAPI.IdentityServers
{
    public class CustomResourceOwnerPasswordValidtor<TUser, TRole> : IResourceOwnerPasswordValidator
       where TUser : ApplicationUser
       where TRole : ApplicationRole
    {
        private readonly SignInManager<TUser> _signInManager;
        private IEventService _events;
        private readonly UserManager<TUser> _userManager;
        private readonly RoleManager<TRole> _roleManager;
        private readonly ILogger<CustomResourceOwnerPasswordValidtor<TUser, TRole>> _logger;




        /// <summary>
        /// Initializes a new instance of the <see cref="CustomResourceOwnerPasswordValidtor{TUser,TRole}"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The sign in manager.</param>
        /// <param name="signInManager"></param>
        /// <param name="events">The events.</param>
        /// <param name="logger">The logger.</param>
        public CustomResourceOwnerPasswordValidtor(
            UserManager<TUser> userManager,
            RoleManager<TRole> roleManager,
            SignInManager<TUser> signInManager,
            IEventService events,
            ILogger<CustomResourceOwnerPasswordValidtor<TUser, TRole>> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _events = events;
            _logger = logger;

        }

        /// <summary>
        /// Validates the resource owner password credential
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await _userManager.FindByNameAsync(context.UserName);

            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, context.Password, true);
                if (result.Succeeded)
                {
                    var sub = await _userManager.GetUserIdAsync(user);

                    _logger.LogInformation("Credentials validated for username: {username}", context.UserName);
                    await _events.RaiseAsync(new UserLoginSuccessEvent(context.UserName, sub, context.UserName, interactive: false));

                    var claims = new List<Claim>();
                    var userRoles = await _userManager.GetRolesAsync(user);
                    foreach (var roleid in userRoles)
                    {
                        var roles = await _roleManager.FindByNameAsync(roleid);
                        if (roles != null)
                        {
                            var roleClaim = await _roleManager.GetClaimsAsync(roles);
                            claims.AddRange(roleClaim.ToList().FindAll(c => c.Type == AuthorizeClaims.ProductFault || c.Type == AuthorizeClaims.Simulation || c.Type == AuthorizeClaims.PartDesin || c.Type == AuthorizeClaims.Config));
                        }

                    }
                    //添加用户声明.
                    var userClaims = await _userManager.GetClaimsAsync(user);
                    claims.AddRange(userClaims.ToList().FindAll(c => c.Type == AuthorizeClaims.ProductFault || c.Type == AuthorizeClaims.Simulation || c.Type == AuthorizeClaims.PartDesin || c.Type == AuthorizeClaims.Config));

                    var data = new Dictionary<string, object>();
                    data.Add("Data", new { user.Id, user.Name, claims = claims.Select(d => new { d.Type, d.Value }) });
                    context.Result = new GrantValidationResult(sub, AuthenticationMethods.Password, customResponse: data);
                    return;
                }
                else if (result.IsLockedOut)
                {
                    _logger.LogInformation("Authentication failed for username: {username}, reason: locked out", context.UserName);
                    await _events.RaiseAsync(new UserLoginFailureEvent(context.UserName, "locked out", interactive: false));
                }
                else if (result.IsNotAllowed)
                {
                    _logger.LogInformation("Authentication failed for username: {username}, reason: not allowed", context.UserName);
                    await _events.RaiseAsync(new UserLoginFailureEvent(context.UserName, "not allowed", interactive: false));
                }
                else
                {
                    _logger.LogInformation("Authentication failed for username: {username}, reason: invalid credentials", context.UserName);
                    await _events.RaiseAsync(new UserLoginFailureEvent(context.UserName, "invalid credentials", interactive: false));
                }
            }
            else
            {
                _logger.LogInformation("No user found matching username: {username}", context.UserName);
                await _events.RaiseAsync(new UserLoginFailureEvent(context.UserName, "invalid username", interactive: false));
            }

            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
        }


    }

}
