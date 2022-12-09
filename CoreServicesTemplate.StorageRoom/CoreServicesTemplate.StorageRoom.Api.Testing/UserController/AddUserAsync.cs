using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.Shared.Core.Resources;
using CoreServicesTemplate.StorageRoom.Api.Testing.Fixtures;
using CoreServicesTemplate.StorageRoom.Common.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Xunit;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.UserController
{
    [Collection("BaseTest")]
    public class AddUserAsync
    {
        private readonly HttpClient _client;
        private readonly TestFixtureBase _fixture;

        public AddUserAsync(WebApplicationFactory<Startup> factory, TestFixtureBase fixture)
        {
            _fixture = fixture;
            _client = _fixture.GenerateClient(factory);
        }

        [Fact]
        public async Task Should_ExecuteCreationNewUser()
        {
            //Arrange
            var modelApi = new UserApiModel
            {
                Name = "Foo",
                Surname = "Foo Foo",
                Birth = DateTime.Now.AddDays(-14000)
            };

            _fixture.AddUserDepotMock.Setup(depot => depot.HandleAsync(It.IsAny<UserModel>()));

            //Act
            await _client.PostAsJsonAsync($"{ApiUrlStrings.StorageRoomUserControllerLocalhostUrl}/AddUser/{modelApi}", modelApi);

            //Assert
            _fixture.AddUserDepotMock.Verify((c => c.HandleAsync(It.Is<UserModel>(arg => arg.Name == modelApi.Name))));
        }
    }
}
