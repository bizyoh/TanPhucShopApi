using FluentAssertions;
using IntegrationTest.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TanPhucShopApi.Models;
using TanPhucShopApi.Models.DTO.InvoiceDto;

namespace IntegrationTest
{
    public class InvoiceControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly string BASE_URL;
        private readonly HttpClient client;
        private readonly CustomWebApplicationFactory<Program> factory;
        public InvoiceControllerTest(CustomWebApplicationFactory<Program> _factory)
        {
            factory = _factory;
            client = factory.CreateClient();
            BASE_URL = "https://localhost:7023/api/";
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TestAuthHandler.AuthenticationScheme);
        }

        [Fact]
        public async Task Create_Invoice_Successfully()
        {
            var invoice = new CreateInvoiceDto()
            {
                Created = DateTime.Now,
                Status = true,
                UserId = 1,
                CreateInvoiceDetailDtos = new List<CreateInvoiceDetailDto>()
                {
                    new CreateInvoiceDetailDto()
                    {
                        ProductId= 2,
                        Amount= 2
                    },
                     new CreateInvoiceDetailDto()
                    {
                        ProductId= 1,
                        Amount= 5
                    }
                }
            };

        }

        [Fact]
        public async Task Create_Invoice_With_Out_Of_Stock_Product_Fail()
        {
            var invoice = new CreateInvoiceDto()
            {
                Created = DateTime.Now,
                Status = true,
                UserId = 1,
                CreateInvoiceDetailDtos = new List<CreateInvoiceDetailDto>()
                {
                    new CreateInvoiceDetailDto()
                    {
                        ProductId= 4,
                        Amount= 1       //product 4 quantity is 0
                    },
                     new CreateInvoiceDetailDto()
                    {
                        ProductId= 1,
                        Amount= 5
                    }
                }
            };
         
            var response = await client.PostAsJsonAsync("invoices", invoice);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Get_All_Invoice_User_View_Model_Successfully()
        {
            var response = await client.GetAsync("invoices");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact] 
        public async Task Get_All_Invoice_User_Detail_View_Model_Successfully()
        {
            var response = await client.GetAsync("invoices/2/detail");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_All_Invoice_User_Detail_View_Model_With_Id_Not_Found_Fail()
        {
            var response = await client.GetAsync("invoices/200/detail");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }


        [Fact]
        public async Task Get_All_Invoice_User_View_Model_By_Id_With_Id_Not_Found_Fail()
        {
            var response = await client.GetAsync("invoices/user/200");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound
                );
        }
    }
}