using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Data.Builders;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using CoreServicesTemplate.Shared.Core.Builders;
using Newtonsoft.Json;
using System.Text;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Api.Testing.UserController.Fixtures;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.User;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.UserController
{
    public class GetUserDepotAsyncTest : IClassFixture<ApiDepotCustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly ApiDepotCustomWebApplicationFactory<Program> _factory;

        public GetUserDepotAsyncTest(ApiDepotCustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Should_Return_Specified_User()
        {
            //Arrange
            IUserApiModelAdded userApiModelBuilder = new UserApiModelBuilder();
            IUserAggModelAdded userModelBuilder = new UserAggModelBuilder();

            var usersApiModel =  userApiModelBuilder
                    .AddUser("Foo", "Foo Foo", DateTime.Now.AddDays(-123987))
                    .AddUser("Duffy", "Duck", DateTime.Now.AddDays(-187962))
                    .AddUser("Micky", "Mouse", DateTime.Now.AddDays(-22897))
                    .Build();

            var usersModel =  userModelBuilder
                    .AddUser("Foo", "Foo Foo", DateTime.Now.AddDays(-123987))
                    .AddUser("Duffy", "Duck", DateTime.Now.AddDays(-187962))
                    .AddUser("Micky", "Mouse", DateTime.Now.AddDays(-22897))
                    .Build();


            var modelMock = usersModel.ElementAtOrDefault(2);
            _factory.GetUserDepotMock.Setup(depot => depot.ExecuteAsync(It.IsAny<UserAggModel>())).Returns(Task.FromResult(new OperationResult<UserAggModel>(modelMock)));

            //Act
            UserApiModel userApiModel = usersApiModel.ElementAt(2);

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
            _factory.GetUserDepotMock.Verify((c => c.ExecuteAsync(It.IsAny<UserAggModel>())), Times.Once());
        }
    }
}
