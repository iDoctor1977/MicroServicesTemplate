using CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Dashboard.Core.Features;
using CoreServicesTemplate.Shared.Core.Interfaces.IServices;
using CoreServicesTemplate.Dashboard.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CoreServicesTemplate.Dashboard.Core
{
    public static class CoreConfigureServices
    {
        public static void InitializeDependencies(IServiceCollection services)
        {
            services.AddTransient<IAddUserFeature, AddUserFeature>();
            services.AddTransient<IGetUsersFeature, GetUsersFeature>();
            services.AddTransient<IStorageRoomService, StorageRoomService>();
        }
    }
}
