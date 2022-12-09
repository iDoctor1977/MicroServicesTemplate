using System.Net.Http;
using CoreServicesTemplate.Shared.Core.Filters;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.Fixtures
{
    public class TestFixtureBase
    {
        public Mock<ILogger<ApiLogActionFilterAsync>> LoggerMock { get; private set; }
        public Mock<IAddUserDepot> AddUserDepotMock { get; private set; }
        public Mock<IGetUserDepot> GetUserDepotMock { get; set; }
        public Mock<IGetUsersDepot> GetUsersDepotMock { get; set; }

        public HttpClient GenerateClient(WebApplicationFactory<Startup> factory)
        {
            AddUserDepotMock = new Mock<IAddUserDepot>();
            GetUserDepotMock = new Mock<IGetUserDepot>();
            GetUsersDepotMock = new Mock<IGetUsersDepot>();
            LoggerMock = new Mock<ILogger<ApiLogActionFilterAsync>>();

            var client = factory.WithWebHostBuilder(hostBuilder =>
            {
                //hostBuilder.UseStartup<Startup>();
                hostBuilder.ConfigureServices(services =>
                {
                    services.Replace(new ServiceDescriptor(typeof(IAddUserDepot), AddUserDepotMock.Object));
                    services.Replace(new ServiceDescriptor(typeof(IGetUserDepot), GetUserDepotMock.Object));
                    services.Replace(new ServiceDescriptor(typeof(IGetUsersDepot), GetUsersDepotMock.Object));
                    services.AddTransient(provider => LoggerMock.Object);
                });
            }).CreateClient();

            return client;
        }
    }
}
