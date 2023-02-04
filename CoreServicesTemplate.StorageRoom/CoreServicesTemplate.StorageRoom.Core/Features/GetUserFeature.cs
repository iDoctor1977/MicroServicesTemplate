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
        private readonly ICustomMapper<UserAppModel, UserAggModel> _userCustomMapper;
        private readonly IGetUserDepot _getUserDepot;
        private readonly ISubStepSupplier _subStepSupplier;

        public GetUserFeature(
            ICustomMapper<UserAppModel, UserAggModel> userCustomMapper,
            IGetUserDepot getUserDepot, 
            ISubStepSupplier subStepSupplier)
        {
            _getUserDepot = getUserDepot;
            _subStepSupplier = subStepSupplier;
            _userCustomMapper = userCustomMapper;
        }

        public async Task<UserAppModel> ExecuteAsync(UserAppModel @in)
        {
            // execute interaction with repository if necessary
            var modelAppOut = await _getUserDepot.ExecuteAsync(@in);

            // Do something on User aggregate if necessary

            // execute getUserFeature sub steps
            // this part is added only for features scalability 
            modelAppOut = _subStepSupplier.ExecuteGetAsync(modelAppOut);

            return modelAppOut;
        }

        public UserAppModel Execute(UserAppModel @in)
        {
            var appModel = _getUserDepot.Execute(@in);

            return appModel;
        }
    }
}
