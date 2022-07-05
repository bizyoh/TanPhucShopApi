﻿using TanPhucShopApi.Models;
using TanPhucShopApi.Models.DTO.RoleDto;

namespace TanPhucShopApi.Services.RoleService
{
    public interface IRoleService
    {
        public List<GetAllRoleDto> GetAllRoles();
        public Task<Role> GetRoleById(int id);
        public Task<Role> GetRoleByName(string roleName);
        public Task<bool> Delete(int id);
        public Task<bool> Update(int id, UpdateRoleDto UpdateRoleDto);
        public Task<bool> Create(CreateRoleDto roleDto);
    }
}
