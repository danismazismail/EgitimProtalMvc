using ApplicationCore.Entities.UserEntites.Concrete;
using DTO.Concrete.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers.Interface
{
    public interface IUserManager
    {
        Task<List<GetUserForRoleDTO>> GetUsers();

        Task<T> FindUser<T>(Guid id);
        Task<T> FindUser<T>(string email);
        Task<EditUserDTO> FindUser(ClaimsPrincipal claimsPrincipal);
        Task<Guid> GetUserId(ClaimsPrincipal claimsPrincipal);

        Task<List<GetUserForRoleDTO>> GetUsersHasRole(string roleName);

        Task<List<GetUserForRoleDTO>> GetUsersHasNotRole(string roleName);

        Task<bool> AddUserToRole(string email, string roleName);
        Task<bool> AddUserToRole(Guid id, string roleName);
        
        Task<bool> RemoveUserFromRole(GetUserForRoleDTO dto, string roleName);
        Task<bool> IsUserInRole(GetUserForRoleDTO dto, string roleName);
        Task<bool> IsUserInRole(GetUserDTO dto, string roleName);
        Task<bool> IsUserInRole(string email, string roleName);
        Task<bool> Login(LoginDTO dto);
        Task Login(string email);
        Task Logout();

        Task<string> GenerateTokenForResetPassword(Guid userId);
        Task<bool> ResetPassword(ResetPasswordDTO dto);
        Task<bool> TokenIsValid(GetUserDTO dto, string token);
        Task<ExternalLoginInfo> GetExternalLoginInfo();
        AuthenticationProperties ConfigureExternalAuthenticationProperties(string? provider, string? redirectUrl);

        Task<bool> UpdateUser(EditUserDTO dto);
        Task<bool> CreateUser(CreateUserDTO dto);
        Task<bool> ChangePassword(CreatePasswordDTO dto);
        Task<bool> ChangePassword(ChangePasswordDTO dto);
    }
}
