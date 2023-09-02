/*using AgroExpressAPI.ApplicationContext;
using MediatR;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AgroExpressTests.IntegratedTest
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
    {
        private readonly IServiceScope _scope;
        protected readonly ISender Sender;
        protected readonly ApplicationDbContext DbContext;

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
        {
            _scope = factory.Services.CreateScope();

            Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
            DbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }

        public void Dispose()
        {
            _scope?.Dispose();
            // DbContext?.Dispose();
        }
    }
}
*/