using CoreServicesTemplate.Shared.Core.Interfaces.IResolveMappers;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;
using CoreServicesTemplate.StorageRoom.Core.Interfaces;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class GetUserFeature : IGetUserFeature
    {
        private readonly IResolveMapper<UserAppModel, UserAggModel> _userModelConsolidator;
        private readonly IGetUserDepot _getUserDepot;
        private readonly ISubStepSupplier _subStepSupplier;

        public GetUserFeature(
            IResolveMapper<UserAppModel, UserAggModel> userModelConsolidator,
            IGetUserDepot getUserDepot, 
            ISubStepSupplier subStepSupplier)
        {
            _getUserDepot = getUserDepot;
            _subStepSupplier = subStepSupplier;
            _userModelConsolidator = userModelConsolidator;
        }

        public async Task<UserAppModel> HandleAsync(UserAppModel @in)
        {
            // execute interaction with repository if necessary
            var modelAppOut = await _getUserDepot.HandleAsync(@in);

            // Do something on User aggregate if necessary

            // execute getUserFeature sub steps
            // this part is added only for features scalability 
            modelAppOut = await _subStepSupplier.GetHandleAsync(modelAppOut);

            return modelAppOut;
        }

        public UserAppModel Handle(UserAppModel @in)
        {
            var appModel = _getUserDepot.Handle(@in);

            return appModel;
        }
    }
}
