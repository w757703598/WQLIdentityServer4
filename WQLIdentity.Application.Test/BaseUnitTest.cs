using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WQLIdentity.Domain.Interface;
using WQLIdentityServer.Infra.Dto;
using Xunit;

namespace WQLIdentity.Application.Test
{
    public class BaseUnitTest
    {
        protected Mock<IConfigurationRepository<ApiResource>> MockApiresourceRepo;
        protected Mock<IConfigurationRepository<ApiScope>> MockApiScopeRepo;
        protected Mock<IConfigurationRepository<ApiResourceSecret>> MockApiresourceSecretRepo;
        protected Mock<IConfigurationRepository<ApiResourceProperty>> MockApiresourcePropertyRepo;

        protected PageInputDto  PageInput;


        protected virtual MapperConfiguration Mapper { get; set; }
        public BaseUnitTest()
        {
            MockApiresourceRepo = new Mock<IConfigurationRepository<ApiResource>>();
            MockApiScopeRepo = new Mock<IConfigurationRepository<ApiScope>>();
            MockApiresourceSecretRepo = new Mock<IConfigurationRepository<ApiResourceSecret>>();
            MockApiresourcePropertyRepo = new Mock<IConfigurationRepository<ApiResourceProperty>>();

            PageInput = new PageInputDto()
            {
                Search = "",
                PageSize = 10,
                Page = 0,
            };


            Mapper = new MapperConfiguration(cfg=> { });
        }

        [Fact]
        public void Should_Valid_AtuoMap_Configuration()
        {
            Mapper.AssertConfigurationIsValid();
        }


    }
}
