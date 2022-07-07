using TanPhucShopApi.Models;
using TanPhucShopApi.Models.DTO.Category;

namespace TanPhucShopApi.Services.CategoryService
{
    public interface ICategoryService
    {
        public List<GetAllCategoryDto> GetAllCategory();
        public List<CategoryDto> GetAllCategoryDtoByStatus(bool status);
        public List<GetAllCategoryDto> GetAllCategoryByStatus(bool status);
        public CreatedCategoryDto Create(CreateCategoryDto createCategoryDto);
        public bool Update(int id,UpdateCategoryDto updateCategoryDto);
        public bool Delete(int id);
        public Category GetById(int id);
    }
}
