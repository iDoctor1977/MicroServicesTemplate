using CoreServicesTemplate.Dashboard.Api.Controllers;
using CoreServicesTemplate.Dashboard.Api.Testing.Fixtures;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles;
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
            //client.PostAsJsonAsync();

            var controller = new UserController(
                _factory.Services.GetRequiredService<IFeatureQuery<UsersAppModel>>(),
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
