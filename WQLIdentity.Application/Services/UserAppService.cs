using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WQLIdentity.Application.Dtos;
using WQLIdentity.Application.Dtos.Roles;
using WQLIdentity.Application.Dtos.UserManager;
using WQLIdentity.Application.Interfaces;
using WQLIdentity.Infra.Data.Entities;
using WQLIdentityServer.Infra.Dto;
using WQLIdentityServer.Infra.Extensions;

namespace WQLIdentity.Application.Services
{
    public class UserAppService : IUserAppService
    {
        protected readonly UserManager<ApplicationUser> _userManager;
        private IMapper _mapper;
        public UserAppService(IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public Pagelist<UserListDto> GetUsers(PageInputDto input)
        {
            var pageresult= _userManager.Users.PageBy(input, d => d.Id);
            var result = _mapper.Map<Pagelist<UserListDto>>(pageresult);
            return result;
        }

        public async Task<UserDetailDto> GetUser(string userId)
        {
            var pageresult = await _userManager.FindByIdAsync(userId);
            var result = _mapper.Map<UserDetailDto>(pageresult);
            return result;
        }

        public async Task<IdentityResult> Register(CreateUserDto input)
        {
            var user = _mapper.Map<ApplicationUser>(input);
            var result = await _userManager.CreateAsync(user, input.Password);
            return result;
        }
        public async Task<IdentityResult> DeleteAsync(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.DeleteAsync(user);
            return result;
        }

        public async Task<IdentityResult> UpdateAsync(UpdateUserDto userDto)
        {
            var user = await _userManager.FindByIdAsync(userDto.Id);
            var updateUser= _mapper.Map(userDto, user);
            var result = await _userManager.UpdateAsync(updateUser);
            return result;
        }
        public async Task<IdentityResult> ChangePasswordAsync(PasswordDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            user.Password = dto.NewPassword;
            var result = await _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);
            return result;
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles =await _userManager.GetRolesAsync(user);
            return roles;
        }

        public async Task<IdentityResult> AddToRoleAsync(UserRoleDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            var result =await _userManager.AddToRoleAsync(user, dto.RoleName);
            return result;
        }

        public async Task<IdentityResult> RemoveUserRoleAsync(UserRoleDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            var result = await _userManager.RemoveFromRoleAsync(user, dto.RoleName);
            return result;
        }

        public async Task<IdentityResult> CreateUserClaim(CreateUserOrRoleClaimDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.Id);
            var userclaims = await _userManager.GetClaimsAsync(user);

            if (userclaims.FirstOrDefault(d => d.Type == dto.Type && d.Value == dto.Value) != null)
            {
                throw new Exception("已存在该声明");
            }

            var result = await _userManager.AddClaimAsync(user, new Claim(dto.Type, dto.Value));
            return result;

        }

        public async Task<List<ClaimViewDto>> GetUserClaims(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var claim = await _userManager.GetClaimsAsync(user);
            return _mapper.Map<List<ClaimViewDto>>(claim);
        }

        public async Task<IdentityResult> RemoveUserClaim(CreateUserOrRoleClaimDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.Id);
            var claims = await _userManager.GetClaimsAsync(user);
            var cliamToRemove = claims.FirstOrDefault(c => c.Type == dto.Type && c.Value == dto.Value);
            var result = await _userManager.RemoveClaimAsync(user, cliamToRemove);
            return result;
        }
    }
}
