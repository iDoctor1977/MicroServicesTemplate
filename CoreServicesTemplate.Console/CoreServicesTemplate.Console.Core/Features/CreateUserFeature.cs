using System;
using System.Net.Http;
using System.Threading.Tasks;
using CoreServicesTemplate.Console.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Console.Core.Aggregates;
using CoreServicesTemplate.Shared.Core.Interfaces.IServices;
using CoreServicesTemplate.Shared.Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CoreServicesTemplate.Console.Core.Features
{
    public class CreateUserFeature : ICreateUserFeature
    {
        private readonly IStorageRoomService _storageRoomService;

        public CreateUserFeature(IServiceProvider service) {
            _storageRoomService = service.GetRequiredService<IStorageRoomService>();
        }

        public async Task<HttpResponseMessage> HandleAsync(UserApiModel model)
        {
            var aggregate = new UserAggregate(model);
            aggregate.SetGuid(Guid.NewGuid());

            var responseMessage = await _storageRoomService.CreateUserAsync(aggregate.ToModel());

            return responseMessage;
        }
    }
}
