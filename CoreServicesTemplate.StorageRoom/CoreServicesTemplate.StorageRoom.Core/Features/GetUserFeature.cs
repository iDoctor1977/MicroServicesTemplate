using System;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates;
using CoreServicesTemplate.StorageRoom.Core.Interfaces;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class GetUserFeature : AFeatureQueryBase<UserAggregate, UserModel, UserModel>
    {
        private readonly IGetUserDepot _getUserDepot;
        private readonly IOperationsSupplier _operationsSupplier;

        public GetUserFeature(IGetUserDepot getUserDepot, IOperationsSupplier operationsSupplier)
        {
            _getUserDepot = getUserDepot;
            _operationsSupplier = operationsSupplier;
        }
        public override IQueryHandleAggregate<UserModel> SetAggregate(UserModel model)
        {
            AggregateModel = new UserAggregate(model);

            return this;
        }

        public override async Task<UserModel> HandleAsync()
        {
            var userAggregate = AggregateModel;

            // Attach model to your model domain logic
            userAggregate?.SetGuid(Guid.NewGuid());

            // execute interaction with repository if necessary
            var resultModel = await _getUserDepot.HandleAsync(AggregateModel.ToModel());

            // execute getUserFeature sub steps
            // this part is added only for features scalability 
            var resultAggregate = new UserAggregate(resultModel);
            resultAggregate.SetBirth(DateTime.Now);
            resultAggregate = await _operationsSupplier.HandleGetAsync(resultAggregate);

            return resultAggregate.ToModel();
        }
    }
}
