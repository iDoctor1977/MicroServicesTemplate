using CoreServicesTemplate.Console.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Console.Common.Models;
using CoreServicesTemplate.Console.Core;
using CoreServicesTemplate.Console.Core.Features;
using CoreServicesTemplate.Console.Core.MapperProfiles;
using CoreServicesTemplate.Console.Core.Presenters;
using CoreServicesTemplate.Console.Core.Receivers;
using CoreServicesTemplate.Console.Web.MapperProfiles;
using CoreServicesTemplate.Console.Web.Models;
using CoreServicesTemplate.Console.Web.Presenters;
using CoreServicesTemplate.Console.Web.Receivers;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;
using CoreServicesTemplate.Shared.Core.Interfaces.IServices;
using CoreServicesTemplate.Shared.Core.Mappers;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.Shared.Core.Presenters;
using CoreServicesTemplate.Shared.Core.Receivers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoreServicesTemplate.Console.Web.NET7.Testing.Fixtures
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

            builder.Services.AddTransient<ICreateUserFeature, CreateUserFeature>();
            builder.Services.AddTransient<IReadUsersFeature, ReadUsersFeature>();
            builder.Services.AddTransient(provider => StorageRoomServiceMock.Object);
            builder.Services.AddTransient(provider => LoggerMock.Object);

            #endregion

            #region Consolidator

            builder.Services.AddTransient<ICustomMapper, CustomMapper>();

            builder.Services.AddTransient(typeof(IConsolidators<,>), typeof(DefaultReceiver<,>));
            builder.Services.AddTransient(typeof(IConsolidators<,>), typeof(DefaultPresenter<,>));

            builder.Services.AddTransient(typeof(IConsolidators<UserViewModel, UserModel>), typeof(CreateUserCustomReceiver));
            builder.Services.AddTransient(typeof(IConsolidators<UsersApiModel, UsersModel>), typeof(GetUsersCoreCustomReceiver));

            builder.Services.AddTransient(typeof(IConsolidators<UserModel, UserViewModel>), typeof(GetUserWebCustomPresenter));
            builder.Services.AddTransient(typeof(IConsolidators<UsersModel, UsersViewModel>), typeof(GetUsersWebCustomPresenter));
            builder.Services.AddTransient(typeof(IConsolidators<UsersModel, UsersApiModel>), typeof(GetUsersCoreCustomPresenter));


            #endregion

            #region Automapper

            builder.Services.AddAutoMapper(typeof(WebMappingProfile), typeof(CoreMappingProfile));

            #endregion

            ServiceProvider = builder.Services.BuildServiceProvider();
        }
    }
}
