using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.Shared.Core.Services;
using CoreServicesTemplate.StorageRoom.Api.Testing.Fixtures;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Data.Builders;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Xunit;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.UserController
{
    [Collection("DepotTestBase")]
    public class GetUsersDepotAsync
    {
        private readonly HttpClient _client;
        private readonly TestFixtureDepots _fixture;

        public GetUsersDepotAsync(WebApplicationFactory<Startup> factory, TestFixtureDepots fixture)
        {
            _fixture = fixture;
            _client = _fixture.GenerateClient(factory);
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

            _fixture.GetUsersDepotMock.Setup(depot => depot.HandleAsync()).Returns(Task.FromResult(users));

            //Act
            var url = API.StorageRoom.GetAllUserToStorageRoomUrl();
            var result = await _client.GetFromJsonAsync<UsersApiModel>(url);

            //Assert
            _fixture.GetUsersDepotMock.Verify((c => c.HandleAsync()), Times.Once());
            result.UsersApiModelList.Should().HaveCountGreaterThan(0);
        }
    }
}
