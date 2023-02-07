using CoreServicesTemplate.Shared.Core.Filters;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Mappers;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Api.CustomMappers;
using CoreServicesTemplate.StorageRoom.Api.MapperProfiles;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.MappingProfiles;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.SeedWork;
using CoreServicesTemplate.StorageRoom.Core.CustomMappers;
using CoreServicesTemplate.StorageRoom.Core.Features;
using CoreServicesTemplate.StorageRoom.Core.Features.SubSteps;
using CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.AddUser;
using CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.GetUser;
using CoreServicesTemplate.StorageRoom.Core.Interfaces;
using CoreServicesTemplate.StorageRoom.Core.MappingProfiles;
using CoreServicesTemplate.StorageRoom.Data.CustomMappers;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;
using CoreServicesTemplate.StorageRoom.Data.MapperProfiles;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Depots;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Mocks;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Injections

builder.Services.AddTransient<IAddUserFeature, AddUserFeature>();
builder.Services.AddTransient<IGetUserFeature, GetUserFeature>();
builder.Services.AddTransient<IGetUsersFeature, GetUsersFeature>();

builder.Services.AddTransient<IAggregateFactory, AggregateFactory>();

builder.Services.AddTransient<IAddUserDepot, AddUserEfDepot>();
builder.Services.AddTransient<IGetUserDepot, GetUserEfDepot>();
builder.Services.AddTransient<IGetUsersDepot, GetUsersEfDepot>();

if (builder.Configuration["mocked"]!.Equals("true", StringComparison.OrdinalIgnoreCase))
{
    builder.Services.AddTransient<IUserRepository, UserEfRepositoryMock>();
}
else
{
    builder.Services.AddTransient<IUserRepository, UserEfRepository>();
}

#endregion

#region Db provider connection string

if (builder.Configuration["DBProvider"]!.Equals("true", StringComparison.OrdinalIgnoreCase))
{
    // only for SQLite
    builder.Services.AddDbContext<StorageRoomDbContext>();
}
else
{
    // only for SQLServer
    builder.Services.AddDbContext<StorageRoomDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StorageRoomDB")));
}

#endregion

#region Mappers

builder.Services.AddTransient(typeof(IDefaultMapper<,>), typeof(DefaultMapper<,>));
builder.Services.AddTransient(typeof(ICustomMapper<UserApiModel, UserAppModel>), typeof(UserApiCustomMapper));
builder.Services.AddTransient(typeof(ICustomMapper<UsersApiModel, UsersAppModel>), typeof(UsersApiCustomMapper));

builder.Services.AddTransient(typeof(ICustomMapper<UserAppModel, UserAggModel>), typeof(UserCoreCustomMapper));

builder.Services.AddTransient(typeof(ICustomMapper<UsersAppModel, IEnumerable<User>>), typeof(UsersDataCustomMapper));

#endregion

#region Filters

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiLogActionFilterAsync>();
});

#endregion

#region Automapper

builder.Services.AddAutoMapper(typeof(ApiMappingProfile), typeof(DataMappingProfile), typeof(FeatureMappingProfile), typeof(AggregateMappingProfile));

#endregion

#region Pipeline FeatureCommand Sub Steps

builder.Services.AddTransient<ISubStepSupplier, SubStepSupplier>();

builder.Services.AddTransient<AddUserStep1>();
builder.Services.AddTransient<AddUserStep1SubStep1>();
builder.Services.AddTransient<AddUserStep1SubStep2>();

builder.Services.AddTransient<GetUserStep1>();
builder.Services.AddTransient<GetUserStep1SubStep1>();

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
public partial class Program { }