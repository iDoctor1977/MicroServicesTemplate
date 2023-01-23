using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Models;
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
                Birth = DateTime.Now.AddDays(-14000),

                AddressApiModel = new AddressApiModel
                {
                    Address1 = "Via Copparo, 208 int. 10",
                    City = "Ferrara",
                    PostalCode = "44123"
                }
            };

            _fixture.UserRepositoryMock.Setup(repo => repo.AddCustomAsync(It.IsAny<User>()));

            //Act
            var url = ApiUrl.StorageRoom.User.AddUserToStorageRoom();
            await _client.PostAsJsonAsync($"{url}/{modelApi}", modelApi);

            //Assert
            _fixture.UserRepositoryMock.Verify((repo => repo.AddCustomAsync(It.Is<User>(arg => arg.Name == modelApi.Name))));
        }
    }
}
