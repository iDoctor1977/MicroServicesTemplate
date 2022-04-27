using System;
using System.Net.Http;
using System.Threading.Tasks;
using CoreServicesTemplate.Console.Core.Aggregates;

namespace CoreServicesTemplate.Console.Core.Features
{
    public class CreateUserFeature : ICreateUserFeature
    {
        private readonly IStorageRoomService _storageRoomService;

        public CreateUserFeature(IServiceProvider service) {
            _storageRoomService = service.GetRequiredService<IStorageRoomService>();
        }

        public async Task<HttpResponseMessage> ExecuteAsync(UserApiModel model)
        {
            var aggregate = new UserAggregate(model);
            aggregate.SetGuid(Guid.NewGuid());

            var responseMessage = await _storageRoomService.CreateUserAsync(aggregate.ToModel());

            return responseMessage;
        }
    }
}
