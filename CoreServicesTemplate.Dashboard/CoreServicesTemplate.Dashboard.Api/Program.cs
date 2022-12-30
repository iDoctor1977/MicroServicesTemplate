using CoreServicesTemplate.Dashboard.Common.Consolidators;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Core.MapperProfiles;
using CoreServicesTemplate.Shared.Core.Consolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;
using CoreServicesTemplate.Shared.Core.Mappers;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.Shared.Core.HealthChecks;
using CoreServicesTemplate.Dashboard.Core.Features;
using CoreServicesTemplate.Dashboard.Services;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles;
using CoreServicesTemplate.Shared.Core.Interfaces.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region Injections

builder.Services.AddTransient<IFeatureCommand<UserModel>, AddUserFeature>();
builder.Services.AddTransient<IFeatureQuery<UsersModel>, GetUsersFeature>();
builder.Services.AddTransient<IStorageRoomService, StorageRoomService>();

builder.Services.AddHttpClient();

#endregion

#region Consolidator

builder.Services.AddTransient<ICustomMapper, CustomMapper>();

builder.Services.AddTransient(typeof(IConsolidator<,>), typeof(DefaultConsolidator<,>));
builder.Services.AddTransient(typeof(IConsolidator<UsersApiModel, UsersModel>), typeof(UsersApiCustomConsolidator));

#endregion

#region Automapper

builder.Services.AddAutoMapper(typeof(CoreMappingProfile));

#endregion

builder.Services.AddHealthChecks().AddCheck<StorageRoomHealthCheck>("StorageRooma API service");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHealthChecks("/health");

app.MapControllers();

app.Run();
