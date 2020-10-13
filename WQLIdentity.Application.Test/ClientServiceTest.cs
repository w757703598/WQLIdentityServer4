using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WQLIdentity.Application.Dtos.Clients;
using WQLIdentityServer.Infra.Dto;

namespace WQLIdentity.Application.Test
{
    public class ClientServiceTest : BaseUnitTest
    {


        public ClientServiceTest()
        {
            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ClientMapping());
                cfg.CreateMap(typeof(Pagelist<>), typeof(Pagelist<>));

            });
        }
    }
}
