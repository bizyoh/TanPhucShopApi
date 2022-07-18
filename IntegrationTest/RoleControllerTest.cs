using FluentAssertions;
using IntegrationTest.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TanPhucShopApi.Models.DTO.RoleDto;

namespace IntegrationTest
{
    public class RoleControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly string BASE_URL;
        private readonly HttpClient client;
        private readonly CustomWebApplicationFactory<Program> factory;
        public RoleControllerTest(CustomWebApplicationFactory<Program> _factory)
        {
            factory = _factory;
            client = factory.CreateClient();
            BASE_URL = "https://localhost:7023/api/";
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TestAuthHandler.AuthenticationScheme);
        }

        [Fact]
        public async Task Get_All_RoleDto_Successfully()
        {
            var response = await client.GetAsync("roles");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }


        [Fact]
        public async Task Create_Role_Successfully()
        {
            var role = new CreateRoleDto()
            {
                Name = "Test",
                Description = "alipapapapa"
            };
            var response = await client.PostAsJsonAsync("roles",role);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_Role_With_Exist_Name_Fail()
        {
            var role = new CreateRoleDto()
            {
                Name = "Admin",
                Description = "alipapapapa"
            };
            var response = await client.PostAsJsonAsync("roles", role);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_Role_With_Id_Not_Found_Fail()
        {
            var role = new CreateRoleDto()
            {
                Name = "Admin",
                Description = "alipapapapa"
            };
            var response = await client.PutAsJsonAsync("roles/5", role);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

   
        [Fact]
        public async Task Update_Role_Successfully()
        {
            var role = new CreateRoleDto()
            {
                Name = "TestRole",
                Description = "alipapapapa"
            };
            var response = await client.PutAsJsonAsync("roles/3", role);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }


        [Fact]
        public async Task Delete_Role_Successfully()
        {
            var response = await client.DeleteAsync("roles/2");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Delete_Role_With_Not_Found_Fail()
        {

            var response = await client.DeleteAsync("roles/10");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_Role_By_Id_Successfully()
        {
            var response = await client.GetAsync("roles/1");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_Role_By_Id_With_Not_Found_Fail()
        {
            var response = await client.GetAsync("roles/10");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
