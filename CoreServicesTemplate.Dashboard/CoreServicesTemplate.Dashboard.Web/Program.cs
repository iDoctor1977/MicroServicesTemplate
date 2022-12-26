using CoreServicesTemplate.Dashboard.Common.Consolidators;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Core;
using CoreServicesTemplate.Dashboard.Core.MapperProfiles;
using CoreServicesTemplate.Dashboard.Web.Consolidators;
using CoreServicesTemplate.Dashboard.Web.MapperProfiles;
using CoreServicesTemplate.Dashboard.Web.Models;
using CoreServicesTemplate.Shared.Core.Consolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;
using CoreServicesTemplate.Shared.Core.Mappers;
using CoreServicesTemplate.Shared.Core.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Injection

CoreConfigureServices.InitializeDependencies(builder.Services);

#endregion

#region Consolidator

builder.Services.AddTransient<ICustomMapper, CustomMapper>();

builder.Services.AddTransient(typeof(IConsolidatorToData<,>), typeof(DefaultConsolidator<,>));

builder.Services.AddTransient(typeof(IConsolidatorToData<UserViewModel, UserModel>), typeof(UserWebCustomConsolidator));
builder.Services.AddTransient(typeof(IConsolidatorToData<UsersViewModel, UsersModel>), typeof(UsersWebCustomConsolidator));

builder.Services.AddTransient(typeof(IConsolidatorToData<UsersApiModel, UsersModel>), typeof(UsersApiCustomConsolidator));

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
