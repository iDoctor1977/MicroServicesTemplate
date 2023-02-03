using CoreServicesTemplate.Dashboard.Common.CustomMappers;
using CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Dashboard.Common.Interfaces.IServices;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Core.Features;
using CoreServicesTemplate.Dashboard.Core.MapperProfiles;
using CoreServicesTemplate.Dashboard.Services;
using CoreServicesTemplate.Dashboard.Web.CustomMappers;
using CoreServicesTemplate.Dashboard.Web.MapperProfiles;
using CoreServicesTemplate.Dashboard.Web.Models;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Mappers;
using CoreServicesTemplate.Shared.Core.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Injection

builder.Services.AddTransient<IAddUserFeature, AddUserFeature>();
builder.Services.AddTransient<IGetUsersFeature, GetUsersFeature>();
builder.Services.AddTransient<IStorageRoomService, StorageRoomService>();

#endregion

#region Mapper

builder.Services.AddTransient(typeof(IDefaultMapper<,>), typeof(DefaultMapper<,>));

builder.Services.AddTransient(typeof(ICustomMapper<UserViewModel, UserAppModel>), typeof(UserWebCustomMapper));
builder.Services.AddTransient(typeof(ICustomMapper<UsersViewModel, UsersAppModel>), typeof(UsersWebCustomMapper));

builder.Services.AddTransient(typeof(ICustomMapper<UsersApiModel, UsersAppModel>), typeof(UsersApiCustomConsolidator));

#endregion

#region Automapper

builder.Services.AddAutoMapper(typeof(WebMappingProfile), typeof(CoreMappingProfile));

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Use only for xUnit tests
namespace CoreServicesTemplate.Dashboard.Web
{
    public partial class Program { }
}
