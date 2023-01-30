using System.Net.Http.Json;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Api.Testing.UserController.Fixtures;
using CoreServicesTemplate.StorageRoom.Data.Builders;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.UserController
{
    public class GetUsersRepositoryAsyncTest : IClassFixture<ApiRepositoryCustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly ApiRepositoryCustomWebApplicationFactory<Program> _factory;

        public GetUsersRepositoryAsyncTest(ApiRepositoryCustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Should_Access_To_UserRepository_GetEntities_At_Last_Once()
        {
            //Arrange
            var userBuilder = new UserEntityBuilder();
            var userEntities = userBuilder
                .AddUser("Foo", "Foo Foo", DateTime.Now.AddDays(-123987))
                .AddUser("Duffy", "Duck", DateTime.Now.AddDays(-187962))
                .AddUser("Micky", "Mouse", DateTime.Now.AddDays(-22897))
                .Build();

            _factory.UserRepositoryMock.Setup(depot => depot.GetAllCustomAsync()).Returns(Task.FromResult(userEntities));

            //Act
            var url = ApiUrl.StorageRoom.User.GetAllUserToStorageRoom();
            var result = await _client.GetFromJsonAsync<UsersApiModel>(url);

            //Assert
            _factory.UserRepositoryMock.Verify((c => c.GetAllCustomAsync()), Times.Once());
            result.UsersApiModelList.Should().HaveCountGreaterThan(0);
        }
    }
}