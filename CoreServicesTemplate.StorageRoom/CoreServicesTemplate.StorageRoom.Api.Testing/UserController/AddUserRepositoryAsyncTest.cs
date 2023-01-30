using System.Net.Http.Json;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Api.Testing.UserController.Fixtures;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.UserController
{
    public class AddUserRepositoryAsyncTest : IClassFixture<ApiRepositoryCustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly ApiRepositoryCustomWebApplicationFactory<Program> _factory;

        public AddUserRepositoryAsyncTest(ApiRepositoryCustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
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

            _factory.UserRepositoryMock.Setup(repo => repo.AddCustomAsync(It.IsAny<User>()));

            //Act
            var url = ApiUrl.StorageRoom.User.AddUserToStorageRoom();
            await _client.PostAsJsonAsync($"{url}/{modelApi}", modelApi);

            //Assert
            _factory.UserRepositoryMock.Verify((repo => repo.AddCustomAsync(It.Is<User>(arg => arg.Name == modelApi.Name))));
        }
    }
}
