using ApplicationCore.Entities.UserEntites.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services.Interface
{
    public interface IUserService
    {
        Task<List<AppUser>> GetUsers();

        Task<List<AppUser>> GetUsersHasRole(string roleName);

        Task<List<AppUser>> GetUsersHasNotRole(string roleName);

        Task<AppUser> FindUser(Guid id);
        Task<AppUser> FindUser(string email);
        Task<AppUser> FindUserByUsername(string userName);
        Task<AppUser> FindUser(ClaimsPrincipal claims);
        Task<Guid> GetUserId(ClaimsPrincipal claims);

        Task<IdentityResult> AddUserToRole(AppUser appUser, string roleName);
        Task<IdentityResult> RemoveUserFromRole(AppUser appUser, string roleName);
        Task<bool> IsUserInRole(AppUser appUser, string roleName);
        
        Task<SignInResult> Login(string userName, string password);
        Task Login(string email);
        
        Task Logout();
        Task<IdentityResult> ResetPassword(AppUser user, string token, string password);
        
        Task<string> GenerateTokenForResetPassword(AppUser appUser);
        
        Task SendEmail(string email, string subject, string message);
        
        
        Task<bool> TokenIsValid(AppUser user, string token);
        
        Task<ExternalLoginInfo> GetExternalLoginInfo();
        
        AuthenticationProperties ConfigureExternalAuthenticationProperties(string? provider, string? redirectUrl);

        Task<IdentityResult> UpdateUser(AppUser user);
        Task<IdentityResult> CreateUser(AppUser appUser);

        Task<IdentityResult> ChangePassword(AppUser appUser, string oldPassword, string newPassword);
    }
}
