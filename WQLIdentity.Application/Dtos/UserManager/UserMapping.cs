using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WQLIdentity.Infra.Data.Entities;

namespace WQLIdentity.Application.Dtos.UserManager
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<CreateUserDto, ApplicationUser>(MemberList.None);
            CreateMap<UpdateUserDto, ApplicationUser>(MemberList.None);

        }
    }
}
