using AutoMapper;
using TanPhucShopApi.Models;
using TanPhucShopApi.Models.DTO.UserDto;

namespace TanPhucShopApi.Mapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<RegisterUserDto, User>();
            CreateMap<User, CreatedUserDto>();
            CreateMap<User, DetailUserDto>();
            CreateMap<User, AccessedUserDto>();

        }
    }
}
