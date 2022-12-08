using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.Shared.Core.Resources;
using CoreServicesTemplate.StorageRoom.Api.Testing.Fixtures;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Data.Builders;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.UserController
{
    [Collection("BaseTest")]
    public class GetUserAsync
    {
        private readonly HttpClient _client;
        private readonly TestFixtureBase _fixture;

        public GetUserAsync(WebApplicationFactory<Startup> factory, TestFixtureBase fixture)
        {
            _fixture = fixture;
            _client = _fixture.GenerateClient(factory);
        }

        [Fact]
        public async Task Should_Return_Specified_User()
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

            _fixture.GetUserDepotMock.Setup(depot => depot.HandleAsync(users.UsersModelList.ElementAt(2))).Returns(Task.FromResult(users.UsersModelList.FirstOrDefault()));

            //Act
            var a = $"{ApiUrlStrings.StorageRoomUserControllerLocalhostUrl + users.UsersModelList.FirstOrDefault()}";
            var result = await _client.GetFromJsonAsync<UserApiModel>($"{ApiUrlStrings.StorageRoomUserControllerLocalhostUrl + users.UsersModelList.FirstOrDefault()}");

            //Assert
            _fixture.GetUserDepotMock.Verify((c => c.HandleAsync(It.IsAny<UserModel>())), Times.Once());
            result.Should().BeOfType<UserApiModel>().And.NotBeNull().And.Equals(users.UsersModelList.FirstOrDefault());
        }
    }
}
