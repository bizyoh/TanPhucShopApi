
using TanPhucShopApi.Models.DTO.RoleDto;

namespace TanPhucShopApi.Models.DTO.UserDto
{
    public class GetUserRoleDto
    {
        public int Id { get; set; }
        public List<DetailRoleDto> DetailRoleDtos { get; set; }
    }
}
