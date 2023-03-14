using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using CoreServicesTemplate.StorageRoom.Data.Testing.Fixtures;
using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.StorageRoom.Api;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDbContexts;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.Address;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.User;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Data.Testing.AddUserEfDepot
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
            var depot = new ORMFrameworks.EntityFramework.Depots.AddUserEfDepot(_factory.Services.GetRequiredService<IDbContextWrap>(),
                _factory.Services.GetRequiredService<IDefaultMapper<UserAggModel, User>>(),
                _factory.Services.GetRequiredService<IUserRepository>(),
                _factory.Services.GetRequiredService<ILogger<ORMFrameworks.EntityFramework.Depots.AddUserEfDepot>>());

            // Act
            var user = new UserAggModel
            {
                Name = "Walter",
                Surname = "Mazarin",
                Birth = DateTime.Today.Add(new TimeSpan(-1323068)),
                AddressAggModel = new AddressAggModel
                {
                    Address1 = "Via Scandinavia, 20",
                    Address2 = "Boars {FE)",
                    City = "Ferrari",
                    PostalCode = "44100",
                    State = "Italy"
                }
            };

            var response = depot.ExecuteAsync(user);

            // Assert
            response.Result.State.Should().BeOfType<OutcomeState>().And.Be(OutcomeState.Success);
            _factory.CreateContext().Users.Should().HaveCount(3);
        }
    }
}