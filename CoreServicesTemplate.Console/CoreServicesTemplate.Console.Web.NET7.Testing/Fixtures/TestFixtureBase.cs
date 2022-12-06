using CoreServicesTemplate.Console.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Console.Core.Features;
using CoreServicesTemplate.Console.Web.MapperProfiles;
using CoreServicesTemplate.Console.Web.Models;
using CoreServicesTemplate.Console.Web.Presenters;
using CoreServicesTemplate.Console.Web.Receivers;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;
using CoreServicesTemplate.Shared.Core.Interfaces.IServices;
using CoreServicesTemplate.Shared.Core.Mappers;
using CoreServicesTemplate.Shared.Core.Models;
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
            builder.Services.AddTransient(typeof(IConsolidators<UserViewModel, UserApiModel>), typeof(CreateUserCustomReceiver));
            builder.Services.AddTransient(typeof(IConsolidators<UserApiModel, UserViewModel>), typeof(ReadUserCustomPresenter));
            builder.Services.AddTransient(typeof(IConsolidators<UsersApiModel, UsersViewModel>), typeof(ReadUsersCustomPresenter));

            #endregion

            #region Automapper

            builder.Services.AddAutoMapper(typeof(WebMappingProfile));

            #endregion

            ServiceProvider = builder.Services.BuildServiceProvider();
        }
    }
}
