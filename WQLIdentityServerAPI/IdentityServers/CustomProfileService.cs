
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WQLIdentity.Infra.Data.Entities;

namespace WQLIdentityServerAPI.IdentityServers
{

    // <summary>
    /// IProfileService to integrate with ASP.NET Identity.
    /// </summary>
    /// <typeparam name="TUser">The type of the user.</typeparam>
    /// <seealso cref="IdentityServer4.Services.IProfileService" />
    public class CustomProfileService<TUser> : IProfileService
        where TUser : ApplicationUser
    {
        /// <summary>
        /// The claims factory.
        /// </summary>
        protected readonly IUserClaimsPrincipalFactory<TUser> ClaimsFactory;

        /// <summary>
        /// The logger
        /// </summary>
        protected readonly ILogger<CustomProfileService<TUser>> Logger;

        /// <summary>
        /// The user manager.
        /// </summary>
        protected readonly UserManager<TUser> UserManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CusromProfileService{TUser}"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="claimsFactory">The claims factory.</param>
        public CustomProfileService(UserManager<TUser> userManager,
            IUserClaimsPrincipalFactory<TUser> claimsFactory)
        {
            UserManager = userManager;
            ClaimsFactory = claimsFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileService{TUser}"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="claimsFactory">The claims factory.</param>
        /// <param name="logger">The logger.</param>
        public CustomProfileService(UserManager<TUser> userManager,
            IUserClaimsPrincipalFactory<TUser> claimsFactory,
            ILogger<CustomProfileService<TUser>> logger)
        {
            UserManager = userManager;
            ClaimsFactory = claimsFactory;
            Logger = logger;
        }

        /// <summary>
        /// This method is called whenever claims about the user are requested (e.g. during token creation or via the userinfo endpoint)
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject?.GetSubjectId();
            if (sub == null) throw new Exception("No sub claim present");

            var user = await UserManager.FindByIdAsync(sub);
            if (user == null)
            {
                Logger?.LogWarning("No user found matching subject Id: {0}", sub);
            }
            else
            {
                var calims = GetClaimsFromUserAsync(user);
                //context.AddRequestedClaims(calims);
                var principal = await ClaimsFactory.CreateAsync(user);
                if (principal == null) throw new Exception("ClaimsFactory failed to create a principal");

                //context.IssuedClaims = principal.Claims.ToList();
                calims.AddRange(principal.Claims);
                context.AddRequestedClaims(calims);
            }
        }
        public List<Claim> GetClaimsFromUserAsync(TUser user)
        {

            var claims = new List<Claim> {
                //new Claim(JwtClaimTypes.Subject,user.Id.ToString()),
                new Claim("username",user.Name),
                //new Claim("name",user.Name),
                new Claim("createdon",user.CreatedOn.ToString()),
                new Claim("picture",user.Picture??"")
            };

 
            //var role = await UserManager.GetRolesAsync(user);
            //role.ToList().ForEach(f =>
            //{
            //    claims.Add(new Claim(JwtClaimTypes.Role, f));
            //});

            return claims;
        }
        /// <summary>
        /// This method gets called whenever identity server needs to determine if the user is valid or active (e.g. if the user's account has been deactivated since they logged in).
        /// (e.g. during token issuance or validation).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject?.GetSubjectId();
            if (sub == null) throw new Exception("No subject Id claim present");

            var user = await UserManager.FindByIdAsync(sub);
            if (user == null)
            {
                Logger?.LogWarning("No user found matching subject Id: {0}", sub);
            }

            context.IsActive = user != null;
        }
    }
}
