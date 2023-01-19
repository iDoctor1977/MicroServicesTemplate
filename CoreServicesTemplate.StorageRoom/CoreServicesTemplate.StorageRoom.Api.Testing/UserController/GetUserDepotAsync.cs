using CoreServicesTemplate.Shared.Core.Models;
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
using CoreServicesTemplate.Shared.Core.Services;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.UserController
{
    [Collection("DepotTestBase")]
    public class GetUserDepotAsync
    {
        private readonly HttpClient _client;
        private readonly TestFixtureDepots _fixture;

        public GetUserDepotAsync(WebApplicationFactory<Startup> factory, TestFixtureDepots fixture)
        {
            _fixture = fixture;
            _client = _fixture.GenerateClient(factory);
        }

        [Fact]
        public async Task Should_Return_Specified_User()
        {
            //Arrange
            var userModelBuilder = new UserModelBuilder();
            var userApiModelBuilder = new UserApiModelBuilder();

            var usersApiModel = new UsersApiModel
            {
                UsersApiModelList = userApiModelBuilder
                    .AddUser("Foo", "Foo Foo", DateTime.Now.AddDays(-123987))
                    .AddUser("Duffy", "Duck", DateTime.Now.AddDays(-187962))
                    .AddUser("Micky", "Mouse", DateTime.Now.AddDays(-22897))
                    .Build()
            };

            var usersModel = new UsersAppModel
            {
                UsersModelList = userModelBuilder
                    .AddUser("Foo", "Foo Foo", DateTime.Now.AddDays(-123987))
                    .AddUser("Duffy", "Duck", DateTime.Now.AddDays(-187962))
                    .AddUser("Micky", "Mouse", DateTime.Now.AddDays(-22897))
                    .Build()
            };


            var modelMock = usersModel.UsersModelList.ElementAtOrDefault(2);
            _fixture.GetUserDepotMock.Setup(depot => depot.HandleAsync(It.IsAny<UserAppModel>())).Returns(Task.FromResult(modelMock));

            //Act
            UserApiModel userApiModel = usersApiModel.UsersApiModelList.ElementAt(2);

            var serializedObject = JsonConvert.SerializeObject(userApiModel);
            var url = API.StorageRoom.User.GetUserToStorageRoomUrl();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
                Content = new StringContent(serializedObject, Encoding.Default, "application/json")
            };

            var result = await _client.SendAsync(request);

            //Assert
            _fixture.GetUserDepotMock.Verify((c => c.HandleAsync(It.IsAny<UserAppModel>())), Times.Once());
        }
    }
}
