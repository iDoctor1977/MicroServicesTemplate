using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Data.Common;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.SeedWorks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CoreServicesTemplate.StorageRoom.Data.Testing.Fixtures
{
    public class DataCustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        private readonly DbConnection _connection;
        private readonly DbContextOptions<AppDbContext> _contextOptions;


        public DataCustomWebApplicationFactory()
        {
            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();

            _contextOptions = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(_connection).Options;

            SeedDb();
        }

        public AppDbContext CreateContext() => new AppDbContext(_contextOptions);

        private void SeedDb()
        {
            using var context = CreateContext();

            if (context.Database.EnsureCreated())
            {
                context.AddRange(
                    new ApiUrl.Dashboard.User
                    {
                        Guid = Guid.NewGuid(),
                        Name = "Filippo",
                        Surname = "Foglia",
                        Birth = DateTime.Today,
                        State = EntityState.Added,
                    }, new ApiUrl.Dashboard.User
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
                services.Replace(new ServiceDescriptor(typeof(AppDbContext), CreateContext()));
            });

            builder.UseEnvironment("Development");
        }
    }
}