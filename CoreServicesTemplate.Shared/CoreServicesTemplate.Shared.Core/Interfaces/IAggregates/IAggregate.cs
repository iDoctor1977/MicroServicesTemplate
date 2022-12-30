using CoreServicesTemplate.Shared.Core.Interfaces.Models;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IAggregates;

public interface IAggregate<T> where T : IAppModel
{
    T Model { set; }

    T ToModel();
    bool IsModelValid();
}