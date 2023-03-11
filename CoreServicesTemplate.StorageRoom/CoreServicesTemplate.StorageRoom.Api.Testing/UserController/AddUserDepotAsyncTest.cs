using System.Net;
using System.Net.Http.Json;
using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Api.Testing.UserController.Fixtures;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.UserController
{
    public class AddUserDepotAsyncTest : IClassFixture<ApiDepotCustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly ApiDepotCustomWebApplicationFactory<Program> _factory;

        public AddUserDepotAsyncTest(ApiDepotCustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Should_Execute_Adding_New_User()
        {
            //Arrange
            var modelApi = new UserApiModel
            {
                Name = "Foo",
                Surname = "Foo Foo",
                Birth = DateTime.Now.AddDays(-14000),

                AddressApiModel = new AddressApiModel
                {
                    Address1 = "Via Copparo, 208 int. 10",
                    Address2 = "Boara (FE)",
                    PostalCode = "44123",
                    City = "Ferrara",
                    State = "Italy"
                }
            };

            _factory.AddUserDepotMock.Setup(depot => depot.ExecuteAsync(It.IsAny<UserAggModel>())).ReturnsAsync(new OperationResult(OutcomeState.Success));

            //Act
            var url = ApiUrl.StorageRoom.User.AddUserToStorageRoom();
            var responseMessage = await _client.PostAsJsonAsync($"{url}/{modelApi}", modelApi);

            //Assert
            _factory.AddUserDepotMock.Verify(method => method.ExecuteAsync(It.IsAny<UserAggModel>()), Times.Once);
            responseMessage.Should().NotBeNull().And.BeOfType<HttpResponseMessage>();
            responseMessage.Headers.Location?.AbsoluteUri.Should().NotBeNull().And.Be(ApiUrl.StorageRoom.User.IndexFromUserToStorageRoom());
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}