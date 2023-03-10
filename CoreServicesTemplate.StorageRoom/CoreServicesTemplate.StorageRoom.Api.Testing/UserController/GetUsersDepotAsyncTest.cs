using System.Net.Http.Json;
using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Api.Testing.UserController.Fixtures;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Data.Builders;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.UserController
{
    public class GetUsersDepotAsyncTest : IClassFixture<ApiDepotCustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly ApiDepotCustomWebApplicationFactory<Program> _factory;

        public GetUsersDepotAsyncTest(ApiDepotCustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Should_Access_To_UserDepot_HandleAsync_At_Last_Once()
        {
            //Arrange
            var userBuilder = new UserModelBuilder();
            var users = new UsersAppModel
            {
                UsersModelList = userBuilder
                    .AddUser("Foo", "Foo Foo", DateTime.Now.AddDays(-123987))
                    .AddUser("Duffy", "Duck", DateTime.Now.AddDays(-187962))
                    .AddUser("Micky", "Mouse", DateTime.Now.AddDays(-22897))
                    .Build()
            };

            _factory.GetUsersDepotMock.Setup(depot => depot.ExecuteAsync()).Returns(Task.FromResult(new OperationResult<UsersAppModel>(users)));

            //Act
            var url = ApiUrl.StorageRoom.User.GetAllUserToStorageRoom();
            var result = await _client.GetFromJsonAsync<UsersApiModel>(url);

            //Assert
            _factory.GetUsersDepotMock.Verify((c => c.ExecuteAsync()), Times.Once());
            result.UsersApiModelList.Should().HaveCountGreaterThan(0);
        }
    }
}
