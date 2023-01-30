using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Data.Builders;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using CoreServicesTemplate.Shared.Core.Builders;
using Newtonsoft.Json;
using System.Text;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Api.Testing.UserController.Fixtures;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.UserController
{
    public class GetUserRepositoryAsyncTest : IClassFixture<ApiRepositoryCustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly ApiRepositoryCustomWebApplicationFactory<Program> _factory;

        public GetUserRepositoryAsyncTest(ApiRepositoryCustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
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

            _factory.UserRepositoryMock.Setup(repo => repo.GetByNameAsync(It.IsAny<User>())).Returns(Task.FromResult(userEntity.ElementAtOrDefault(2)));

            //Act
            UserApiModel userApiModel = usersApiModel.UsersApiModelList.ElementAt(2);

            var serializedObject = JsonConvert.SerializeObject(userApiModel);
            var url = ApiUrl.StorageRoom.User.GetUserToStorageRoom();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
                Content = new StringContent(serializedObject, Encoding.Default, "application/json")
            };

            var result = await _client.SendAsync(request);

            //Assert
            _factory.UserRepositoryMock.Verify((repo => repo.GetByNameAsync(It.IsAny<User>())), Times.Once());
        }
    }
}
