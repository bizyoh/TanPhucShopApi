
using FluentAssertions;
using IntegrationTest.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TanPhucShopApi.Models.DTO.Product;

namespace IntegrationTest
{
    public class ProductControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly string BASE_URL;
        private readonly HttpClient client;
        private readonly CustomWebApplicationFactory<Program> factory;
        public ProductControllerTest(CustomWebApplicationFactory<Program> _factory)
        {
            factory = _factory;
            client = factory.CreateClient();
            BASE_URL = "https://localhost:7023/api/";
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TestAuthHandler.AuthenticationScheme);
        }

        [Fact] 
        public async Task Get_All_Product_Admin_View_Model_Successfully()
        {
            var response = await client.GetAsync("products/admin");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_All_ProductSuccessfully()
        {
            var response = await client.GetAsync("products");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task Get_All_Products_Dto_By_Category_Id_Successfully()
        {
            var response = await client.GetAsync("products/categories/2");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_All_Products_Top_3_By_Date_Successfully()
        {
            var response = await client.GetAsync("products/3/products");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_All_Products_Dto_By_Status_Successfully()
        {
            var response = await client.GetAsync("products/status?status=true");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_Product_Successfully()
        {
            var file = File.OpenRead(@"PhotoTest/avata.PNG");
            var fileContent = new StreamContent(file);
            var multipart = new MultipartFormDataContent();
            multipart.Add(new StringContent("Phoduct Test"), "Name");
            multipart.Add(new StringContent("2"), "CategoryId");
            multipart.Add(new StringContent("15"), "Quantity");
            multipart.Add(new StringContent("true"), "Status");
            multipart.Add(new StringContent("2000"), "Price");
            multipart.Add(new StringContent("Product so good"), "Description");
            multipart.Add(fileContent,"File","avata.PNG");

            var response = await client.PostAsync("products",multipart);

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Create_Product_With_Fail_Validation_Fail()
        {
            var file = File.OpenRead(@"PhotoTest/avata.PNG");
            var fileContent = new StreamContent(file);
            var multipart = new MultipartFormDataContent();
            multipart.Add(new StringContent(""), "Name");
            multipart.Add(new StringContent("2"), "CategoryId");
            multipart.Add(new StringContent("15"), "Quantity");
            multipart.Add(new StringContent("true"), "Status");
            multipart.Add(new StringContent("2000"), "Price");
            multipart.Add(new StringContent("Product so good"), "Description");
            multipart.Add(fileContent, "File", "avata.PNG");

            var response = await client.PostAsync("products", multipart);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Product_Successfully()
        {
            var product = new UpdateProductDto()
            {
                Id=1,
                Status = true,
                CategoryId = 2,
                Name="ABCCC",
                Price=200,
                Quantity=200,
                Description="ssasfasf"
            };
            var response = await client.PutAsJsonAsync("products/1", product);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Update_Product_With_Not_Found_Id_Fail()
        {
            var product = new UpdateProductDto()
            {
                Id = 100,
                Status = true,
                CategoryId = 2,
                Name = "ABCCC",
                Price = 200,
                Quantity = 200,
                Description = "ssasfasf"
            };
            var response = await client.PutAsJsonAsync("products/100", product);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }


        [Fact]
        public async Task Update_Product_With_Fail_Validation_Fail()
        {
            var product = new UpdateProductDto()
            {
                Id = 1,
                Status = true,
                CategoryId = 2,
                Name = "",
                Price = 200,
                Quantity = 200,
                Description = ""
            };
            var response = await client.PutAsJsonAsync("products/1", product);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Upload_Photo_Successfully()
        {
            var file = File.OpenRead(@"PhotoTest/avata.PNG");
            var fileContent = new StreamContent(file);
            var multipart = new MultipartFormDataContent();
            multipart.Add(fileContent, "file", "avata.PNG");
            var response = await client.PostAsync("products/1/uploadphoto", multipart);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Upload_Photo_With_Not_Found_Id_Fail()
        {
            var file = File.OpenRead(@"PhotoTest/avata.PNG");
            var fileContent = new StreamContent(file);
            var multipart = new MultipartFormDataContent();
            multipart.Add(fileContent, "file", "avata.PNG");
            var response = await client.PostAsync("products/100/uploadphoto", multipart);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact] 
        public async Task Get_Product_Cart_Dto_By_Id_Successfully()
        {
            var response = await client.GetAsync("products/cart/2");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_Product_Cart_Dto_By_Id_With_Id_Not_Found_Fail()
        {
            var response = await client.GetAsync("products/cart/200");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_Detail_Product_By_Id_With_Id_Not_Found_Fail()
        {
            var response = await client.GetAsync("products/200");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_Detail_Product_By_Id_With_Id_Successfully()
        {
            var response = await client.GetAsync("products/1");
            var content = response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Change_Status_Product()
        {
            var response = await client.GetAsync("products/1/status");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

  
    }
}
