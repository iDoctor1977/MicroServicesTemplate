using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Core.Features;
using CoreServicesTemplate.Dashboard.Core.MapperProfiles;
using CoreServicesTemplate.Dashboard.Web.Consolidators;
using CoreServicesTemplate.Dashboard.Web.MapperProfiles;
using CoreServicesTemplate.Dashboard.Web.Models;
using CoreServicesTemplate.Shared.Core.Consolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles;
using CoreServicesTemplate.Shared.Core.Interfaces.IServices;
using CoreServicesTemplate.Shared.Core.Mappers;
using CoreServicesTemplate.Shared.Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using CoreServicesTemplate.Dashboard.Common.Consolidators;

namespace CoreServicesTemplate.Dashboard.Web.Testing.Fixtures
{
    public class TestFixtureBase
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public Mock<IStorageRoomService> StorageRoomServiceMock { get; private set; }
        public Mock<ILogger<Controllers.HomeController>> LoggerMock { get; private set; }

        public TestFixtureBase()
        {
            StorageRoomServiceMock = new Mock<IStorageRoomService>();
            ServiceProvider = null!;
            LoggerMock = new Mock<ILogger<Controllers.HomeController>>();
        }

        public void GenerateHost()
        {
            var builder = WebApplication.CreateBuilder();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            #region Injection

            builder.Services.AddTransient<IFeatureCommand<UserAppModel>, AddUserFeature>();
            builder.Services.AddTransient<IFeatureQuery<UsersAppModel>, GetUsersFeature>();
            builder.Services.AddTransient(provider => StorageRoomServiceMock.Object);
            builder.Services.AddTransient(provider => LoggerMock.Object);

            #endregion

            #region Consolidator

            builder.Services.AddTransient<ICustomMapper, CustomMapper>();

            builder.Services.AddTransient(typeof(IConsolidator<,>), typeof(DefaultConsolidator<,>));

            builder.Services.AddTransient(typeof(IConsolidator<UserViewModel, UserAppModel>), typeof(UserWebCustomConsolidator));
            builder.Services.AddTransient(typeof(IConsolidator<UsersViewModel, UsersAppModel>), typeof(UsersWebCustomConsolidator));

            builder.Services.AddTransient(typeof(IConsolidator<UsersApiModel, UsersAppModel>), typeof(UsersApiCustomConsolidator));

            #endregion

            #region Automapper

            builder.Services.AddAutoMapper(typeof(WebMappingProfile), typeof(CoreMappingProfile));

            #endregion

            ServiceProvider = builder.Services.BuildServiceProvider();
        }
    }
}
