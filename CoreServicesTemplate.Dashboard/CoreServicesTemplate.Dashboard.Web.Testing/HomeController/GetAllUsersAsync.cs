using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Web.Models;
using CoreServicesTemplate.Dashboard.Web.Testing.Fixtures;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoreServicesTemplate.Dashboard.Web.Testing.HomeController
{
    public class GetAllUsersAsync : IClassFixture<WebCustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebCustomWebApplicationFactory<Program> _factory;

        public GetAllUsersAsync(WebCustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Should_Execute_Reading_Users_With_StorageRoomServiceMock()
        {
            //Arrange
            var controller = new Controllers.HomeController(
                _factory.Services.GetRequiredService<IConsolidator<UserViewModel, UserAppModel>>(),
                _factory.Services.GetRequiredService<IConsolidator<UsersViewModel, UsersAppModel>>(),
                _factory.Services.GetRequiredService<IFeatureCommand<UserAppModel>>(),
                _factory.Services.GetRequiredService<IFeatureQuery<UsersAppModel>>(),
                _factory.Services.GetRequiredService<ILogger<Controllers.HomeController>>());

            //Act
            var result = await controller.GetAll();

            //Assert
            _factory.StorageRoomServiceMock.Verify((c => c.GetUsersAsync()), Times.Once);
        }
    }
}