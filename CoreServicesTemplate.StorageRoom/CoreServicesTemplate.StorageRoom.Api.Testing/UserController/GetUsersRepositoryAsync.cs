using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Api.Testing.Fixtures;
using CoreServicesTemplate.StorageRoom.Data.Builders;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Xunit;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.UserController
{
    [Collection("RepositoryTestBase")]
    public class GetUsersRepositoryAsync
    {
        private readonly HttpClient _client;
        private readonly TestFixtureRepositories _fixture;

        public GetUsersRepositoryAsync(WebApplicationFactory<Startup> factory, TestFixtureRepositories fixture)
        {
            _fixture = fixture;
            _client = _fixture.GenerateClient(factory);
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

            _fixture.UserRepositoryMock.Setup(depot => depot.GetAllCustomAsync()).Returns(Task.FromResult(userEntities));

            //Act
            var url = API.StorageRoom.User.GetAllUserToStorageRoomUrl();
            var result = await _client.GetFromJsonAsync<UsersApiModel>(url);

            //Assert
            _fixture.UserRepositoryMock.Verify((c => c.GetAllCustomAsync()), Times.Once());
            result.UsersApiModelList.Should().HaveCountGreaterThan(0);
        }
    }
}
