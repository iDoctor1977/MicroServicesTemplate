using System.Net;
using System.Net.Http.Json;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Api.Testing.UserController.Fixtures;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.UserController
{
    public class AddUserRepositoryAsyncTest : IClassFixture<ApiRepositoryCustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly ApiRepositoryCustomWebApplicationFactory<Program> _factory;

        public AddUserRepositoryAsyncTest(ApiRepositoryCustomWebApplicationFactory<Program> factory)
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
                    City = "Ferrara",
                    PostalCode = "44123",
                    State = "Italy"
                }
            };

            _factory.DbContextWrapMock.Setup(context => context.SaveChangesAsync()).Returns(Task.CompletedTask);
            _factory.UserRepositoryMock.Setup(repo => repo.AddCustomAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

            //Act
            var url = ApiUrl.StorageRoom.User.AddUserToStorageRoom();
            var responseMessage = await _client.PostAsJsonAsync($"{url}/{modelApi}", modelApi);

            //Assert
            _factory.UserRepositoryMock.Verify(method => method.AddCustomAsync(It.IsAny<User>()), Times.Once);
            responseMessage.Should().NotBeNull().And.BeOfType<HttpResponseMessage>();
            responseMessage.Headers.Location?.AbsoluteUri.Should().NotBeNull().And.Be(ApiUrl.StorageRoom.User.IndexFromUserToStorageRoom());
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
