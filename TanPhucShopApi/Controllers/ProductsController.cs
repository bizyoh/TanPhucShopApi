﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TanPhucShopApi.Models;
using TanPhucShopApi.Models.DTO.Product;
using TanPhucShopApi.Services.ProductService;

namespace TanPhucShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService productService;
        private IConfiguration config;
        private string BASE_URL;
        public ProductsController(IProductService _productService, IConfiguration _config)
        {
            productService = _productService;
            config = _config;
            BASE_URL = config["BASE_URL"];
        }

        [HttpGet]
        public IActionResult GetAllProduct()
        {
            var products = productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("3/products")]
        public IActionResult GetAllProductTop3ByDate()
        {
            var products = productService.GetAllProductsDtoTop3ByDate();
            return Ok(products);
        }

        [HttpGet("status={statusUri}")]
        public IActionResult GetAllProductsDtoByStatus(string statusUri)
        {
            var status = bool.Parse(statusUri);
            var products = productService.GetAllProductsDtoByStatus(status);
            return Ok(products);
        }

        [HttpPost]
        public IActionResult Create(CreateProductDto createProductDto)
        {
            if (createProductDto != null)
            {
                var createdProduct = productService.Create(createProductDto);
                if (createdProduct != null) return Created(BASE_URL+"api/product",createdProduct);
            }
            return BadRequest();
        }

        [HttpPut("id")]
        public IActionResult Update(int id,UpdateProductDto updateProductDto)
        {
            if (updateProductDto != null)
            {
                var result = productService.Update(id,updateProductDto);
                if (result) return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            var product = productService.GetById(id);
            if (product == null) return NotFound();
            else
            {
                var result = productService.HardDelete(id);
                if(result) return Ok();
            }
            return BadRequest();
        }

        [HttpPost("uploadphoto/{id}")]
        public IActionResult UploadPhoto(int id,  IFormFile file)
        {
            try
            {
                return Ok(productService.UploadPhoto(id, file));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetDetailProductById(int id)
        {
            var product = productService.GetDetailProductDtoById(id);
            if (product == null) return NotFound();
            else
            {
                return Ok(product);
            }
            return BadRequest();
        }
    }

}
