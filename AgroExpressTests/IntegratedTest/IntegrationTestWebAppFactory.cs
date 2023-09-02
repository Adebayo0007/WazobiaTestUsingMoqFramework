/*using AgroExpressAPI.ApplicationContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Testcontainers.MsSql;
using Xunit;

namespace AgroExpressTests.IntegratedTest
{
    public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
       .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
       .WithPassword("Strong_password_123!")
       .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                //ApplicationDbContext descriptor = null;

                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSql(_dbContainer.GetConnectionString()));
            });
        }
        public Task InitializeAsync()
        {
            return _dbContainer.StartAsync();
        }

        public new Task DisposeAsync()
        {
            return _dbContainer.StopAsync();
        }
    }
}
*/