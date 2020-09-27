using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WQLIdentity.Application.Dtos;
using WQLIdentity.Application.Dtos.Roles;
using WQLIdentity.Application.Interfaces;
using WQLIdentity.Infra.Data.Entities;
using WQLIdentityServer.Infra.Dto;
using WQLIdentityServer.Infra.Extensions;

namespace WQLIdentity.Application.Services
{
    public class RoleAppService : IRoleAppService
    {
        protected readonly RoleManager<ApplicationRole> _roleManager;
        private IMapper _mapper;
        public RoleAppService(IMapper mapper, RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> CreateRoleAsync(CreateRoleDto roleDto)
        {
            var role = _mapper.Map<ApplicationRole>(roleDto);

            var result = await _roleManager.CreateAsync(role);
            return result;
        }

        public Pagelist<RoleListDto> GetRoles(PageInputDto input)
        {
            var roles = _roleManager.Roles.PageBy(input, r => r.Id);
            var result = _mapper.Map<Pagelist<RoleListDto>>(roles);
            return result;
        }

        public async Task<IdentityResult> UpdateRoleAsync(UpdateRoleDto roleDto)
        {
            var role = await _roleManager.FindByIdAsync(roleDto.Id);
            role.Name = roleDto.Name;
            var result = await _roleManager.UpdateAsync(role);
            return result;

        }

        public async Task<IdentityResult> DeleteRoleAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            var result = await _roleManager.DeleteAsync(role);
            return result;
        }

        public async Task<IdentityResult> CreateRoleClaim(CreateUserOrRoleClaimDto dto)
        {

            var role = await _roleManager.FindByIdAsync(dto.Id);
            var roleClaims = await _roleManager.GetClaimsAsync(role);
            if (roleClaims.FirstOrDefault(d => d.Type == dto.Type && d.Value == dto.Value) != null)
            {
                throw new Exception("已存在该声明");
            }
            var result = await _roleManager.AddClaimAsync(role, new Claim(dto.Type, dto.Value));
            return result;
        }

        public async Task<IdentityResult> RemoveRoleClaim(CreateUserOrRoleClaimDto dto)
        {
            var role = await _roleManager.FindByIdAsync(dto.Id);
            var claims = await _roleManager.GetClaimsAsync(role);
            var cliamToRemove = claims.FirstOrDefault(c => c.Type == dto.Type && c.Value == dto.Value);
            var result = await _roleManager.RemoveClaimAsync(role, cliamToRemove);
            return result;
        }

        public async Task<List<ClaimViewDto>> GetRoleClaims(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            var claim = await _roleManager.GetClaimsAsync(role);
            return _mapper.Map<List<ClaimViewDto>>(claim);

        }
    }
}
