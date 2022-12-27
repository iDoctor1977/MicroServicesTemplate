using CoreServicesTemplate.Shared.Core.Attributes;
using CoreServicesTemplate.Shared.Core.Builders;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Core.Aggregates;

namespace CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.GetUser
{
    [Root]
    public class GetUserStep1 : RootPipelineBuilder<GetAggregate, GetAggregate>
    {
        private readonly IGetUserDepot _getUserDepot;

        public GetUserStep1(IGetUserDepot getUserDepot)
        {
            _getUserDepot = getUserDepot;
        }

        protected override GetAggregate ExecuteRootStep(GetAggregate aggregate)
        {
            // Read
            var model = aggregate.ToModel();

            // Do

            var resultModel = _getUserDepot.HandleAsync(model);
           
            aggregate.SetName(resultModel.Result.Name);
            aggregate.SetSurname(resultModel.Result.Surname);
            aggregate.SetBirthDay(model.Birth);

            // Write

            return aggregate;
        }
    }
}