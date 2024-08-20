using ApplicationCore.Entities.UserEntites.Concrete;
using AutoMapper;
using Business.ExtensionMethods;
using Business.Managers.Interface;
using DataAccess.Services.Interface;
using DTO.Concrete.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class UserManager : IUserManager
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        public UserManager(IUserService userService, IMapper mapper, IPasswordHasher<AppUser> passwordHasher)
        {
            _userService = userService;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> AddUserToRole(Guid id, string roleName)
        {
            var user = await _userService.FindUser(id);
            var result = await _userService.AddUserToRole(user, roleName);
            return result.Succeeded ? true : false;
        }

        public async Task<bool> AddUserToRole(string email, string roleName)
        {
            var user = await _userService.FindUser(email);
            var result = await _userService.AddUserToRole(user, roleName);
            return result.Succeeded ? true : false;
        }

        public async Task<bool> RemoveUserFromRole(GetUserForRoleDTO dto, string roleName)
        {
            var user = await _userService.FindUser(dto.Id);
            var result = await _userService.RemoveUserFromRole(user, roleName);
            return result.Succeeded ? true : false;
        }

        public async Task<T> FindUser<T>(Guid id)
             => _mapper.Map<T>(await _userService.FindUser(id));
        public async Task<T> FindUser<T>(string email)
             => _mapper.Map<T>(await _userService.FindUser(email));

        public async Task<EditUserDTO> FindUser(ClaimsPrincipal claimsPrincipal)
            => _mapper.Map<EditUserDTO>(await _userService.FindUser(claimsPrincipal));

        public async Task<Guid> GetUserId(ClaimsPrincipal claimsPrincipal)
            => await _userService.GetUserId(claimsPrincipal);

        public async Task<List<GetUserForRoleDTO>> GetUsers()
            => _mapper.Map<List<GetUserForRoleDTO>>(await _userService.GetUsers());

        public async Task<List<GetUserForRoleDTO>> GetUsersHasNotRole(string roleName)
            => _mapper.Map<List<GetUserForRoleDTO>>(await _userService.GetUsersHasNotRole(roleName));

        public async Task<List<GetUserForRoleDTO>> GetUsersHasRole(string roleName)
            => _mapper.Map<List<GetUserForRoleDTO>>(await _userService.GetUsersHasRole(roleName));

        public async Task<bool> IsUserInRole(GetUserForRoleDTO dto, string roleName)
            => await _userService.IsUserInRole(await _userService.FindUser(dto.Id), roleName);

        public async Task<bool> IsUserInRole(GetUserDTO dto, string roleName)
            => await _userService.IsUserInRole(await _userService.FindUser(dto.Id), roleName);

        public async Task<bool> IsUserInRole(string userName, string roleName)
       => await _userService.IsUserInRole(await _userService.FindUserByUsername(userName), roleName);

        public async Task<bool> Login(LoginDTO dto)
        {
            var result = await _userService.Login(dto.UserName, dto.Password);
            return result.Succeeded ? true : false;
        }

        public async Task Login(string email)
            => await _userService.Login(email);

        public async Task Logout()
            => await _userService.Logout();

        public async Task<string> GenerateTokenForResetPassword(Guid userId)
        {
            var user = await _userService.FindUser(userId);
            return await _userService.GenerateTokenForResetPassword(user);
        }

        public async Task<bool> ResetPassword(ResetPasswordDTO dto)
        {
            var user = await _userService.FindUser(dto.Email);
            var result = await _userService.ResetPassword(user, dto.Token, dto.Password);
            return result.Succeeded ? true : false;
        }

        public async Task<bool> TokenIsValid(GetUserDTO dto, string token)
        {
            var user = await _userService.FindUser(dto.Email);
            return await _userService.TokenIsValid(user, token);
        }

        public async Task<ExternalLoginInfo> GetExternalLoginInfo()
            => await _userService.GetExternalLoginInfo();

        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string? provider, string? redirectUrl)
            => _userService.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

        public async Task<bool> UpdateUser(EditUserDTO dto)
        {
            var user = _mapper.Map<AppUser>(dto);
            var result = await _userService.UpdateUser(user);
            return result.Succeeded ? true : false;
        }

        public async Task<bool> CreateUser(CreateUserDTO dto)
        {
            var user = _mapper.Map<AppUser>(dto);
            user.PasswordHash = _passwordHasher.HashPassword(user, "1234");
            user.UserName = CreateUserName(dto.FirstName, dto.LastName);
            
            var result = await _userService.CreateUser(user);
            
            return result.Succeeded ? true : false;
        }

        private string CreateUserName(string firstName, string lastName)
        {
            var changeFirstName = firstName.ChangeCharacters();
            var changeLastName = lastName.ChangeCharacters();

            var userNamePartI = changeFirstName.Trim().Contains(" ") ?
               (changeFirstName.Split(' ')[0] + changeFirstName.Split(' ')[1]) : changeFirstName;
            
            var userNamePartII = changeLastName.Trim().Contains(" ") ? (changeLastName.Split(' ')[0] + changeLastName.Split(' ')[1]) : changeLastName;
            
            var userName = userNamePartI + "." + userNamePartII;
            return userName;
        }

        public async Task<bool> ChangePassword(CreatePasswordDTO dto)
        {
            var user = await _userService.FindUser(dto.Email);
            var result = await _userService.ChangePassword(user, dto.OldPassword, dto.NewPassword);
            return result.Succeeded ? true : false;
        }

        public async Task<bool> ChangePassword(ChangePasswordDTO dto)
        {
            var user = await _userService.FindUser(dto.Id);
            var result = await _userService.ChangePassword(user, dto.OldPassword, dto.NewPassword);
            return result.Succeeded ? true : false;
        }
    }
}
