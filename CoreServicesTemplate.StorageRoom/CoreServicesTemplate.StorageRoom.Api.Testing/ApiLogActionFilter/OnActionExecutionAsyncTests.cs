using System.Net.Http.Json;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Api.Testing.ApiLogActionFilter.Fixtures;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.ApiLogActionFilter
{
    public class OnActionExecutionAsyncTests : IClassFixture<ApiLogCustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly ApiLogCustomWebApplicationFactory<Program> _factory;

        public OnActionExecutionAsyncTests(ApiLogCustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
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
            var url = ApiUrl.StorageRoom.User.AddUserToStorageRoom();
            await _client.PostAsJsonAsync($"{url}/{apiModel}", apiModel);

            //Assert
            _factory.LoggerMock.Verify(x => x.Log(LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.AtLeastOnce);
        }
    }
}
