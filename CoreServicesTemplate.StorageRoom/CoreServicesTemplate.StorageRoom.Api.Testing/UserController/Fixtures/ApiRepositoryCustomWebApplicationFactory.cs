using CoreServicesTemplate.StorageRoom.Data.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.UserController.Fixtures
{
    public class ApiRepositoryCustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        public Mock<IUserRepository> UserRepositoryMock { get; }

        public ApiRepositoryCustomWebApplicationFactory()
        {
            UserRepositoryMock = new Mock<IUserRepository>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.Replace(new ServiceDescriptor(typeof(IUserRepository), UserRepositoryMock.Object));
            });

            builder.UseEnvironment("Development");
        }
    }
}
