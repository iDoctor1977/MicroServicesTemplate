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
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Builders;
using Newtonsoft.Json;
using Xunit;
using System.Text;

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
            var userApiBuilder = new UserApiModelBuilder();

            var usersApiModel = new UsersApiModel
            {
                UsersApiModelList = userApiBuilder
                    .AddUser("Foo", "Foo Foo", DateTime.Now.AddDays(-123987))
                    .AddUser("Duffy", "Duck", DateTime.Now.AddDays(-187962))
                    .AddUser("Micky", "Mouse", DateTime.Now.AddDays(-22897))
                    .Build()
            };

            var users = new UsersModel
            {
                UsersModelList = userBuilder
                    .AddUser("Foo", "Foo Foo", DateTime.Now.AddDays(-123987))
                    .AddUser("Duffy", "Duck", DateTime.Now.AddDays(-187962))
                    .AddUser("Micky", "Mouse", DateTime.Now.AddDays(-22897))
                    .Build()
            };

            var modelMock = users.UsersModelList.ElementAtOrDefault(2);
            _fixture.GetUserDepotMock.Setup(depot => depot.HandleAsync(It.IsAny<UserModel>())).Returns(Task.FromResult(modelMock));

            //Act
            UserApiModel userApiModel = usersApiModel.UsersApiModelList.ElementAt(2);

            var serializedObject = JsonConvert.SerializeObject(userApiModel);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{ApiUrlStrings.StorageRoomUserControllerLocalhostGetUserUrl}"),
                Content = new StringContent(serializedObject, Encoding.Default, "application/json")
            };

            var result = await _client.SendAsync(request);

            //Assert
            _fixture.GetUserDepotMock.Verify((c => c.HandleAsync(It.IsAny<UserModel>())), Times.Once());
        }
    }
}
