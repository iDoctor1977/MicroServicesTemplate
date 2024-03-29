﻿using System.Net.Http.Json;
using CoreServicesTemplate.Shared.Core.Filters;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Models.Wallet;
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

        private Mock<ILogger<ApiLogActionFilterAsync>> LoggerMock { get; set; }

        private static readonly string UrlPost = ApiUrl.StorageRoomApi.CreateWallet();

        public OnActionExecutionAsyncTests(WebApplicationFactory<Program> factory)
        {
            LoggerMock = new Mock<ILogger<ApiLogActionFilterAsync>>();

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
            var apiModel = new RequestStorageRoomCreateWalletApiDto()
            {
                TradingAllowedBalance = 1.23m,
                OperationAllowedBalance = 12.3m,
                Balance = 2.36m
            };

            // Act
            await _client.PostAsJsonAsync(UrlPost, apiModel);

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
