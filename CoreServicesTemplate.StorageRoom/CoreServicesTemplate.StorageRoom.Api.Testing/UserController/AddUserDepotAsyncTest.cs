using System.Net.Http.Json;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Api.Testing.UserController.Fixtures;
using CoreServicesTemplate.StorageRoom.Common.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.UserController
{
    public class AddUserDepotAsyncTest : IClassFixture<ApiDepotCustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly ApiDepotCustomWebApplicationFactory<Program> _factory;

        public AddUserDepotAsyncTest(ApiDepotCustomWebApplicationFactory<Program> factory)
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
                    PostalCode = "44123",
                    City = "Ferrara"
                }
            };

            _factory.AddUserDepotMock.Setup(depot => depot.HandleAsync(It.IsAny<UserAppModel>()));

            //Act
            var url = ApiUrl.StorageRoom.User.AddUserToStorageRoom();
            await _client.PostAsJsonAsync($"{url}/{modelApi}", modelApi);

            //Assert
            _factory.AddUserDepotMock.Verify((c => c.HandleAsync(It.Is<UserAppModel>(arg => arg.Name == modelApi.Name))));
        }
    }
}