using CoreServicesTemplate.Dashboard.Common.Interfaces.IServices;
using CoreServicesTemplate.Shared.Core.Builders;
using CoreServicesTemplate.Shared.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace CoreServicesTemplate.Dashboard.Api.Testing.Fixtures
{
    public class ApiCustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        public Mock<IStorageRoomService> StorageRoomServiceMock { get; }

        public ApiCustomWebApplicationFactory()
        {
            StorageRoomServiceMock = new Mock<IStorageRoomService>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var userBuilder = new UserApiModelBuilder();
            var users = userBuilder
                .AddUser("Foo", "Foo Foo", DateTime.Now.AddDays(-12369))
                .AddUser("Matt", "Daemon", DateTime.Now.AddDays(-36982))
                .AddUser("Duffy", "Duck", DateTime.Now.AddDays(-11023))
                .AddUser("Micky", "Mouse", DateTime.Now.AddDays(-693983))
                .Build();

            var model = new UsersApiModel()
            {
                UsersApiModelList = users
            };

            StorageRoomServiceMock.Setup(service => service.GetUsersAsync()).ReturnsAsync(model);

            builder.ConfigureServices(services =>
            {
                services.Replace(new ServiceDescriptor(typeof(IStorageRoomService), StorageRoomServiceMock.Object));
            });

            builder.UseEnvironment("Development");
        }
    }
}
