using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WQLIdentity.Application.Dtos.Roles;
using WQLIdentityServer.Infra.Dto;

namespace WQLIdentity.Application.Test
{
    public class RoleServiceTest:BaseUnitTest
    {
        public RoleServiceTest()
        {
            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RoleMapping());
                cfg.CreateMap(typeof(Pagelist<>), typeof(Pagelist<>));

            });
        }
       
    }
}
