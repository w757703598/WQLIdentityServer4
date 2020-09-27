namespace WQLIdentityServerAPI.IdentityServers.Services
{
    public interface IAuthCodeService
    {
        /// <summary>
        /// 验证手机验证码是否正确
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        bool Validate(string phone, string code);
    }
}
