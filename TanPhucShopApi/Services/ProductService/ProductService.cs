using AutoMapper;
using TanPhucShopApi.Data;
using TanPhucShopApi.Models;
using TanPhucShopApi.Models.DTO.Category;
using TanPhucShopApi.Models.DTO.Product;
using TanPhucShopApi.Services.CategoryService;
using TanPhucShopApi.Services.ProductService;

namespace TanPhucShopApi.Services.ProductService
{
    public class ProductService : IProductService
    {
        private AppDBContext db;
        private IMapper mapper;
        private IWebHostEnvironment webHostEnvironment;
        private IHttpContextAccessor httpContextAccessor;
        private IConfiguration configuration;
        private ICategoryService categoryService;
        public ProductService(AppDBContext _db, IMapper _mapper, IWebHostEnvironment _webHostEnvironment, IHttpContextAccessor _httpContextAccessor, IConfiguration _configuration)
        {
            db = _db;
            mapper = _mapper;
            webHostEnvironment = _webHostEnvironment;
            httpContextAccessor = _httpContextAccessor;
            configuration = _configuration;
        }
        public CreatedProductDto Create(CreateProductDto createProductDto)
        {
            var product = mapper.Map<Product>(createProductDto);
            product.Created = DateTime.Now;
            db.Products.Add(product);
            if (db.SaveChanges() > 0)
            {
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
            return db.Products.Select(x => new GetAllProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Status = x.Status,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name,
                Description = x.Description,
                Created = x.Created,
                Price = x.Price,
                Photo = configuration["IMG_URL"] + "products/" + x.Photo,
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
                if (db.SaveChanges() > 0) return true;
            }
            return false;
        }


        public bool ChangeProductsStatus(List<Product> products)
        {
            foreach (var product in products)
            {
                db.Products.Find(product.Id);
                product.Status = !product.Status;
                db.SaveChanges();
            }
            return true;
        }

        public dynamic UploadPhoto(int id, IFormFile file)
        {
            string fileName = GenerateFileName(file);

            var path = Path.Combine(webHostEnvironment.WebRootPath, "images/products", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            var product = db.Products.Find(id);
            if (product != null)
            {
                string oldFileName = product.Photo;
                product.Photo = fileName;
                db.SaveChanges();
                DeleteFile(oldFileName);
            
            }

            var baseURL = httpContextAccessor.HttpContext.Request.Scheme + "://" + httpContextAccessor.HttpContext.Request.Host + httpContextAccessor.HttpContext.Request.PathBase;
            return (new
            {
                fileURL = baseURL + "/uploads/images/products" + file.FileName,
                fileSize = file.Length,
                fileName = fileName,
            });
        }

        public bool DeleteFile(string fileName)
        {
            string path = Path.Combine(webHostEnvironment.WebRootPath, "images/products", fileName);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
                return true;
            }
            return false;
        }

        public string GenerateFileName(IFormFile file)
        {
            string extension = Path.GetExtension(file.FileName);
            string FileName = Guid.NewGuid().ToString().Replace("-", "");
            string ReturnValue = FileName + extension;
            if (!File.Exists(Path.Combine(webHostEnvironment.WebRootPath, ReturnValue)))
            {
                return ReturnValue;
            }
            return GenerateFileName(file);
        }

        public DetailProductDto GetDetailProductDtoById(int id)
        {
            var product = db.Products.FirstOrDefault(x => x.Id == id);
            var detailProductDto = mapper.Map<DetailProductDto>(product);
            return detailProductDto;
        }

        public List<GetAllProductDto> GetAllProductsDtoByStatus(bool status)
        {
            var products= db.Products.Where(x=>x.Status==status).ToList();
            var getAllProductsDto = mapper.Map<List<Product>,List<GetAllProductDto>>(products);
            return getAllProductsDto;
        }

        public List<GetAllProductDto> GetAllProductsDtoTop3ByDate()
        {
            var products = db.Products.Where(x=>x.Status==true).OrderByDescending(x=>x.Id).Take(3).ToList();
            var getAllProductsDto = mapper.Map<List<Product>, List<GetAllProductDto>>(products);
            return getAllProductsDto;
        }
    }
}
