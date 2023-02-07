using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using CoreServicesTemplate.Shared.Core.Filters;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoreServicesTemplate.StorageRoom.Data.Testing.AddUserEfDepot.Fixtures
{
    public class DataCustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        public Mock<ILogger<ApiLogActionFilterAsync>> LoggerMock { get; }
        public Mock<IAddUserDepot> AddUserDepotMock { get; }
        public Mock<IGetUserDepot> GetUserDepotMock { get; }
        public Mock<IGetUsersDepot> GetUsersDepotMock { get; }

        public DataCustomWebApplicationFactory()
        {
            LoggerMock = new Mock<ILogger<ApiLogActionFilterAsync>>();
            AddUserDepotMock = new Mock<IAddUserDepot>();
            GetUserDepotMock = new Mock<IGetUserDepot>();
            GetUsersDepotMock = new Mock<IGetUsersDepot>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddTransient(provider => LoggerMock.Object);
                services.Replace(new ServiceDescriptor(typeof(IAddUserDepot), AddUserDepotMock.Object));
                services.Replace(new ServiceDescriptor(typeof(IGetUserDepot), GetUserDepotMock.Object));
                services.Replace(new ServiceDescriptor(typeof(IGetUsersDepot), GetUsersDepotMock.Object));

                var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<StorageRoomDbContext>));
                if (dbContextDescriptor != null) services.Remove(dbContextDescriptor);

                var dbConnectionDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbConnection));
                if (dbConnectionDescriptor != null) services.Remove(dbConnectionDescriptor);

                // Create open SqliteConnection so EF won't automatically close it.
                services.AddSingleton<DbConnection>(container =>
                {
                    var connection = new SqliteConnection("DataSource=:memory:");
                    connection.Open();

                    return connection;
                });

                services.AddDbContext<StorageRoomDbContext>((container, options) =>
                {
                    var connection = container.GetRequiredService<DbConnection>();
                    options.UseSqlite(connection);
                });
            });

            builder.UseEnvironment("Development");
        }
    }
}
