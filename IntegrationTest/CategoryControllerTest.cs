using FluentAssertions;
using IntegrationTest.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TanPhucShopApi.Models.DTO.Category;

namespace IntegrationTest
{
    public class CategoryControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly string BASE_URL;
        private readonly HttpClient client;
        private readonly CustomWebApplicationFactory<Program> factory;
        public CategoryControllerTest(CustomWebApplicationFactory<Program> _factory)
        {
            factory = _factory;
            client = factory.CreateClient();
            BASE_URL = "https://localhost:7023/api/";
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TestAuthHandler.AuthenticationScheme);
        }

        [Fact]
        public async Task Get_All_Category_Successfully()
        {
            var response = await client.GetAsync("categories");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }


        [Fact]
        public async Task Get_Category_By_Id_Not_Found_Fail()
        {
            var response = await client.GetAsync("categories/100");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_Category_By_Id_Successfully()
        {
            var response = await client.GetAsync("categories/2");
            response.EnsureSuccessStatusCode();
            response.Content.ReadAsStringAsync().Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_Category_By_Status_Successfully()
        {
            var response = await client.GetAsync("categories/status?status=true");
            response.EnsureSuccessStatusCode();
            response.Content.ReadAsStringAsync().Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_Category_By_Status_Fail()
        {
            var response = await client.GetAsync("categories/status?status=false");
            response.EnsureSuccessStatusCode();
            var content =await response.Content.ReadAsStringAsync();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_Category_Successfully()
        {
            var category = new CreateCategoryDto()
            {
                Name = "Category Test",
                Status = true,
            };
            var response = await client.PostAsJsonAsync("categories",category);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Create_Category_Fail()
        {
            var category = new CreateCategoryDto();
          
            var response = await client.PostAsJsonAsync("categories", category);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Category_Successfully()
        {
            var category = new UpdateCategoryDto()
            {
                Name = "abccd",
                Status = false
            };
            var response = await client.PutAsJsonAsync("categories/1", category);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }


        [Fact]
        public async Task Update_Category_With_Not_Found_Fail()
        {
            var category = new UpdateCategoryDto()
            {
                Name = "abccd",
                Status = false
            };
            var response = await client.PutAsJsonAsync("categories/100", category);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Update_Category_With_Exist_Category_Name_Fail()
        {
            var category = new UpdateCategoryDto()
            {
                Name = "Jean",
                Status = false
            };
            var response = await client.PutAsJsonAsync("categories/1", category);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

    }
}
