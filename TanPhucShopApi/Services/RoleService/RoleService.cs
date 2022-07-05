using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TanPhucShopApi.Models;
using TanPhucShopApi.Models.DTO.RoleDto;

namespace TanPhucShopApi.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private RoleManager<Role> roleManager;
        private IMapper mapper;
        public RoleService(RoleManager<Role> _roleManager, IMapper _mapper)
        {
            roleManager = _roleManager;
            mapper = _mapper;
        }

        public async Task<bool> Create(CreateRoleDto roleDto)
        {
            var role = mapper.Map<Role>(roleDto);
            if(await roleManager.RoleExistsAsync(role.Name)) return false;
            var result = await roleManager.CreateAsync(role);
            if (result.Succeeded) return true;
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var role = await GetRoleById(id);
            if (role != null)
            {
                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded) return true;
            }
            return false;
        }

        public List<GetAllRoleDto> GetAllRoles()
        {
            return roleManager.Roles.Select(p => new GetAllRoleDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
            }).ToList();
        }

        public async Task<Role> GetRoleById(int id)
        {
            var role = await roleManager.FindByIdAsync(id.ToString());
            if (role != null) return role;
            return null;
        }

        public async Task<Role> GetRoleByName(string roleName)
        {
            var role = await roleManager.FindByIdAsync(roleName);
            if (role != null) return role;
            return null;
        }

        public async Task<bool> Update(int id, UpdateRoleDto updateRoleDto)
        {
            var currentRole = await GetRoleById(id);
            if(currentRole is not null)
            {
                mapper.Map(updateRoleDto,currentRole);
                IdentityResult result =await roleManager.UpdateAsync(currentRole);
                if (result.Succeeded) return true;
            }
            return false;
        }
    }
}
