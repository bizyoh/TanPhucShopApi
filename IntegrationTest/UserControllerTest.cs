using FluentAssertions;
using IntegrationTest.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TanPhucShopApi.Data;
using TanPhucShopApi.Models.DTO.UserDto;

namespace IntegrationTest
{
    public class UserControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly string BASE_URL;
        private readonly HttpClient client;
        private readonly CustomWebApplicationFactory<Program> factory;
        public UserControllerTest(CustomWebApplicationFactory<Program> _factory)
        {
            factory = _factory;
            client = factory.CreateClient();
            BASE_URL = "https://localhost:7023/api/";
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TestAuthHandler.AuthenticationScheme);
        }

        [Fact]
        public async Task Login_Successfully()
        {
            LoginUserDto user = new LoginUserDto()
            {
                UserNameOrEmail = "admin2",
                Password = "Admin@123"
            };
            var response = await client.PostAsJsonAsync("users/login", user);
            var responseString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Login_With_Wrong_UserName_Or_Password_Fail()
        {
            LoginUserDto user = new LoginUserDto()
            {
                UserNameOrEmail = "ABC",
                Password = "123"
            };
            var response = await client.PostAsJsonAsync("users/login", user);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Create_User_With_Fail_Validation_Fail()
        {
            RegisterUserDto user = new RegisterUserDto()
            {
                UserName = "a",
                Password = "b",
                Address = "c",
                ConfirmPassword = "c",
                Email = "aax",
                FirstName = "ax",
                LastName = "ss",
                PhoneNumber = "aasd"
            };
            var response = await client.PostAsJsonAsync("users", user);
            var responseString = await response.Content.ReadAsStringAsync();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_User_With_Exist_UserName_Fail()
        {
            RegisterUserDto user = new RegisterUserDto()
            {
                UserName = "admin2",
                Password = "Admin@123",
                Address = "40/13/12",
                ConfirmPassword = "Admin@123",
                Email = "Admin@gmail.com",
                FirstName = "phucdeptrai",
                LastName = "phucdeptrai",
                PhoneNumber = "090909099"
            };
            var response = await client.PostAsJsonAsync("users", user);
            var responseString = await response.Content.ReadAsStringAsync();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_User_Successfully()
        {
            RegisterUserDto user = new RegisterUserDto()
            {
                UserName = "admin3",
                Password = "Admin@123",
                Address = "40/13/12",
                ConfirmPassword = "Admin@123",
                Email = "Admin3@gmail.com",
                FirstName = "phucdeptrai",
                LastName = "phucdeptrai",
                PhoneNumber = "090909099"
            };
            var response = await client.PostAsJsonAsync("users",user);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);

        }

        [Fact]
        public async Task Get_All_Successfully()
        {
            var response = await client.GetAsync("users");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().NotBeNull();
        }

   
        [Fact]
        public async Task Update_By_Admin_With_User_Not_Found_Fail()
        {
            UpdateUserDto user = new UpdateUserDto()
            {
                Address = "40/13/12",
                ConfirmPassword = "Admin@123",
                Email = "Admin3@gmail.com",
                FirstName = "phucdeptrai",
                LastName = "phucdeptrai",
                PhoneNumber = "090909099",
                CurrentPassword="Admin@123",
                NewPassword="Phucdeptrai123",
                Status = true
            };
            var response = await client.PutAsJsonAsync("users/admin/5",user);
            var responseString = await response.Content.ReadAsStringAsync();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Update_By_Admin_With_Fail_Validation_Fail()
        {
            UpdateUserDto user = new UpdateUserDto()
            {
                Address = "40/13/12",
                ConfirmPassword = "Admin@123",
                Email = "Admin2@gmail.com",
                FirstName = "",
                LastName = "phucdeptrai",
                PhoneNumber = "090909099",
                CurrentPassword = "Admin@123",
                NewPassword = "Phucdeptrai123",
                Status = true
            };
            var response = await client.PutAsJsonAsync("users/admin/3", user);
            var responseString = await response.Content.ReadAsStringAsync();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


        [Fact]
        public async Task Update_By_Admin_Successfully()
        {
            UpdateUserDto user = new UpdateUserDto()
            {
                Address = "40/13/12",
                ConfirmPassword = "Admin@123",
                Email = "Admin3333@gmail.com",
                FirstName = "",
                LastName = "phucdeptrai",
                PhoneNumber = "090909099",
                CurrentPassword = "Admin@123",
                NewPassword = "Phucdeptrai123",
                Status = true
            };
            var response = await client.PutAsJsonAsync("users/admin/3", user);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }


     

        [Fact]
        public async Task Add_Role_User_Successfully()
        {
            var response = await client.PostAsJsonAsync("Users/AddRole/2", "User");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Add_Role_With_Role_Does_Not_Exist_Fail()
        {
            var response = await client.PostAsJsonAsync("Users/AddRole/1", "123");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


        [Fact]
        public async Task Change_Status_User_With_User_Not_Found_Fail()
        {
            var response = await client.GetAsync("users/700/status");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

      
        [Fact]
        public async Task Change_Status_User_Sucessfully()
        {
            var response = await client.GetAsync("users/2/status");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        [Fact]
        public async Task Remove_Role_With_Role_Does_Not_Exist_Fail()
        {
            var response = await client.PostAsJsonAsync("Users/RemoveRole/2", "123");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


        [Fact]
        public async Task Remove_Role_Successfully()
        {
            var response = await client.PostAsJsonAsync("Users/RemoveRole/2", "User");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Get_By_Id_Fail()
        {
            var response = await client.GetAsync("Users/100");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_By_Id_Successfully()
        {
            var response = await client.GetAsync("Users/1");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Find_Detail_User_Dto_By_Id_Successfully()
        {
            var response = await client.GetAsync("Users/1");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Find__User__By_Role_Not_Found_Fail()
        {
            var response = await client.GetAsync("Users/admin/100");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }


        [Fact]
        public async Task Find_Detail_User_Dto_By_Id_Admin_Sucessfully()
        {
            var response = await client.GetAsync("Users/admin/1");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
