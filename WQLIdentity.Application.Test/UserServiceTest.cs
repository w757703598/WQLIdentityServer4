using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WQLIdentity.Application.Dtos.UserManager;
using WQLIdentityServer.Infra.Dto;

namespace WQLIdentity.Application.Test
{
    public class UserServiceTest:BaseUnitTest
    {
        public UserServiceTest()
        {
            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserMapping());
                cfg.CreateMap(typeof(Pagelist<>), typeof(Pagelist<>));

            });
        }
    }
}
