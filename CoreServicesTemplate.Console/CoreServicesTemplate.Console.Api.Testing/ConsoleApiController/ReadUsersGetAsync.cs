using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreServicesTemplate.Console.Api.Testing.Fixtures;
using CoreServicesTemplate.Shared.Core.Interfaces.ICqrs;
using CoreServicesTemplate.Shared.Core.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoreServicesTemplate.Console.Api.Testing.ConsoleApiController
{
    [Collection("BaseTest")]
    public class ReadUsersGetAsync
    {
        private readonly BaseTestFixture _fixture;

        public ReadUsersGetAsync(BaseTestFixture fixture)
        {
            _fixture = fixture;
            _fixture.GenerateHost();
        }

        [Fact]
        public async Task Should_Execute_Read_Users()
        {
            var model = new UserApiModel
            {
                PagingData = new PagingData
                {
                    MaxRecords = 100000,
                    PageNumber = 1000
                }
            };

            var users = new List<UserApiModel>
            {
                new UserApiModel
                {
                    Guid = Guid.NewGuid(),
                    Name = "Foo",
                    Surname = "Foo Foo",
                    Birth = DateTime.Now.AddDays(-12569)
                },
                new UserApiModel
                {
                    Guid = Guid.NewGuid(),
                    Name = "Duffy",
                    Surname = "Duck",
                    Birth = DateTime.Now.AddDays(-11398)
                },
                new UserApiModel
                {
                    Guid = Guid.NewGuid(),
                    Name = "Micky",
                    Surname = "Mouse",
                    Birth = DateTime.Now.AddDays(-12569)
                }
            };

            //Arrange
            _fixture.StorageRoomServiceMock.Setup(service => service.ReadUsersAsync()).ReturnsAsync(users);

            var controller = new Controllers.ConsoleApiController(_fixture.ServiceProvider, _fixture.LoggerMock.Object);

            //Act
            var result = await controller.Get(model);

            //Assert
            _fixture.StorageRoomServiceMock.Verify((method => method.ReadUsersAsync()), Times.Once());
            result.Should().AllBeOfType<UserApiModel>().And.HaveCountGreaterThan(0);
        }
    }
}
