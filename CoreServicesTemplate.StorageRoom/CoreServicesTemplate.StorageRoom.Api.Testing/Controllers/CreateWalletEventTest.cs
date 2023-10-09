﻿using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Models.WalletItem;
using CoreServicesTemplate.StorageRoom.Api.Testing.Fixtures;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using System.Net;
using CoreServicesTemplate.Shared.Core.Models.Wallet;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.Controllers
{
    public class CreateWalletEventTest : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        private readonly Guid _ownerGuid;

        private static readonly string UrlGet = ApiUrl.StorageRoomApi.CreateWalletEvent();

        public CreateWalletEventTest(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;

            _ownerGuid = Guid.NewGuid();

            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Should_Send_An_Email_When_Wallet_Event_Received()
        {
            // Arrange
            _factory.OpenDbConnection();

            SeedDatabaseForTest();

            var uri = $"{UrlGet}/{_ownerGuid}";

            // Act
            var response = await _client.GetAsync(uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            //(await response.Content.ReadFromJsonAsync<CreateWalletEventApiDto>()).Should().;
        }

        public void Dispose()
        {
            _factory.CloseDbConnection();
        }

        private void SeedDatabaseForTest()
        {
            var dbContext = _factory.GetContext();

            if (dbContext != null && !dbContext.Database.EnsureCreatedAsync().Equals(null))
            {
                dbContext.Wallets.Add(new Data.Entities.Wallet
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
                });

                dbContext.SaveChanges();

                dbContext.WalletItems.AddRange(
                    new WalletItem
                    {
                        Guid = Guid.NewGuid(),
                        Amount = 10.34m,
                        BuyDate = DateTime.Now,
                        BuyPrice = 1.52m,
                        Quantity = 3,
                        ExtWalletId = 1,
                        DateUpdated = DateTime.Now,
                        ExtTicker = "A2A",
                        ExtMarketItemGuid = Guid.NewGuid()
                    }
                );

                dbContext.SaveChanges();
            }
        }
    }
}
