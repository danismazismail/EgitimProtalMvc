using DTO.Concrete.Roles;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers.Interface
{
    public interface IRoleManager
    {
        Task<List<GetRoleDTO>> GetRolesAsync();
        Task<T> FindRoleAsync<T>(Guid roleId);
        Task<bool> CheckRoleNameAsync(string roleName, Guid? roleId);
        Task<bool> AddRoleAsync(CreateRoleDTO dto);
        Task<bool> UpdateRoleAsync(UpdateRoleDTO role);
        Task<bool> DeleteRoleAsync(Guid roleId);
        //Task<List<GetUserDTO>> GetUserWhoInRole(string roleName);
    }
}
