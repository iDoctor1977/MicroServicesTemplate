using CoreServicesTemplate.Dashboard.Api.Controllers;
using CoreServicesTemplate.Dashboard.Api.Testing.Fixtures;
using CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Shared.Core.Builders;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoreServicesTemplate.Dashboard.Api.Testing.HomeApiController
{
    public class GetAllUsersAsync : IClassFixture<ApiCustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly ApiCustomWebApplicationFactory<Program> _factory;

        public GetAllUsersAsync(ApiCustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Should_Execute_Read_Users()
        {
            //Arrange
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

            _factory.StorageRoomServiceMock.Setup(service => service.GetUsersAsync()).ReturnsAsync(model);

            //client.PostAsJsonAsync();

            var controller = new UserController(
                _factory.Services.GetRequiredService<IGetUsersFeature>(),
                _factory.Services.GetRequiredService<IConsolidator<UsersApiModel, UsersAppModel>>(),
                _factory.Services.GetRequiredService<ILogger<UserController>>());

            //Act
            var result = await controller.GetAll();

            //Assert
            _factory.StorageRoomServiceMock.Verify((method => method.GetUsersAsync()), Times.Once());
            result.Value?.UsersApiModelList.Should().AllBeOfType<UserApiModel>().And.HaveCountGreaterThan(0);
        }
    }
}
