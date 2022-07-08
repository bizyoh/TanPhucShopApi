using System.Security.Claims;
using TanPhucShopApi.DTO;
using TanPhucShopApi.Models;
using TanPhucShopApi.Models.DTO.UserDto;

namespace TanPhucShopApi.Services.UserService
{
    public interface IUserService
    {
        public Task<CreatedUserDto> Create(RegisterUserDto registerUserDto);
        public Task<bool> Delete(int id);
        public Task<bool> ChangeStatusUser(int id);
        public Task<bool> UpdateByUser(int id, UpdateUserDto userUpdateDto);
        public Task<bool> UpdateByAdmin(int id, AdminUpdateUserDto adminUpdateUserDto);
        public Task<User> FindUserById(int id);
        public List<GetAllUserDto>GetAll();
        public Task<bool> AddRoleUser(int id,IList<string> roles);
        public Task<bool> RemoveRoleUser(UserRoleDto userRoleDTO);
        public Task<DetailUserDto> FindDetailUserDtoById(int id);
        public Task<AccessToken> CreateAccessToken(User user);
        public Task<AccessedUserDto> Login(LoginUserDto loginUserDto);
    }
}
