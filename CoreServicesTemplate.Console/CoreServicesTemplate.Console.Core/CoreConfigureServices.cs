using CoreServicesTemplate.Console.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Console.Core.Features;
using CoreServicesTemplate.Console.Services;
using CoreServicesTemplate.Shared.Core.Interfaces.IServices;
using Microsoft.Extensions.DependencyInjection;

namespace CoreServicesTemplate.Console.Core
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
