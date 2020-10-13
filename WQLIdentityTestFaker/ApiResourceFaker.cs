using Bogus;
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WQLIdentityTestFaker
{
    public class ApiResourceFaker
    {
        public static Faker<ApiResource> GenerateApiResource(string name = null, bool addApiSecrets = true, bool addUserClaims = true)
        {
            return new Faker<ApiResource>()
                .RuleFor(a => a.Secrets, f => addApiSecrets ? GenerateSecret().Generate(f.Random.Int(0, 2)) : new List<ApiResourceSecret>())
                .RuleFor(a => a.Enabled, f => f.Random.Bool())
                .RuleFor(a => a.Name, f => name ?? f.Internet.DomainName())
                .RuleFor(a => a.DisplayName, f => f.Lorem.Word())
                .RuleFor(a => a.Scopes, f => GenerateScope().Generate(f.Random.Int(0, 2)))
                .RuleFor(a => a.Description, f => f.Lorem.Word())
                .RuleFor(a => a.UserClaims,f=> GenerateUserCliam().Generate(f.Random.Int(0,3)));
        }

        public static Faker<ApiResourceSecret> GenerateSecret()
        {
            return new Faker<ApiResourceSecret>()
                .RuleFor(s => s.Description, f => f.Lorem.Word())
                .RuleFor(s => s.Value, f => f.Lorem.Word())
                .RuleFor(s => s.Type, f => f.PickRandom(IdentityHelpers.SecretTypes));
        }
        public static Faker<ApiResourceScope> GenerateScope()
        {
            return new Faker<ApiResourceScope>()
                .RuleFor(s => s.Scope, f => f.Lorem.Word());

        }
        public static Faker<ApiResourceClaim> GenerateUserCliam()
        {
            return new Faker<ApiResourceClaim>()
                .RuleFor(s => s.Type, f => f.Lorem.Word());

        }
    }
}
