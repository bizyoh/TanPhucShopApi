using TanPhucShopApi.Models;
using TanPhucShopApi.Models.DTO.Product;

namespace TanPhucShopApi.Services.ProductService
{
    public interface IProductService
    {
        public List<GetAllProductDto> GetAllProducts();
        public Product GetById(int id);
        public bool HardDelete(int id);
        public bool ChangeProductsStatus(List<Product> products);
        public CreatedProductDto Create(CreateProductDto createProductDto);
        public bool Update(int id, UpdateProductDto updateProductDto);
    }
}
