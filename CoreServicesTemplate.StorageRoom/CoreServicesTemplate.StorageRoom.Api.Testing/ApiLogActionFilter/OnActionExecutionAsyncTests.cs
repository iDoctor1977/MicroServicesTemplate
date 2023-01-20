using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Api.Testing.Fixtures;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.ApiLogActionFilter
{
    [Collection("DepotTestBase")]
    public class OnActionExecutionAsyncTests
    {
        private readonly HttpClient _client;
        private readonly TestFixtureDepots _fixture;

        public OnActionExecutionAsyncTests(WebApplicationFactory<Startup> factory, TestFixtureDepots fixture)
        {
            _fixture = fixture;
            _client = _fixture.GenerateClient(factory);
        }

        [Fact]
        public async Task Should_LogTheCallToAnyAction()
        {
            //Arrange
            var apiModel = new UserApiModel
            {
                Name = "Foo",
                Surname = "Foo Foo",
                Birth = DateTime.Now.AddDays(-14000)
            };

            //Act
            var url = API.StorageRoom.User.AddUserToStorageRoomUrl();
            await _client.PostAsJsonAsync($"{url}/{apiModel}", apiModel);

            //Assert
            _fixture.LoggerMock.Verify(x => x.Log(LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.AtLeastOnce);
        }
    }
}
