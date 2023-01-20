using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Api.Testing.Fixtures;
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
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.StorageRoom.Data.Entities;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.UserController
{
    [Collection("RepositoryTestBase")]
    public class GetUserRepositoryAsync
    {
        private readonly HttpClient _client;
        private readonly TestFixtureRepositories _fixture;

        public GetUserRepositoryAsync(WebApplicationFactory<Startup> factory, TestFixtureRepositories fixture)
        {
            _fixture = fixture;
            _client = _fixture.GenerateClient(factory);
        }

        [Fact]
        public async Task Should_Access_To_UserRepository_GetEntityByName_At_Last_Once()
        {
            //Arrange
            var userEntityBuilder = new UserEntityBuilder();
            var userApiModelBuilder = new UserApiModelBuilder();

            var usersApiModel = new UsersApiModel
            {
                UsersApiModelList = userApiModelBuilder
                    .AddUser("Foo", "Foo Foo", DateTime.Now.AddDays(-123987))
                    .AddUser("Duffy", "Duck", DateTime.Now.AddDays(-187962))
                    .AddUser("Micky", "Mouse", DateTime.Now.AddDays(-22897))
                    .Build()
            };

            var userEntity = userEntityBuilder
                .AddUser("Foo", "Foo Foo", DateTime.Now.AddDays(-123987))
                .AddUser("Duffy", "Duck", DateTime.Now.AddDays(-187962))
                .AddUser("Micky", "Mouse", DateTime.Now.AddDays(-22897))
                .Build();

            _fixture.UserRepositoryMock.Setup(repo => repo.GetByNameAsync(It.IsAny<User>())).Returns(Task.FromResult(userEntity.ElementAtOrDefault(2)));

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
            _fixture.UserRepositoryMock.Verify((repo => repo.GetByNameAsync(It.IsAny<User>())), Times.Once());
        }
    }
}
