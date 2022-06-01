using System;
using System.Threading.Tasks;
using CoreServicesTemplate.Console.Api.Testing.Fixtures;
using CoreServicesTemplate.Shared.Core.Builders;
using CoreServicesTemplate.Shared.Core.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoreServicesTemplate.Console.Api.Testing.ConsoleApiController
{
    using Controllers;

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
            //Arrange
            var builder = new UserModelBuilder();
            var usersList = builder
                .AddUser("Foo", "Foo Foo", DateTime.Now.AddDays(-12569))
                .AddUser("Duffy", "Duck", DateTime.Now.AddDays(-11398))
                .AddUser("Micky", "Mouse", DateTime.Now.AddDays(-168963))
                .Build();

            var model = new UsersApiModel
            {
                UsersApiModelList = usersList
            };

            _fixture.StorageRoomServiceMock.Setup(service => service.ReadUsersAsync()).ReturnsAsync(model);

            var controller = new ConsoleApiController(_fixture.ServiceProvider, _fixture.LoggerMock.Object);

            //Act
            var result = await controller.Get();

            //Assert
            _fixture.StorageRoomServiceMock.Verify((method => method.ReadUsersAsync()), Times.Once());
            result.UsersApiModelList.Should().AllBeOfType<UserApiModel>().And.HaveCountGreaterThan(0);
        }
    }
}
