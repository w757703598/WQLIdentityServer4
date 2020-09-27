using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Threading.Tasks;

namespace WQLIdentityServerAPI.Services
{
    public class ProfileService : IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            throw new NotImplementedException();
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            throw new NotImplementedException();
        }
    }
}
