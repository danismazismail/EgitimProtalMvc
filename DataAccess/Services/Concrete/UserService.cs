using ApplicationCore.Consts;
using ApplicationCore.Entities.UserEntites.Concrete;
using DataAccess.Context.ApplicationContext;
using DataAccess.Context.IdentityContext;
using DataAccess.Services.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly AppIdentityDbContext _context;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailSender emailSender, AppIdentityDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _context = context;
        }
        public async Task<List<AppUser>> GetUsers()
            => await _userManager.Users.ToListAsync();

        public async Task<List<AppUser>> GetUsersHasRole(string roleName)
        {
            var users = await _userManager.Users.ToListAsync();
            var usersWhoInRole = new List<AppUser>();
            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, roleName))
                {
                    usersWhoInRole.Add(user);
                }
            }

            return usersWhoInRole;

        }

        public async Task<List<AppUser>> GetUsersHasNotRole(string roleName)
        {
            var users = await _userManager.Users.ToListAsync();
            var usersWhoNotInRole = new List<AppUser>();
            foreach (var user in users)
            {
                if (!await _userManager.IsInRoleAsync(user, roleName))
                {
                    usersWhoNotInRole.Add(user);
                }
            }

            return usersWhoNotInRole;
        }

        public async Task<AppUser> FindUser(Guid id)
            => await _userManager.FindByIdAsync(id.ToString());

        public async Task<AppUser> FindUser(string email)
            => await _userManager.FindByEmailAsync(email);

        public async Task<AppUser> FindUserByUsername(string email)
            => await _userManager.FindByNameAsync(email);

        public async Task<AppUser> FindUser(ClaimsPrincipal claims)
             => await _userManager.GetUserAsync(claims);

        public async Task<Guid> GetUserId(ClaimsPrincipal claims)
        {
            var user = await _userManager.GetUserAsync(claims);
            return user.Id;
        } 

        public async Task<IdentityResult> AddUserToRole(AppUser appUser, string roleName)
        {
            // Mevcut kullanıcıyı kontrol edin ve takipten çıkarın
            var trackedUser = _context.Users.Local.FirstOrDefault(u => u.Id == appUser.Id);
            if (trackedUser != null)
            {
                _context.Entry(trackedUser).State = EntityState.Detached;
            }
            return await _userManager.AddToRoleAsync(appUser, roleName);
        }

        public async Task<IdentityResult> RemoveUserFromRole(AppUser appUser, string roleName)
            => await _userManager.RemoveFromRoleAsync(appUser, roleName);

        public async Task<bool> IsUserInRole(AppUser appUser, string roleName)
            => await _userManager.IsInRoleAsync(appUser, roleName);

        public async Task<SignInResult> Login(string userName, string password)
            => await _signInManager.PasswordSignInAsync(userName, password, false, false);

        public async Task Login(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            await _signInManager.SignInAsync(user, isPersistent: false);
        }

        public async Task Logout()
            => await _signInManager.SignOutAsync();

        public async Task<string> GenerateTokenForResetPassword(AppUser appUser)
            => await _userManager.GeneratePasswordResetTokenAsync(appUser);

        public async Task SendEmail(string email, string subject, string message)
            => await _emailSender.SendEmailAsync(email, subject, message);

        public async Task<IdentityResult> ResetPassword(AppUser user, string token, string password)
            => await _userManager.ResetPasswordAsync(user, token, password);

        public async Task<bool> TokenIsValid(AppUser user, string token)
             => await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token);

        public async Task<ExternalLoginInfo> GetExternalLoginInfo()
            => await _signInManager.GetExternalLoginInfoAsync();

        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string? provider, string? redirectUrl)
            => _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

        public async Task<IdentityResult> UpdateUser(AppUser user)
        {
            var entity = await _userManager.FindByIdAsync(user.Id.ToString());
            entity.Email = user.Email;
            entity.UpdatedDate = DateTime.Now;
            entity.Status = Status.Modified;

            return await _userManager.UpdateAsync(entity);
        }

        public async Task<IdentityResult> CreateUser(AppUser appUser)
            => await _userManager.CreateAsync(appUser);

        public async Task<IdentityResult> ChangePassword(AppUser appUser, string oldPassword, string newPassword)
            => await _userManager.ChangePasswordAsync(appUser, oldPassword, newPassword);
    }
}
