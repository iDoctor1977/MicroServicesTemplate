using CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Core.Features;
using CoreServicesTemplate.Dashboard.Core.MapperProfiles;
using CoreServicesTemplate.Dashboard.Core.Presenters;
using CoreServicesTemplate.Dashboard.Core.Receivers;
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

namespace CoreServicesTemplate.Dashboard.Api.Testing.Fixtures
{
    public class TestFixtureBase
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public Mock<IStorageRoomService> StorageRoomServiceMock { get; private set; }
        public Mock<ILogger<Controllers.HomeApiController>> LoggerMock { get; private set; }

        public TestFixtureBase()
        {
            StorageRoomServiceMock = new Mock<IStorageRoomService>();
            ServiceProvider = null!;
            LoggerMock = new Mock<ILogger<Controllers.HomeApiController>>();

        }

        public void GenerateHost()
        {
            var builder = WebApplication.CreateBuilder();

            #region Injections

            builder.Services.AddTransient<IAddUserFeature, AddUserFeature>();
            builder.Services.AddTransient<IGetUsersFeature, GetUsersFeature>();
            builder.Services.AddTransient(provider => StorageRoomServiceMock.Object);
            builder.Services.AddTransient(provider => LoggerMock.Object);

            #endregion

            #region Consolidator

            builder.Services.AddTransient<ICustomMapper, CustomMapper>();

            builder.Services.AddTransient(typeof(IConsolidators<,>), typeof(DefaultReceiver<,>));
            builder.Services.AddTransient(typeof(IConsolidators<,>), typeof(DefaultPresenter<,>));

            builder.Services.AddTransient(typeof(IConsolidators<UsersApiModel, UsersModel>), typeof(GetUsersCoreCustomReceiver));

            builder.Services.AddTransient(typeof(IConsolidators<UsersModel, UsersApiModel>), typeof(GetUsersCoreCustomPresenter));

            #endregion

            #region Automapper

            builder.Services.AddAutoMapper(typeof(CoreMappingProfile));

            #endregion

            ServiceProvider = builder.Services.BuildServiceProvider();
        }
    }
}
