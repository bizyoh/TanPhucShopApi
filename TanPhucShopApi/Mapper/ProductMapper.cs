using AutoMapper;
using TanPhucShopApi.Models;
using TanPhucShopApi.Models.DTO.Product;

namespace TanPhucShopApi.Mapper
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<UpdateProductDto, Product>().ForAllMembers(opts => opts.Condition((src, des, srcMem) => srcMem != null || srcMem != ""));
            CreateMap<CreateProductDto, Product>();
            CreateMap<Product,CreatedProductDto > ();
        }
    }
}
