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
            CreateMap<User, AdminUpdateUserDto>();
            CreateMap<AdminUpdateUserDto, User>();
           
            // CreateMap<PostAdminUpdateUserDto, User>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PostAdminUpdateUserDto, User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
