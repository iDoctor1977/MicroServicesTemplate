using System.Net;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Api.Testing.UserController.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Newtonsoft.Json;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.UserController
{
    public class GetUsersDepotAsyncTest : IClassFixture<ApiDepotCustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly ApiDepotCustomWebApplicationFactory<Program> _factory;

        public GetUsersDepotAsyncTest(ApiDepotCustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Should_Access_To_UserDepot_HandleAsync_At_Last_Once()
        {
            //Arrange
            var userBuilder = new UserAggModelBuilder();
            var users = userBuilder
                .AddUser("Foo", "Foo Foo", DateTime.Now.AddDays(-123987))
                .AddUser("Duffy", "Duck", DateTime.Now.AddDays(-187962))
                .AddUser("Micky", "Mouse", DateTime.Now.AddDays(-22897))
                .Build();

            _factory.GetUsersDepotMock.Setup(depot => depot.ExecuteAsync()).Returns(Task.FromResult(new OperationResult<ICollection<UserAggModel>>(users.ToList())));

            //Act
            var url = ApiUrl.StorageRoom.User.GetAllUserToStorageRoom();
            var result = await _client.GetAsync(url);

            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = new StringContent(JsonConvert.SerializeObject(result));
            _factory.GetUsersDepotMock.Verify((c => c.ExecuteAsync()), Times.Once());
        }
    }
}
