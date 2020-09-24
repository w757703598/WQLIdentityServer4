using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace WQLIdentity.Domain.Interface
{
    public interface IScopeRepository: IConfigurationRepository<ApiScope>
    {
    }
}
