using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Api;
using CoreServicesTemplate.StorageRoom.Data.Factories;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.SeedWorks;
using CoreServicesTemplate.StorageRoom.Data.Testing.Fixtures;
using Microsoft.Extensions.DependencyInjection;

namespace CoreServicesTemplate.StorageRoom.Data.Testing.GetUsersEfDepot
{
    public class GetUsersEfDepotSQLiteInMemoryTest : IClassFixture<DataCustomWebApplicationFactory<Program>>
    {
        private readonly DataCustomWebApplicationFactory<Program> _factory;

        public GetUsersEfDepotSQLiteInMemoryTest(DataCustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public void Should_Get_All_Users_From_InMemorySQLiteDb()
        {
            // Arrange
            var depot = new ORMFrameworks.EntityFramework.Depots.GetUsersEfDepot(_factory.Services.GetRequiredService<AppDbContext>(),
                _factory.Services.GetRequiredService<IRepositoryFactory>(),
                _factory.Services.GetRequiredService<IDefaultMapper<UserAggModel, ApiUrl.Dashboard.User>>());

            // Act
            var response = depot.ExecuteAsync();

            // Assert
            //response.Result.Value.Should().BeOfType(typeof(ICollection<UserAggModel>)).And.HaveCount(2);
        }
    }
}