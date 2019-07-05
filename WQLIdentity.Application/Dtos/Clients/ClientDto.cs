using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace WQLIdentity.Application.Dtos.Clients
{
    public class ClientDto
    {

        #region 基础知识
        /// <summary>
        /// 指定是否启用客户端。默认为true。
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 客户端的唯一ID
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// 客户端机密列表 - 访问令牌端点的凭据。
        /// </summary>
        public List<ClientSecretDto> ClientSecrets { get; set; }
        /// <summary>
        /// 指定此客户端是否需要密钥才能从令牌端点请求令牌（默认为true）
        /// </summary>
        public bool RequireClientSecret { get; set; }
        /// <summary>
        /// 允许授权类型
        /// </summary>
        public List<string> AllowedGrantTypes { get; set; }
        /// <summary>
        /// 指定使用基于授权代码的授权类型的客户端是否必须发送校验密钥
        /// </summary>
        public bool RequirePkce { get; set; }
        /// <summary>
        /// 指定使用PKCE的客户端是否可以使用纯文本代码质询（不推荐 - 默认为false）
        /// </summary>
        public bool AllowPlainTextPkce { get; set; }
        /// <summary>
        /// 指定允许的URI以返回令牌或授权码
        /// </summary>
        public List<string> RedirectUris { get; set; }
        /// <summary>
        /// 默认情况下，客户端无权访问任何资源 - 通过添加相应的范围名称来指定允许的资源
        /// </summary>
        public List<string> AllowedScopes { get; set; }
        /// <summary>
        /// 指定此客户端是否可以请求刷新令牌（请求offline_access范围）
        /// </summary>
        public bool AllowOfflineAccess { get; set; }
        /// <summary>
        /// 指定是否允许此客户端通过浏览器接收访问令牌
        /// </summary>
        public bool AllowAccessTokensViaBrowser { get; set; }

        /// <summary>
        /// 字典根据需要保存任何自定义客户端特定值
        /// </summary>
        public List<ClientPropertyDto> Properties { get; set; }
        #endregion
        #region 认证/注销
        /// <summary>
        /// 指定在注销后重定向到的允许URI
        /// </summary>
        public List<string> PostLogoutRedirectUris { get; set; }
        /// <summary>
        /// 指定客户端的注销URI
        /// </summary>
        public string FrontChannelLogoutUri { get; set; }
        /// <summary>
        /// 指定是否应将用户的会话ID发送到FrontChannelLogoutUri。默认为true
        /// </summary>
        public bool FrontChannelLogoutSessionRequired { get; set; }
        /// <summary>
        /// 指定客户端的注销URI，以用于基于HTTP的反向通道注销
        /// </summary>
        public string BackChannelLogoutUri { get; set; }
        /// <summary>
        /// 指定是否应在请求中将用户的会话ID发送到BackChannelLogoutUri。默认为true
        /// </summary>
        public bool BackChannelLogoutSessionRequired { get; set; }
        /// <summary>
        /// 指定此客户端是否可以仅使用本地帐户或外部IdP。默认为true
        /// </summary>
        public bool EnableLocalLogin { get; set; }
        /// <summary>
        /// IdentityProviderRestrictions
        /// </summary>
        public List<string> IdentityProviderRestrictions { get; set; }
        /// <summary>
        /// 自上次用户进行身份验证以来的最长持续时间（以秒为单位）
        /// </summary>
        public int? UserSsoLifetime { get; set; }
        #endregion
        #region 令牌
        /// <summary>
        /// 以秒为单位识别令牌的生命周期（默认为300秒/ 5分钟）
        /// </summary>
        public int IdentityTokenLifetime { get; set; }

        /// <summary>
        /// 访问令牌的生命周期（以秒为单位）（默认为3600秒/ 1小时）
        /// </summary>
        public int AccessTokenLifetime { get; set; }
        /// <summary>
        /// 授权码生命周期
        /// </summary>
        public int AuthorizationCodeLifetime { get; set; }
        /// <summary>
        /// 刷新令牌的最长生命周期
        /// </summary>
        public int AbsoluteRefreshTokenLifetime { get; set; }
        /// <summary>
        /// 以秒为单位滑动刷新令牌的生命周期
        /// </summary>
        public int SlidingRefreshTokenLifetime { get; set; }
        /// <summary>
        /// ReUse 刷新令牌时，刷新令牌句柄将保持不变
        /// OneTime刷新令牌时将更新刷新令牌句柄。这是默认值。
        /// </summary>
        public int RefreshTokenUsage { get; set; }
        /// <summary>
        /// 获取或设置一个值，该值指示是否应在刷新令牌请求上更新访问令牌（及其声明）
        /// </summary>
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }
        /// <summary>
        /// Absolute 刷新令牌将在固定时间点到期（由AbsoluteRefreshTokenLifetime指定）
        /// Sliding刷新令牌时，将刷新刷新令牌的生命周期（按SlidingRefreshTokenLifetime中指定的数量）。生命周期不会超过AbsoluteRefreshTokenLifetime。
        /// </summary>
        public int RefreshTokenExpiration { get; set; }

        /// <summary>
        /// 指定访问令牌是引用令牌还是自包含JWT令牌（默认为Jwt）。
        /// </summary>
        public int AccessTokenType { get; set; }
        /// <summary>
        /// 指定JWT访问令牌是否应具有嵌入的唯一ID（通过jti声明）。
        /// </summary>
        public bool IncludeJwtId { get; set; }
        /// <summary>
        /// 如果指定，将由默认CORS策略服务实现（内存和EF）用于为JavaScript客户端构建CORS策略
        /// </summary>
        public List<string> AllowedCorsOrigins { get; set; }
        /// <summary>
        /// 客户端声明
        /// </summary>
        public List<ClientClaimDto> Claims { get; set; }
        /// <summary>
        /// 如果设置，将为每个流发送客户端声明。如果不是，仅用于客户端凭证流（默认为false）
        /// </summary>
        public bool AlwaysSendClientClaims { get; set; }
        /// <summary>
        /// 在请求id令牌和访问令牌时，如果用户声明始终将其添加到id令牌而不是请求客户端使用userinfo端点。默认值为false。
        /// </summary>
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }
        /// <summary>
        /// 如果设置，前缀客户端声明类型将以前缀为。默认为client_。目的是确保它们不会意外地与用户声明冲突。
        /// </summary>
        public string ClientClaimsPrefix { get; set; }
        /// <summary>
        /// 对于此客户端的用户，在成对的subjectId生成中使用的salt值。
        /// </summary>
        public string PairWiseSubjectSalt { get; set; }

        #endregion
        #region 同意屏幕
        /// <summary>
        /// 指定是否需要同意屏幕。默认为true。
        /// </summary>
        public bool RequireConsent { get; set; }
        /// <summary>
        /// 指定用户是否可以选择存储同意决策。默认为true。
        /// </summary>
        public bool AllowRememberConsent { get; set; }
        /// <summary>
        /// 用户同意 的生命周期
        /// </summary>
        public int? ConsentLifetime { get; set; }
        /// <summary>
        /// 客户端名称
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 有关客户端的更多信息的URI（在同意屏幕上使用）
        /// </summary>
        public string ClientUri { get; set; }
        /// <summary>
        /// URI到客户端徽标（在同意屏幕上使用）
        /// </summary>
        public string LogoUri { get; set; }
        #endregion

        #region 设备流程
        /// <summary>
        /// 指定用于客户端的用户代码的类型。否则回落到默认值
        /// </summary>
        public string UserCodeType { get; set; }

        /// <summary>
        /// 设备代码的生命周期（以秒为单位）（默认为300秒/ 5分钟）
        /// </summary>
        public int DeviceCodeLifetime { get; set; }
        #endregion


        public DateTime? Updated { get; set; }
        public DateTime? LastAccessed { get; set; }

        public int Id { get; set; }
    
        /// <summary>
        /// 协议类型
        /// </summary>
        public string ProtocolType { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        public bool NonEditable { get; set; }
    }
}
