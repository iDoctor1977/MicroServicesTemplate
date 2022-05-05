using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Builders;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.Shared.Core.Resources;
using CoreServicesTemplate.StorageRoom.Api.Testing.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Xunit;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.UserController
{
    [Collection("BaseTest")]
    public class ReadUsersGetAsync
    {
        private readonly HttpClient _client;
        private readonly BaseTestFixture _fixture;

        public ReadUsersGetAsync(WebApplicationFactory<Startup> factory, BaseTestFixture fixture)
        {
            _fixture = fixture;
            _client = _fixture.GenerateClient(factory);
        }

        [Fact]
        public async Task Should_ExecuteReadingUsers()
        {
            //Arrange
            var userBuilder = new UserModelBuilder();
            var users = new UsersApiModel
            {
                UsersApiModelList = userBuilder
                    .AddUser("Foo", "Foo Foo", DateTime.Now.AddDays(-123987))
                    .AddUser("Duffy", "Duck", DateTime.Now.AddDays(-187962))
                    .AddUser("Micky", "Mouse", DateTime.Now.AddDays(-22897))
                    .Build()
            };

            _fixture.ReadUsersDepotMock.Setup(depot => depot.HandleAsync()).Returns(Task.FromResult(users));

            //Act
            var result = await _client.GetFromJsonAsync<UsersApiModel>($"{ApiUrlStrings.StorageRoomUserControllerLocalhostUrl}");

            //Assert
            _fixture.ReadUsersDepotMock.Verify((c => c.HandleAsync()), Times.Once());
            result.UsersApiModelList.Should().HaveCountGreaterThan(0);
        }
    }
}
