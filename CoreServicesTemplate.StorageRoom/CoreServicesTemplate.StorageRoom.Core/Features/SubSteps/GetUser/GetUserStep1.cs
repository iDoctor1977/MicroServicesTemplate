using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Attributes;
using CoreServicesTemplate.Shared.Core.Builders;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Core.Aggregates;

namespace CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.GetUser
{
    [Root]
    public class GetUserStep1 : RootPipelineBuilder<UserAggregate, UserAggregate>
    {
        private readonly IGetUserDepot _getUserDepot;

        public GetUserStep1(IGetUserDepot getUserDepot)
        {
            _getUserDepot = getUserDepot;
        }

        protected override async Task<UserAggregate> HandleRootStepAsync(UserAggregate aggregate)
        {
            // Do anything on Depot
            // Example: var model = aggregate.ToModel();
            //                      model.SetName("Alfred");
            //
            //                      var resultModel = _getUserDepot.HandleAsync(model);
            //                      aggregate.SetName(resultModel.Result.Name);
            //                      aggregate.SetSurname(resultModel.Result.Surname);
            //                      aggregate.SetBirth(model.Birth);

            return aggregate;
        }
    }
}