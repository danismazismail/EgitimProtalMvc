using ApplicationCore.Entities.UserEntites.Concrete;
using AutoMapper;
using Business.Managers.Interface;
using DataAccess.Services.Concrete;
using DataAccess.Services.Interface;
using DTO.Concrete.Roles;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class RoleManager : IRoleManager
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RoleManager(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        public async Task<bool> AddRoleAsync(CreateRoleDTO dto)
        {
            var result = await _roleService.AddRoleAsync(_mapper.Map<AppRole>(dto));
            return result.Succeeded ? true : false;
        }

        public async Task<bool> UpdateRoleAsync(UpdateRoleDTO role)
        {
            var result = await _roleService.UpdateRoleAsync(_mapper.Map<AppRole>(role));
            return result.Succeeded ? true : false;
        }

        public async Task<bool> DeleteRoleAsync(Guid roleId)
        {
            var result = await _roleService.DeleteRoleAsync(roleId);
            return result.Succeeded ? true : false;
        }

        public async Task<bool> CheckRoleNameAsync(string roleName, Guid? roleId)
        {
            if (roleId != null)
            {
                return await _roleService.CheckRoleNameAsync(roleName, roleId);
            }
            return await _roleService.CheckRoleNameAsync(roleName, null);
        }

        public async Task<T> FindRoleAsync<T>(Guid roleId)
            => _mapper.Map<T>(await _roleService.FindRoleAsync(roleId));

        public async Task<List<GetRoleDTO>> GetRolesAsync()
            => _mapper.Map<List<GetRoleDTO>>(await _roleService.GetRolesAsync());

    }
}
