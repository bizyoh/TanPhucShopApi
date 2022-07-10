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
            CreateMap<PostAdminUpdateUserDto, User>().ForMember(x => x.Address, y => y.MapFrom(y => y.Address))
                .ForMember(x => x.FirstName, y => y.MapFrom(y => y.FirstName)).ForMember(x => x.LastName, y => y.MapFrom(y => y.LastName))
                .ForMember(x => x.Email, y => y.MapFrom(y => y.Email)).ForMember(x => x.Status, y => y.MapFrom(y => y.Status))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
