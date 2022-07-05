using AutoMapper;
using TanPhucShopApi.Models;
using TanPhucShopApi.Models.DTO.RoleDto;

namespace TanPhucShopApi.Mapper
{
    public class RoleMapper : Profile
    {
        public RoleMapper()
        {
            CreateMap<CreateRoleDto, Role>();
            CreateMap<UpdateRoleDto, Role>();
        }
    }
}
