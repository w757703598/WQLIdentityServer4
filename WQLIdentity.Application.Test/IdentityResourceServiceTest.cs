using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WQLIdentity.Application.Dtos.IdentityResources;
using WQLIdentityServer.Infra.Dto;

namespace WQLIdentity.Application.Test
{
    public class IdentityResourceServiceTest:BaseUnitTest
    {
        public IdentityResourceServiceTest()
        {
            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new IdentityResourceMapping());
                cfg.CreateMap(typeof(Pagelist<>), typeof(Pagelist<>));

            });
        }
    }
}
