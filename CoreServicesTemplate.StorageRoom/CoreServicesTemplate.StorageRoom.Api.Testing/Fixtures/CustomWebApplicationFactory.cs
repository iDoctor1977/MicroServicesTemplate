using System.Data.Common;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.SeedWorks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.Fixtures;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    private readonly DbConnection _connection;
    private readonly AppDbContext _dbContext;

    public CustomWebApplicationFactory()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        var contextOptions = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(_connection).Options;
        _dbContext = new AppDbContext(contextOptions);
    }

    public void OpenDbConnection()
    {
        _connection.Open();
        _dbContext.Database.EnsureCreated();
    }

    public void CloseDbConnection()
    {
        _dbContext.Database.EnsureDeleted();
        _connection.Close();
    }

    public AppDbContext GetContext() => _dbContext;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.Replace(new ServiceDescriptor(typeof(AppDbContext), _dbContext));
        });
    }
}