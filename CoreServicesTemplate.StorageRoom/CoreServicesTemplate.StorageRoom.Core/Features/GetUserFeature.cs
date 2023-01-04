using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Interfaces;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;
using CoreServicesTemplate.StorageRoom.Core.Interfaces;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class GetUserFeature : IFeatureQuery<UserAppModel, UserAppModel>
    {
        private readonly IUserAggregateRoot _userAggregateRoot;
        private readonly IConsolidator<UserAppModel, UserAggModel> _userModelConsolidator;
        private readonly IGetUserDepot _getUserDepot;
        private readonly ISubStepSupplier _subStepSupplier;

        public GetUserFeature(
            IUserAggregateRoot userAggregateRoot, 
            IConsolidator<UserAppModel, UserAggModel> userModelConsolidator,
            IGetUserDepot getUserDepot, 
            ISubStepSupplier subStepSupplier)
        {
            _getUserDepot = getUserDepot;
            _subStepSupplier = subStepSupplier;
            _userAggregateRoot = userAggregateRoot;
            _userModelConsolidator = userModelConsolidator;
        }

        public async Task<UserAppModel> HandleAsync(UserAppModel @in)
        {
            // execute interaction with repository if necessary
            var modelAppOut = await _getUserDepot.HandleAsync(@in);

            // Do something on User aggregate

            // execute getUserFeature sub steps
            // this part is added only for features scalability 
            modelAppOut = await _subStepSupplier.GetHandleAsync(modelAppOut);

            return modelAppOut;
        }
    }
}
