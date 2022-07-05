using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TanPhucShopApi.Models.DTO.Category;
using TanPhucShopApi.Services.CategoryService;

namespace TanPhucShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private IInvoiceService categoryService;
        private IConfiguration configuration;
        private string BASE_URL;
        public CategoriesController(IInvoiceService _categoryService, IConfiguration _configuration)
        {
            categoryService = _categoryService;
            configuration = _configuration;
        }
        [HttpGet]
        public IActionResult GetAllCategory()
        {
            var categories = categoryService.GetAllCategory();
            return Ok(categories);
        }

        [HttpGet("status")]
        public IActionResult GetAllCategoryByStatus(bool status)
        {
            var categories = categoryService.GetAllCategoryByStatus(status);
            return Ok(categories);
        }


        [HttpPost]
        public IActionResult Create(CreateCategoryDto createCategoryDto)
        {
            if (createCategoryDto != null)
            {
                var createdCategory = categoryService.Create(createCategoryDto);
                if (createdCategory != null) return Created(BASE_URL + "/category/"+createdCategory.Id, createdCategory);
            }
            return BadRequest();
        }

        [HttpPut("id")]
        public IActionResult Update(int id, UpdateCategoryDto updateCategoryDto)
        {
            if (updateCategoryDto != null)
            {
                var result = categoryService.Update(id, updateCategoryDto);
                if (result) return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            var category = categoryService.GetById(id);
            if (category == null) return NotFound();
            else
            {
                var result = categoryService.Delete(id);
                if (result) return Ok();
            }
            return BadRequest();
        }
    }
}
