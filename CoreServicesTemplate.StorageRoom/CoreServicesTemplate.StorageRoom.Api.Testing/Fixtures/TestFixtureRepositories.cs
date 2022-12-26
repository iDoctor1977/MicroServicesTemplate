using System.Net.Http;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.Fixtures
{
    public class TestFixtureRepositories
    {
        public Mock<IUserRepository> UserRepositoryMock { get; private set; }

        public HttpClient GenerateClient(WebApplicationFactory<Startup> factory)
        {
            UserRepositoryMock = new Mock<IUserRepository>();

            var client = factory.WithWebHostBuilder(hostBuilder =>
            {
                hostBuilder.ConfigureServices(services =>
                {
                    services.Replace(new ServiceDescriptor(typeof(IUserRepository), UserRepositoryMock.Object));
                });
            }).CreateClient();

            return client;
        }
    }
}
