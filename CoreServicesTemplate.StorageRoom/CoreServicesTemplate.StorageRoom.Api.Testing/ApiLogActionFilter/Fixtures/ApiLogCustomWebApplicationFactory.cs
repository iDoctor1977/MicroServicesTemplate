using CoreServicesTemplate.Shared.Core.Filters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.ApiLogActionFilter.Fixtures
{
    public class ApiLogCustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        public Mock<ILogger<ApiLogActionFilterAsync>> LoggerMock { get; private set; }

        public ApiLogCustomWebApplicationFactory()
        {
            LoggerMock = new Mock<ILogger<ApiLogActionFilterAsync>>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddTransient(provider => LoggerMock.Object);
            });

            builder.UseEnvironment("Development");
        }
    }
}
