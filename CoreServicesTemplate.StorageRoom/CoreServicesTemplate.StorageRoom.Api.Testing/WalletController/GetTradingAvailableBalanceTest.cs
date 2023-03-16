using System.Net;
using System.Net.Http.Json;
using CoreServicesTemplate.StorageRoom.Api.Testing.Fixtures;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.WalletController
{
    public class GetTradingAvailableBalanceTest : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        private readonly Guid _ownerGuid;

        private const string URL_GET = "api/wallet/";

        public GetTradingAvailableBalanceTest(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;

            _ownerGuid = Guid.NewGuid();

            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            _factory.OpenDbConnection();
        }

        [Fact]
        public async Task Should_Be_Return_Trading_Available_Decimal_Value_From_SQLiteInMemory()
        {
            // Arrange
            SeedDatabaseForTest();

            var uri = $"{URL_GET}{_ownerGuid}";

            // Act
            var response = await _client.GetAsync(uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadFromJsonAsync<decimal>();

            content.Should().Be(2m);
        }

        public void Dispose()
        {
            _factory.CloseDbConnection();
        }

        private void SeedDatabaseForTest()
        {
            if (_factory.GetContext().Database.EnsureCreatedAsync() != null)
            {
                _factory.GetContext().Wallets.Add(new Data.Entities.Wallet
                {
                    Guid = Guid.NewGuid(),
                    OwnerGuid = _ownerGuid,
                    TradingAllowedBalance = 12.34m,
                    OperationAllowedBalance = 12.34m,
                    Balance = 1234,
                    Performance = 1234,
                    State = EntityState.Added,
                    DateCreated = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                    ColWalletItems = new List<WalletItem>
                    {
                        new()
                        {
                            Guid = Guid.NewGuid(),
                            Amount = 10.34m,
                            BuyDate = DateTime.Now,
                            BuyPrice = 1.52m,
                            Quantity = 3,
                            ExtWalletId = 1,
                            DateUpdated = DateTime.Now
                        }
                    }
                });

                _factory.GetContext().SaveChanges();
            }
        }
    }
}
