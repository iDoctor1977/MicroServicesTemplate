using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework;
using CoreServicesTemplate.StorageRoom.Data.Testing.AddUserEfDepot.Fixtures;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace CoreServicesTemplate.StorageRoom.Data.Testing.AddUserEfDepot
{
    public class AddUserEfDepotSQLiteInMemoryTest : IClassFixture<DataCustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly DataCustomWebApplicationFactory<Program> _factory;

        public AddUserEfDepotSQLiteInMemoryTest(DataCustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public void Should_Get_All_User_From_InMemorySQLiteDb()
        {
            // Arrange
            var user1 = new User
            {
                Guid = Guid.NewGuid(),
                Name = "Filippo",
                Surname = "Foglia",
                Birth = DateTime.Today,
                IsDeleted = false,
                DeleteBy = "User",
                DeleteDate = DateTime.Today
            };
            var user2 = new User
            {
                Guid = Guid.NewGuid(),
                Name = "Stefania",
                Surname = "Felisati",
                Birth = DateTime.Today,
                IsDeleted = false,
                DeleteBy = "User",
                DeleteDate = DateTime.Today
            };

            var context = _factory.Services.GetRequiredService<StorageRoomDbContext>();

            // Act

            // Assert
        }
    }
}
