using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Interfaces;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;
using CoreServicesTemplate.StorageRoom.Core.Interfaces;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class GetUserFeature : AFeatureQueryBase<UserAppModel, UserAppModel>
    {
        private readonly IUserRoot _userAggregateRoot;
        private readonly IConsolidator<UserAppModel, UserAggModel> _userModelConsolidator;
        private readonly IGetUserDepot _getUserDepot;
        private readonly ISubStepSupplier _subStepSupplier;

        public GetUserFeature(
            IUserRoot userAggregateRoot, 
            IConsolidator<UserAppModel, UserAggModel> userModelConsolidator,
            IGetUserDepot getUserDepot, 
            ISubStepSupplier subStepSupplier)
        {
            ModelAppIn = new UserAppModel();
            ModelAppOut = new UserAppModel();

            _getUserDepot = getUserDepot;
            _subStepSupplier = subStepSupplier;
            _userAggregateRoot = userAggregateRoot;
            _userModelConsolidator = userModelConsolidator;
        }
        public override IQueryHandleAggregate<UserAppModel> SetModel(UserAppModel model)
        {
            ModelAppIn = model;

            return this;
        }

        public override async Task<UserAppModel> HandleAsync()
        {
            // execute interaction with repository if necessary
            ModelAppOut = await _getUserDepot.HandleAsync(ModelAppOut);

            // Do something on User aggregate

            // execute getUserFeature sub steps
            // this part is added only for features scalability 
            ModelAppOut = await _subStepSupplier.HandleGetAsync(ModelAppOut);

            return ModelAppOut;
        }
    }
}
