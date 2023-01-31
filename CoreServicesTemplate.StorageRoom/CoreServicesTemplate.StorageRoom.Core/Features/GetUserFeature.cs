using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;
using CoreServicesTemplate.StorageRoom.Core.Interfaces;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class GetUserFeature : IGetUserFeature
    {
        private readonly IMapping<UserAppModel, UserAggModel> _userMapper;
        private readonly IGetUserDepot _getUserDepot;
        private readonly ISubStepSupplier _subStepSupplier;

        public GetUserFeature(
            IMapping<UserAppModel, UserAggModel> userMapper,
            IGetUserDepot getUserDepot, 
            ISubStepSupplier subStepSupplier)
        {
            _getUserDepot = getUserDepot;
            _subStepSupplier = subStepSupplier;
            _userMapper = userMapper;
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
