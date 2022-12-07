using System;
using System.Net.Http;
using System.Threading.Tasks;
using CoreServicesTemplate.Console.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Console.Common.Models;
using CoreServicesTemplate.Console.Core.Aggregates;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IServices;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Console.Core.Features
{
    public class AddUserFeature : IAddUserFeature
    {
        private readonly IStorageRoomService _storageRoomService;
        private readonly IConsolidators<UserModel, UserApiModel> _consolidators;

        public AddUserFeature(IStorageRoomService storageRoomService, IConsolidators<UserModel, UserApiModel> consolidators) 
        {
            _storageRoomService = storageRoomService;
            _consolidators = consolidators;
        }

        public async Task<HttpResponseMessage> HandleAsync(UserModel model)
        {
            var aggregate = new UserAggregate(model);
            aggregate.SetGuid(Guid.NewGuid());

            var apiModel = _consolidators.ToData(aggregate.ToModel());

            var responseMessage = await _storageRoomService.AddUserAsync(apiModel);

            return responseMessage;
        }
    }
}
