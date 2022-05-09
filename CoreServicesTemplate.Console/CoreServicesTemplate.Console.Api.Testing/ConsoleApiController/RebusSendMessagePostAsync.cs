using System.Threading.Tasks;
using CoreServicesTemplate.Console.Api.Testing.Fixtures;
using CoreServicesTemplate.Shared.Core.QueueMessages;
using Xunit;

namespace CoreServicesTemplate.Console.Api.Testing.ConsoleApiController
{
    [Collection("BaseTest")]
    public class RebusSendMessagePostAsync
    {
        private readonly BaseTestFixture _fixture;

        public RebusSendMessagePostAsync(BaseTestFixture fixture)
        {
            _fixture = fixture;
            _fixture.GenerateHost();
        }

        [Fact]
        public async Task Should_Execute_Creation_User_With_Rebus()
        {
            //Arrange
            var message = new SimulationMessage
            {
                Content = "Test message"
            };

            var controller = new Controllers.ConsoleApiController(_fixture.ServiceProvider, _fixture.LoggerMock.Object);

            //Act
            await controller.Post(message);

            //Assert
        }
    }
}
