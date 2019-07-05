using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WQLIdentity.Domain.Entities;

namespace WQLIdentity.Application.Dtos.Claims
{
    public class ClaimsMapping:Profile
    {
        public ClaimsMapping()
        {
            CreateMap<CreateClaimDto, WQLIdentity.Domain.Entities.Claims>(MemberList.Source);
        }
    }
}
