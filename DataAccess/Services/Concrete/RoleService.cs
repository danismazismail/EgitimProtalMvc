using ApplicationCore.Consts;
using ApplicationCore.Entities.UserEntites.Concrete;
using DataAccess.Context.IdentityContext;
using DataAccess.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services.Concrete
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly AppIdentityDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public RoleService(RoleManager<AppRole> roleManager, AppIdentityDbContext context, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddRoleAsync(AppRole role)
            => await _roleManager.CreateAsync(role);

        public async Task<IdentityResult> UpdateRoleAsync(AppRole role)
        {
            var entity = await _roleManager.FindByIdAsync(role.Id.ToString());
            entity.UpdatedDate = DateTime.Now;
            entity.Status = Status.Modified;
            entity.Name = role.Name;
            return await _roleManager.UpdateAsync(entity);
        }

        public async Task<IdentityResult> DeleteRoleAsync(Guid roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            role.DeletedDate = DateTime.Now;
            role.Status = Status.Passive;
            return await _roleManager.UpdateAsync(role);
        }

        public async Task<AppRole> FindRoleAsync(Guid roleId)
            => await _roleManager.FindByIdAsync(roleId.ToString());

        public async Task<List<AppRole>> GetRolesAsync()
            => await _roleManager.Roles.Where(x => x.Status != Status.Passive).ToListAsync();

        public async Task<bool> CheckRoleNameAsync(string roleName, Guid? roleId)
        {
            if (roleId != null)
                return await _roleManager.Roles.AnyAsync(x => x.Name == roleName && x.Id != roleId);

            return await _roleManager.Roles.AnyAsync(x => x.Name == roleName);
        }


    }
}
