using CoreServicesTemplate.Shared.Core.Filters;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.UserController.Fixtures
{
    public class ApiDepotCustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        public Mock<ILogger<ApiLogActionFilterAsync>> LoggerMock { get; private set; }
        public Mock<IAddUserDepot> AddUserDepotMock { get; private set; }
        public Mock<IGetUserDepot> GetUserDepotMock { get; private set; }
        public Mock<IGetUsersDepot> GetUsersDepotMock { get; private set; }

        public ApiDepotCustomWebApplicationFactory()
        {
            LoggerMock = new Mock<ILogger<ApiLogActionFilterAsync>>();
            AddUserDepotMock = new Mock<IAddUserDepot>();
            GetUserDepotMock = new Mock<IGetUserDepot>();
            GetUsersDepotMock = new Mock<IGetUsersDepot>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddTransient(provider => LoggerMock.Object);
                services.Replace(new ServiceDescriptor(typeof(IAddUserDepot), AddUserDepotMock.Object));
                services.Replace(new ServiceDescriptor(typeof(IGetUserDepot), GetUserDepotMock.Object));
                services.Replace(new ServiceDescriptor(typeof(IGetUsersDepot), GetUsersDepotMock.Object));
            });

            builder.UseEnvironment("Development");
        }
    }
}
