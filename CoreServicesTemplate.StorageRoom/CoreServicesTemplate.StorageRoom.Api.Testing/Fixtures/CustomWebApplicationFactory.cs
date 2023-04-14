using System.Data.Common;
using CoreServicesTemplate.Shared.Core.Interfaces.IData;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.Fixtures;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    private readonly DbConnection _connection;
    private AppDbContext? _dbContext;

    public CustomWebApplicationFactory()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
    }

    public void OpenDbConnection()
    {
        _connection.Open();

        _dbContext = Services.GetRequiredService<IAppDbContext>() as AppDbContext;

        if (_dbContext != null) _dbContext.Database.EnsureCreated();
    }

    public void CloseDbConnection()
    {
        if (_dbContext != null) _dbContext.Database.EnsureDeleted();

        _connection.Close();
    }

    public AppDbContext? GetContext() => _dbContext;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var optionDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            if (optionDescriptor != null)
            {
                services.Remove(optionDescriptor);
            }

            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IAppDbContext));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<IAppDbContext, AppDbContext>(options =>
            {
                options.UseSqlite(_connection);
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);
        });
    }
}