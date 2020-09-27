namespace WQLIdentityServerAPI.IdentityServers.Services
{
    public class AuthCodeService : IAuthCodeService
    {
        public bool Validate(string phone, string code)
        {
            return true;
        }
    }
}
