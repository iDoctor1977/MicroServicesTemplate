using System.Globalization;
using System.Net;
using CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Web.Models;
using CoreServicesTemplate.Dashboard.Web.Testing.Fixtures;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoreServicesTemplate.Dashboard.Web.Testing.HomeController
{
    public class AddUserAsync : IClassFixture<WebCustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebCustomWebApplicationFactory<Program> _factory;

        public AddUserAsync(WebCustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Should_Execute_Creation_New_User_With_StorageRoomServiceMock()
        {
            //Arrange
            var userViewModel = new UserViewModel
            {
                Name = "Foo",
                Surname = "Foo Foo",
                Birth = DateTime.Now.AddDays(-26985).ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)
            };

            var controller = new Controllers.HomeController(
                _factory.Services.GetRequiredService<IMapperService<UserViewModel, UserAppModel>>(),
                _factory.Services.GetRequiredService<IMapperService<UsersViewModel, UsersAppModel>>(),
                _factory.Services.GetRequiredService<IAddUserFeature>(),
                _factory.Services.GetRequiredService<IGetUsersFeature>(),
                _factory.Services.GetRequiredService<ILogger<Controllers.HomeController>>());

            _factory.StorageRoomServiceMock.Setup(service => service.AddUserAsync(It.IsAny<UserApiModel>())).ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });

            //Act
            await controller.Add(userViewModel);

            //Assert
            _factory.StorageRoomServiceMock.Verify(method => method.AddUserAsync(It.IsAny<UserApiModel>()), Times.Once);
        }
    }
}