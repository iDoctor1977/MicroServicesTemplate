using System;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Core.Aggregates;
using Microsoft.Extensions.DependencyInjection;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class CreateUserFeature : ICreateUserFeature
    {
        private readonly ICreateUserDepot _createUserDepot;

        public CreateUserFeature(IServiceProvider service) {
            _createUserDepot = service.GetRequiredService<ICreateUserDepot>();
        }

        public async Task ExecuteAsync(UserApiModel model)
        {
            var aggregate = new CreateAggregate(model);
            aggregate.SetGuid(Guid.NewGuid());

            await _createUserDepot.ExecuteAsync(aggregate.ToModel());
        }
    }
}
