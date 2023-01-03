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
    [Collection("DepotTestBase")]
    public class AddUserDepotAsync
    {
        private readonly HttpClient _client;
        private readonly TestFixtureDepots _fixture;

        public AddUserDepotAsync(WebApplicationFactory<Startup> factory, TestFixtureDepots fixture)
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

            _fixture.AddUserDepotMock.Setup(depot => depot.HandleAsync(It.IsAny<UserAppModel>()));

            //Act
            await _client.PostAsJsonAsync($"{ApiUrlStrings.StorageRoomUserControllerLocalhostAddUserUrl}/{modelApi}", modelApi);

            //Assert
            _fixture.AddUserDepotMock.Verify((c => c.HandleAsync(It.Is<UserAppModel>(arg => arg.Name == modelApi.Name))));
        }
    }
}
