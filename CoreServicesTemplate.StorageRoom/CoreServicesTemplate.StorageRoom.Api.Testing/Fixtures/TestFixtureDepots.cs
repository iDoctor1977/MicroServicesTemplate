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
    public class TestFixtureDepots
    {
        public Mock<ILogger<ApiLogActionFilterAsync>> LoggerMock { get; private set; }
        public Mock<IAddUserDepot> AddUserDepotMock { get; private set; }
        public Mock<IGetUserDepot> GetUserDepotMock { get; private set; }
        public Mock<IGetUsersDepot> GetUsersDepotMock { get; private set; }

        public HttpClient GenerateClient(WebApplicationFactory<Startup> factory)
        {
            LoggerMock = new Mock<ILogger<ApiLogActionFilterAsync>>();
            AddUserDepotMock = new Mock<IAddUserDepot>();
            GetUserDepotMock = new Mock<IGetUserDepot>();
            GetUsersDepotMock = new Mock<IGetUsersDepot>();

            var client = factory.WithWebHostBuilder(hostBuilder =>
            {
                //hostBuilder.UseStartup<Startup>();
                hostBuilder.ConfigureServices(services =>
                {
                    services.AddTransient(provider => LoggerMock.Object);
                    services.Replace(new ServiceDescriptor(typeof(IAddUserDepot), AddUserDepotMock.Object));
                    services.Replace(new ServiceDescriptor(typeof(IGetUserDepot), GetUserDepotMock.Object));
                    services.Replace(new ServiceDescriptor(typeof(IGetUsersDepot), GetUsersDepotMock.Object));
                });
            }).CreateClient();

            return client;
        }
    }
}
