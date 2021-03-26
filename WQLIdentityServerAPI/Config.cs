﻿using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace WQLIdentityServerAPI
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(){
                    UserClaims={"role","username"}
                },
            };
        }
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
                {
                    new ApiScope(name: "IdentityServer.API",   displayName: "IdentityServer Manager"),

                };
        }
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("IdentityServer", "IdentityServerAPI",new List<string>{ JwtClaimTypes.Role })
                {
                    Scopes={ "IdentityServer.API" }

                }

            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "IdentityServer.API" }
                },

                // resource owner password grant client
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "IdentityServer.API" }
                },

                // OpenID Connect hybrid flow and client credentials client (MVC)
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "IdentityServer.API"
                    },
                    AllowOfflineAccess = true
                },

                // JavaScript Client
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { "http://localhost:5003/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:5003/index.html" },
                    AllowedCorsOrigins = { "http://localhost:5003" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "IdentityServer.API"
                    },
                },
                //管理前端默认客户端
                new Client
                {
                    ClientId = "IdentityServer4",
                    ClientName = "base JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    Description="IdentityServer4管理客户端",
                    RedirectUris = { 
                        "http://localhost:8082/oidc-callback",
                        "http://localhost:8082/silent-renew.html",

                         "http://localhost:5001/doc/oauth2-redirect.html", //swagger oauth2

                        "http://10.53.28.168:8082/oidc-callback", 
                        "http://10.53.28.168:8082/silent-renew.html" },
                    PostLogoutRedirectUris = { "http://localhost:8082" },
                    AllowOfflineAccess=true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "IdentityServer.API"

                    }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "Alice"),
                        new Claim("website", "https://alice.com")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "Bob"),
                        new Claim("website", "https://bob.com")
                    }
                }
            };
        }
    }
}
