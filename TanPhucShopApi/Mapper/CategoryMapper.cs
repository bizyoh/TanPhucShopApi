using AutoMapper;
using TanPhucShopApi.Models;
using TanPhucShopApi.Models.DTO.Category;

namespace TanPhucShopApi.Mapper
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<CreateCategoryDto,Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<Category, CreatedCategoryDto>();
            CreateMap<Category, DetailCategoryDto>();
        }
    }
}
