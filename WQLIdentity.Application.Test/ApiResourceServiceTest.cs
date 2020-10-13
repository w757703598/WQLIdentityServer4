using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using WQLIdentity.Application.Dtos.ApiResources;
using WQLIdentity.Application.Interfaces;
using WQLIdentity.Application.Services;
using WQLIdentityServer.Infra.Dto;
using WQLIdentityTestFaker;
using Xunit;

namespace WQLIdentity.Application.Test
{
    public class ApiResourceServiceTest:BaseUnitTest
    {
        private IApiResourceService service;



        public ApiResourceServiceTest()
        {
            MockApiresourceRepo.Setup(d => d.GetAll()).Returns(ApiResourceFaker.GenerateApiResource().Generate(5).AsQueryable());

            //remove
            MockApiresourceRepo.Setup(s => s.GetByIdAsync(It.Is<int>(d => d == 999))).ReturnsAsync(ApiResourceFaker.GenerateApiResource().Generate());
            MockApiresourceRepo.Setup(s => s.Remove(It.IsAny<ApiResource>()));

            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApiResourceMapping());
                cfg.CreateMap(typeof(Pagelist<>), typeof(Pagelist<>));

            });

            service = new ApiResourceService(MockApiresourceRepo.Object, MockApiScopeRepo.Object, MockApiresourceSecretRepo.Object, MockApiresourcePropertyRepo.Object, Mapper.CreateMapper());
        }
        [Fact]
        public void Get_ApiResource_MapTo_ApiResourceList()
        {
            var list= service.GetApiResources(PageInput);

            Assert.Equal(5, list.TotalCount);
        }




        [Fact]
        public async void Should_Remove_ApiResource()
        {
           
            var result= await service.Remove(999);

            MockApiresourceRepo.Setup(s => s.SaveChangesAsync()).ReturnsAsync(1);

            MockApiresourceRepo.Verify(d => d.GetByIdAsync(999), Times.Once);
            MockApiresourceRepo.Verify(s => s.Remove(It.IsAny<ApiResource>()), Times.Once);

        }

    }
}
