using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.Shared.Core.Resources;
using CoreServicesTemplate.StorageRoom.Api.Testing.Fixtures;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Data.Builders;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Xunit;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.UserController
{
    [Collection("BaseTest")]
    public class GetUsersAsync
    {
        private readonly HttpClient _client;
        private readonly TestFixtureBase _fixture;

        public GetUsersAsync(WebApplicationFactory<Startup> factory, TestFixtureBase fixture)
        {
            _fixture = fixture;
            _client = _fixture.GenerateClient(factory);
        }

        [Fact]
        public async Task Should_ExecuteReadingUsers()
        {
            //Arrange
            var userBuilder = new UserModelBuilder();
            var users = new UsersModel
            {
                UsersModelList = userBuilder
                    .AddUser("Foo", "Foo Foo", DateTime.Now.AddDays(-123987))
                    .AddUser("Duffy", "Duck", DateTime.Now.AddDays(-187962))
                    .AddUser("Micky", "Mouse", DateTime.Now.AddDays(-22897))
                    .Build()
            };

            _fixture.GetUsersDepotMock.Setup(depot => depot.HandleAsync()).Returns(Task.FromResult(users));

            //Act
            var result = await _client.GetFromJsonAsync<UsersApiModel>($"{ApiUrlStrings.StorageRoomUserControllerLocalhostUrl}/GetUsers");

            //Assert
            _fixture.GetUsersDepotMock.Verify((c => c.HandleAsync()), Times.Once());
            result.UsersApiModelList.Should().HaveCountGreaterThan(0);
        }
    }
}
