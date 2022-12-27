using System;
using System.Threading.Tasks;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates;
using CoreServicesTemplate.StorageRoom.Core.Interfaces;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class GetUserFeature : IGetUserFeature
    {
        private readonly IGetUserDepot _getUserDepot;
        private readonly IOperationsSupplier _operationsSupplier;

        public GetUserFeature(IGetUserDepot getUserDepot, IOperationsSupplier operationsSupplier)
        {
            _getUserDepot = getUserDepot;
            _operationsSupplier = operationsSupplier;
        }

        public async Task<UserModel> HandleAsync(UserModel model)
        {
            // Attach model to your model domain logic
            var aggregate = new UserAggregate(model);
            aggregate.SetGuid(Guid.NewGuid());

            // execute interaction with repository if necessary
            var resultModel = await _getUserDepot.HandleAsync(aggregate.ToModel());

            // execute getUserFeature sub steps
            // this part is added only for features scalability 
            var resultAggregate = new UserAggregate(resultModel);
            resultAggregate.SetBirth(DateTime.Now);
            resultAggregate = await _operationsSupplier.HandleGetAsync(resultAggregate);

            return resultAggregate.ToModel();
        }
    }
}
