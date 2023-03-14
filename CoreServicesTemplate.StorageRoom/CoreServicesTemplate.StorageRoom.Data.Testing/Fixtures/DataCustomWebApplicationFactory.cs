using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Data.Common;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.SeedWorks;

namespace CoreServicesTemplate.StorageRoom.Data.Testing.Fixtures
{
    public class DataCustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        private readonly DbConnection _connection;
        private readonly DbContextOptions<StorageRoomDbContext> _contextOptions;


        public DataCustomWebApplicationFactory()
        {
            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();

            _contextOptions = new DbContextOptionsBuilder<StorageRoomDbContext>().UseSqlite(_connection).Options;

            SeedDb();
        }

        public StorageRoomDbContext CreateContext() => new StorageRoomDbContext(_contextOptions);

        private void SeedDb()
        {
            using var context = CreateContext();

            if (context.Database.EnsureCreated())
            {
                context.AddRange(
                    new User
                    {
                        Guid = Guid.NewGuid(),
                        Name = "Filippo",
                        Surname = "Foglia",
                        Birth = DateTime.Today,
                        State = EntityState.Added,
                    }, new User
                    {
                        Guid = Guid.NewGuid(),
                        Name = "Stefania",
                        Surname = "Felisati",
                        Birth = DateTime.Today,
                        State = EntityState.Added,
                    });
            }

            context.SaveChanges();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.Replace(new ServiceDescriptor(typeof(StorageRoomDbContext), CreateContext()));
            });

            builder.UseEnvironment("Development");
        }
    }
}