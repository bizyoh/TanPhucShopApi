using AutoMapper;
using TanPhucShopApi.Data;
using TanPhucShopApi.Models;
using TanPhucShopApi.Models.DTO.Product;
using TanPhucShopApi.Services.ProductService;

namespace TanPhucShopApi.Services.ProductService
{
    public class ProductService : IProductService
    {
        private AppDBContext db;
        private IMapper mapper;
        public ProductService(AppDBContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }
        public CreatedProductDto Create(CreateProductDto createProductDto)
        {
            var product = mapper.Map<Product>(createProductDto);
            product.Created = DateTime.Now;
            db.Products.Add(product);
            if(db.SaveChanges() > 0){
                CreatedProductDto createdProduct = mapper.Map<CreatedProductDto>(product);
                return createdProduct;
            }
            return null;
        }

        public bool HardDelete(int id)
        {
            var product = db.Products.Find(id);
            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public List<GetAllProductDto> GetAllProducts()
        {
            return db.Products.Select(x=>new GetAllProductDto {
                  Id = x.Id,
                  Name = x.Name,
                  Status = x.Status,
                  CategoryId = x.CategoryId,
                  CategoryName = x.Category.Name,
                  Description = x.Description,
                  Created = x.Created,
                  Price = x.Price , 
                  Photo = x.Photo,
                  Quantity = x.Quantity
            }).ToList();
        }

        public List<Product> GetAllProductsByStatus(bool status)
        {
            return db.Products.Where(x => x.Status == status).ToList();
        }

        public Product GetById(int id)
        {
            return db.Products.Find(id);
        }

        public bool Update(int id, UpdateProductDto updateProductDto)
        {
            var currentProduct = db.Products.Find(id);
            if (currentProduct != null)
            {
                mapper.Map(updateProductDto, currentProduct);
                db.Update(currentProduct);
                if(db.SaveChanges() > 0) return true;
            }
            return false;
        }

        public bool ChangeProductsStatus(List<Product> products)
        {
           foreach(var product in products)
            {
                db.Products.Find(product.Id);
                product.Status = !product.Status;
                db.SaveChanges();
            }
            return true;
        }
    }
}
