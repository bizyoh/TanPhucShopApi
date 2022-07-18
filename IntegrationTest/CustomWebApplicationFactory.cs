
using IntegrationTest.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TanPhucShopApi.Data;
using TanPhucShopApi.Models;

namespace IntegrationTest
{

    public class CustomWebApplicationFactory<TStartup>
     : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<AppDBContext>));

                services.Remove(descriptor);

                services.AddDbContext<AppDBContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting").ConfigureWarnings(w=>w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                });

                var sp = services.BuildServiceProvider();
               
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<AppDBContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    db.Database.EnsureCreated();

                    try
                    {
                        Utilities.Utilities.InitializeDbForTests(db);
                       //Utilities.InitializeDbForTests(db);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            "database with test messagess. Error: {Message}", ex.Message);
                    }
                }
            });
            builder.ConfigureTestServices(services =>
            {
                //services.Configure<TestAuthHandlerOptions>(options => options.RoleUser = RoleUser);
                services.AddAuthentication(TestAuthHandler.AuthenticationScheme)
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.AuthenticationScheme, options => { });
            });
        }
    }
}



