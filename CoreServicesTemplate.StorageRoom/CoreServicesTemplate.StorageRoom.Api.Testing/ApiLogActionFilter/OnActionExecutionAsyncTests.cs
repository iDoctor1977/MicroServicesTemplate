﻿using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Builders;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.Shared.Core.Resources;
using CoreServicesTemplate.StorageRoom.Api.Testing.Fixtures;
using CoreServicesTemplate.StorageRoom.Data.Builders;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.ApiLogActionFilter
{
    [Collection("BaseTest")]
    public class OnActionExecutionAsyncTests
    {
        private readonly HttpClient _client;
        private readonly TestFixtureBase _fixture;

        public OnActionExecutionAsyncTests(WebApplicationFactory<Startup> factory, TestFixtureBase fixture)
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
            await _client.PostAsJsonAsync($"{ApiUrlStrings.StorageRoomUserControllerLocalhostUrl}/AddUser/{apiModel}", apiModel);

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
