using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.Shared.Core.Resources;
using CoreServicesTemplate.StorageRoom.Api.Testing.Fixtures;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Xunit;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.UserController
{
    [Collection("RepositoryTestBase")]
    public class AddUserRepositoryAsync
    {
        private readonly HttpClient _client;
        private readonly TestFixtureRepositories _fixture;

        public AddUserRepositoryAsync(WebApplicationFactory<Startup> factory, TestFixtureRepositories fixture)
        {
            _fixture = fixture;
            _client = _fixture.GenerateClient(factory);
        }

        [Fact]
        public async Task Should_Execute_Adding_New_User()
        {
            //Arrange
            var modelApi = new UserApiModel
            {
                Name = "Foo",
                Surname = "Foo Foo",
                Birth = DateTime.Now.AddDays(-14000)
            };

            _fixture.UserRepositoryMock.Setup(repo => repo.AddEntity(It.IsAny<User>()));

            //Act
            await _client.PostAsJsonAsync($"{ApiUrlStrings.StorageRoomUserControllerLocalhostAddUserUrl}/{modelApi}", modelApi);

            //Assert
            _fixture.UserRepositoryMock.Verify((repo => repo.AddEntity(It.Is<User>(arg => arg.Name == modelApi.Name))));
        }
    }
}
