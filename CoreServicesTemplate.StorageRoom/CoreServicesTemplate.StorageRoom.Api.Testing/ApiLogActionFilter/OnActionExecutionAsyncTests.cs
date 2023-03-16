using System.Net.Http.Json;
using CoreServicesTemplate.Shared.Core.Filters;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.ApiLogActionFilter
{
    public class OnActionExecutionAsyncTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        private Mock<ILogger<ApiLogActionFilterAsync>> LoggerMock { get; set; }

        public OnActionExecutionAsyncTests(WebApplicationFactory<Program> factory)
        {
            LoggerMock = new Mock<ILogger<ApiLogActionFilterAsync>>();

            _factory = factory;
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddTransient(provider => LoggerMock.Object);
                });
            }).CreateClient(new WebApplicationFactoryClientOptions
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
            LoggerMock.Verify(x => x.Log(LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.AtLeastOnce);
        }
    }
}
