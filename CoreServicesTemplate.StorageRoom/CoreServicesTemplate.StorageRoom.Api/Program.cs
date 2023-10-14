using CoreServicesTemplate.Shared.Core.BusModels.Wallet;
using CoreServicesTemplate.Shared.Core.Factories;
using CoreServicesTemplate.Shared.Core.Filters;
using CoreServicesTemplate.Shared.Core.Interfaces.IData;
using CoreServicesTemplate.Shared.Core.Interfaces.IEvents;
using CoreServicesTemplate.Shared.Core.Interfaces.IFactories;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Mappers;
using CoreServicesTemplate.StorageRoom.Api.Bus;
using CoreServicesTemplate.StorageRoom.Api.MapperProfiles;
using CoreServicesTemplate.StorageRoom.Common.DomainModels.Wallet;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models.Wallet;
using CoreServicesTemplate.StorageRoom.Core.Features;
using CoreServicesTemplate.StorageRoom.Core.MapperProfiles;
using CoreServicesTemplate.StorageRoom.Data.CustomMappers;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories;
using CoreServicesTemplate.StorageRoom.Data.MapperProfiles;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Depots;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Repositories;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Repositories.Mocks;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

#region Injections

builder.Services.AddTransient<ICreateWalletFeature, CreateWalletFeature>();
builder.Services.AddTransient<IGetTradingAvailableBalanceFeature, GetTradingAvailableBalanceFeature>();
builder.Services.AddTransient<IGetWalletItemsFeature, GetWalletItemsFeature>();
builder.Services.AddTransient<IGetEmailPropertiesFeature, GetEmailPropertiesFeature>();

builder.Services.AddTransient<ICreateWalletDepot, CreateWalletEfDepot>();
builder.Services.AddTransient<IGetTradingAvailableBalanceDepot, GetTradingAvailableBalanceEfDepot>();
builder.Services.AddTransient<IGetWalletItemsEfDepot, GetWalletItemsEfDepot>();
builder.Services.AddTransient<IGetEmailPropertiesEfDepot, GetEmailPropertiesEfDepot>();

if (builder.Configuration["repositoryMocked"]!.Equals("true", StringComparison.OrdinalIgnoreCase))
{

    builder.Services.AddTransient(typeof(IRepository<>), typeof(EfRepositoryMock<>));
    builder.Services.AddTransient<IWalletRepository, WalletEfRepositoryMock>();
    builder.Services.AddTransient<IWalletItemRepository, WalletItemEfRepositoryMock>();
}
else
{

    builder.Services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));
    builder.Services.AddTransient<IWalletRepository, WalletEfRepository>();
    builder.Services.AddTransient<IWalletItemRepository, WalletItemEfRepository>();
}

#endregion

#region Factories

builder.Services.AddTransient<IDomainEntityFactory, DomainEntityFactory>();
builder.Services.AddTransient<IRepositoryFactory, RepositoryFactory>();

#endregion

#region Db provider connection string

if (builder.Configuration["DBProvider"]!.Equals("true", StringComparison.OrdinalIgnoreCase))
{
    builder.Services.AddDbContext<IUnitOfWorkContext, AppEfContext>(options => options.UseSqlite());
}
else
{

    builder.Services.AddDbContext<IUnitOfWorkContext, AppEfContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StorageRoomDB")));
}

#endregion

#region Mappers

builder.Services.AddTransient(typeof(IDefaultMapper<,>), typeof(DefaultMapper<,>));
builder.Services.AddTransient(typeof(ICustomMapper<WalletModel, Wallet>), typeof(WalletDataCustomMapper));

#endregion

#region Filters

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiLogActionFilterAsync>();
});

#endregion

#region Automapper

builder.Services.AddAutoMapper(typeof(ApiMapperProfile), typeof(DataMapperProfile), typeof(CoreMapperProfile));

#endregion

#region Pipeline FeatureCommand Sub Steps

//builder.Services.AddTransient<ISubStepSupplier, SubStepSupplier>();

//builder.Services.AddTransient<AddUserStep1>();
//builder.Services.AddTransient<AddUserStep1SubStep1>();
//builder.Services.AddTransient<AddUserStep1SubStep2>();

//builder.Services.AddTransient<GetUserStep1>();
//builder.Services.AddTransient<GetUserStep1SubStep1>();

#endregion

#region BusEvents

builder.Services.AddTransient((Func<IServiceProvider, IEventBus<WalletCreatedBusDto>>)(sp =>
{
    var logger = sp.GetRequiredService<ILogger<WalletCreatedBus>>();

    var connectionFactory = new ConnectionFactory { HostName = builder.Configuration["BusConnectionName"], DispatchConsumersAsync = true };
    var exchangeName = builder.Configuration["CreateWalletExchangeName"];

    return new WalletCreatedBus(connectionFactory, exchangeName, logger);
}));

#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/health");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// only for tests
namespace CoreServicesTemplate.StorageRoom.Api
{
    public partial class Program { }
}